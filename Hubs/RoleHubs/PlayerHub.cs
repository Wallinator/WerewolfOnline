using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WerewolfOnline.Model;
using WerewolfOnline.Services;

namespace WerewolfOnline.Hubs {
    public partial class PlayerHub : Hub<IPlayerHub> {
            protected DataService DataService;
        public PlayerHub(DataService dataService) {
            DataService = dataService;
        }
        public bool Relog(string username, RoleName role) {
            GameManager gm = DataService.Game;
            if (gm.Equals(null)) {
                return false;
            }
            Player p = gm.Players.FirstOrDefault(x => x.Name.Equals(username) && x.Role.Name.Equals(role));
            if (!p.Equals(null)) {
                p.Id = Context.ConnectionId;
                Console.WriteLine("from relog: " + p.Id);
                Console.WriteLine(username + " relogged.");
                if (++gm.Relogs == gm.Players.Count) {
                    gm.StateCheck();
                };
                return true;
            }
            return false;
        }
        public void ReceiveVote(string vote, PollType type) {
            GameManager gm = DataService.Game;
            Player p = gm.Players.FirstOrDefault(x => x.Id.Equals(Context.ConnectionId));
            if (p.Equals(null)) {
                return;
            }
            gm.Vote(p, vote, type);
        }
 }

    public static partial class PlayerHubMethod {
        public static readonly string Relog = "Relog";
        public static readonly string ReceiveVote = "ReceiveVote";
    }
    public static partial class PlayerClientMethod {
        public static readonly string ReceiveEvent = "ReceiveEvent";
        public static readonly string ReceivePoll = "ReceivePoll";
        public static readonly string PollClosed = "PollClosed";
    }
    public partial interface IPlayerHub {
        Task ReceiveEvent(string Event);
        Task ReceivePoll(PollType type, List<string> nominees, int msTimer);
        Task PollClosed();
    }
}