using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WerewolfOnline.Model;
using WerewolfOnline.Model.Structure;
using WerewolfOnline.Services;

namespace WerewolfOnline.Hubs {
    public class LobbyHub : Hub<ILobbyHub> {
        
        private readonly IDataService DataService;
        private readonly IHubContext<PlayerHub, IPlayerHub> _hubContext;
        public LobbyHub(IDataService dataService, IHubContext<PlayerHub, IPlayerHub> hubContext) {
            _hubContext = hubContext;
            DataService = dataService;
        }
        public async Task MessageSent(string message) {
            Lobby lobby = DataService.Lobby;
            if (!DataService.LobbyExists) {
                return;
            }
            if (lobby.Players.Exists(x => x.Id == Context.ConnectionId) && lobby.accepting) {
                Player player = lobby.Players.Find(x => x.Id == Context.ConnectionId);
                message = player.Name.Trim() + ": " + message;
                lobby.messages.Add(message);
                foreach (Player p in lobby.Players) {
                    await Clients.Client(p.Id).MessageReceived(message);
                }
                await Clients.Client(lobby.Host).MessageReceived(message);
                
                Console.WriteLine(message);
            }
        }
        public async Task<bool> Register(string username) {
            GameManager gm = DataService.Game;
            if (!DataService.GameExists) {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(username) && gm.accepting && !gm.Players.Exists(x => x.Name == username)) {
                string message = username + " has joined!";
                Player player = new Player(Context.ConnectionId, username);
                gm.Players.Add(player);
                gm.messages.Add(message);
                foreach (Player p in gm.Players) {
                    await Clients.Client(p.Id).MessageReceived(message);
                }
                await Clients.Client(gm.Host).MessageReceived(message);
                await Clients.Client(gm.Host).PlayerJoined(username);

                Console.WriteLine(message);
                return true;
            }
            return false;
        }
        public bool Relog(string username) {
            GameManager gm = DataService.Game;
            if (!DataService.GameExists) {
                return false;
            }
            Player p = gm.Players.FirstOrDefault(x => x.Name == username);
            if (!p.Equals(null)) {
                p.Id = Context.ConnectionId;
                Console.WriteLine(username + " relogged.");
                return true;
            }
            return false;
        }

        public bool CreateGame(string GameName) {
            if (string.IsNullOrWhiteSpace(GameName) || DataService.GameExists) {
                return false;
            }
            GameManager gm = new GameManager {
                Host = Context.ConnectionId,
                Name = GameName,
                _hubContext = _hubContext
            };
            DataService.Game = gm;
            DataService.GameExists = true;
            return true;
        }

        public async Task<bool> StartGame(string username, List<RoleName> roles, Dictionary<string, RoleName> assigned) {
            Console.WriteLine("start game runs");
            foreach (var r in roles) {
                Console.WriteLine(Enum.GetName(typeof(RoleName), r));
            }
            foreach (string key in assigned.Keys) {
                Console.WriteLine(key + " : " + Enum.GetName(typeof(RoleName), assigned[key]));
            }
            Console.WriteLine("parameters pass");
            GameManager gm = DataService.Game;
            GameOptions options = new GameOptions() {
                Roles = roles,
                AssignedRoles = assigned
            };

            if (gm.Equals(null)) {
                return false;
            }
            if (!await Register(username)) {
                return false;
            };
            gm.accepting = false;
            gm.Options = options;
            return true;
        }
        public void HostJoin(string username) {
            GameManager gm = DataService.Game;
            gm.Players.First(x => x.Name == username).Id = Context.ConnectionId;
            gm.gs = new GameState(gm.Players);
            gm.Init();
            Console.WriteLine("game started hub");
            foreach (Player p in gm.Players) {
                Clients.Client(p.Id).GameStarted(p.Role.Name);
            }
        }
        public List<string> MessageListById() {
            GameManager gm = DataService.Game;
            return gm.messages;
        }


    }

    public static class LobbyHubMethod {
        public static readonly string Register = "Register";
        public static readonly string MessageListById = "MessageListById";
        public static readonly string MessageSent = "MessageSent";
        public static readonly string CreateGame = "CreateGame";
        public static readonly string StartGame = "StartGame";
        public static readonly string HostJoin = "HostJoin";
    }
    public static class LobbyClientMethod {
        public static readonly string MessageReceived = "MessageReceived";
        public static readonly string PlayerJoined = "PlayerJoined";
        public static readonly string GameStarted = "GameStarted";

    }
    public interface ILobbyHub {
        Task MessageReceived(string message);
        Task PlayerJoined(string player);
        Task GameStarted(RoleName role);
    }
}