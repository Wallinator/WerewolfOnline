using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using WerewolfDomain.Play;
using WerewolfOnline.Model.Phases;

namespace WerewolfDomain.Structures {
	public class GameState {
		public List<Player> Players;
		public List<Player> Alive;
		public Dictionary<RoleName, List<Player>> PlayersByRole = new Dictionary<RoleName, List<Player>>();
		public List<Poll> Pending = new List<Poll>();
		public List<Poll> Complete = new List<Poll>();
		public List<Poll> Resolved = new List<Poll>();
		public int RoundNum;
		public AbstractPhase Phase;
		public GameOptions Options;
		public GameState(List<Player> players) {
			Players = players;
			Alive = new List<Player>(players);
			foreach (RoleName r in Enum.GetValues(typeof(RoleName))) {
				PlayersByRole[r] = new List<Player>();
			}
		}
	}

	public class GameOptions {
		public List<RoleName> Roles = new List<RoleName>();
		public Dictionary<string, RoleName> AssignedRoles = new Dictionary<string, RoleName>();
		public Dictionary<PhaseType, PhaseType> NextPhaseDict = new Dictionary<PhaseType, PhaseType>();
	}
}
