using System;
using System.Collections.Generic;
using System.Text;

namespace WerewolfDomain.Structures.GameEvents {
	public class JuryExecutionEvent : GameEvent {
		public string VictimName;

		public JuryExecutionEvent(List<Player> players, string victimName) : base(players) {
			VictimName = victimName;
		}

		public override EventType Type => EventType.JuryExecution;

	}
}
