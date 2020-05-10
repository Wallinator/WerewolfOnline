using System.Collections.Generic;

namespace WerewolfDomain.Structures.GameEvents {
	public class WerewolfKillEvent : GameEvent {
		public string VictimName;
		public WerewolfKillEvent(List<Player> players, string victimName) : base(players) {
			VictimName = victimName;
		}

		public override EventType Type => EventType.WerewolfKill;
	}
}
