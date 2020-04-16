using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WerewolfOnline.Hubs;
using WerewolfOnline.Model;
using WerewolfOnline.Model.Phases;
using WerewolfOnline.Model.Structure;
using WerewolfOnline.Services;

namespace WerewolfOnline.Model {
	public class GameManager {
		public CancellationTokenSource TimerSource;
		public int Relogs = 0;
		public IHubContext<PlayerHub, IPlayerHub> _hubContext;
		public List<string> messages = new List<string>();
		public bool accepting = true;
		public string Host;
		public string Name;
		public List<Player> Players = new List<Player>();
		public GameState gs;
		public GameOptions Options;
		private readonly Random r = new Random();

		public void Init() {
			gs.Options = Options;
			AbstractPhase.PhaseDictInit(gs);
			List<Player> unassignedPlayers = new List<Player>(Players);
			foreach (Player p in Players) {
				RoleName type = Options.AssignedRoles[p.Name];
				if (type == RoleName.None) {
					unassignedPlayers.Add(p);
				}
				else {
					p.SetRole(type);
					Options.Roles.Remove(type);
				}
			}
			int playerIndex = 0;
			while (Options.Roles.Count > 0) {
				int rand = r.Next(Options.Roles.Count);
				Player p = unassignedPlayers[playerIndex++];
				RoleName type = Options.Roles[rand];
				p.SetRole(type);
				gs.PlayersByRole[type].Add(p);
				Options.Roles.RemoveAt(rand);
			}
			gs.RoundNum = 1;
			gs.Phase = new IntroductionPhase(this, gs.Options.NextPhaseDict[PhaseType.Introduction]);
		}

		public void StateCheck() {
			if (!gs.Phase.IsSetup) {
				gs.Phase.SetUp();
				TimerSource = TimerService.StartTimer(gs.Phase.MsTimer, ()=> { }, gs.Phase.ForceResolve);
			}
			if (gs.Phase.ResolveIfAble()) {
				StateCheck();
			}
		}

		public void AddPoll(Poll poll) {
			gs.Pending.Add(poll);
			foreach (Player p in poll.Voters) {
				Console.WriteLine("from server: " + p.Id);
				_ = _hubContext.Clients.Client(p.Id).ReceivePoll(poll.Type, poll.Nominees, gs.Phase.MsTimer);
			}
		}
		public void ForceClose(Poll poll) {
			gs.Pending.Remove(poll);
			gs.Complete.Add(poll);
			poll.ClosePoll();
			foreach (Player p in poll.Voters) {
				_ = _hubContext.Clients.Client(p.Id).PollClosed();
			}
		}

		public void Vote(Player voter, string vote, PollType poll) {
			Poll p = gs.Pending.FirstOrDefault(x => x.Type == poll);
			if (p.Equals(null) || p.Closed) { return; }
			p.Vote(voter, vote);
			if (p.Closed) {
				gs.Pending.Remove(p);
				gs.Complete.Add(p);
			}
			StateCheck();
		}

		public void Kill(Player p, RoleName method) {
			gs.Alive.Remove(p);
			gs.PlayersByRole[p.Role.Name].Remove(p);
			p.Role = new Spectator(p.Role);
			gs.PlayersByRole[p.Role.Name].Add(p);
		}

		

	}
}