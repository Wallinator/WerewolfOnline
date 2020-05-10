using System.Collections.Generic;
using WerewolfDomain.Roles;

namespace WerewolfDomain.Structures.GameEvents {
	public class SeerRevealEvent : GameEvent {
		public string RevealedName;
		public RoleName RevealedRole;
		public SeerRevealEvent(List<Player> players, string name, RoleName role) : base(players) {
			RevealedName = name;
			RevealedRole = role;
		}

		public override EventType Type => EventType.SeerReveal;
	}
}
