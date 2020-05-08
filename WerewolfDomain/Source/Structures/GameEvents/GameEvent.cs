using System;
using System.Collections.Generic;
using System.Text;

namespace WerewolfDomain.Structures.GameEvents {
	public abstract class GameEvent {

		public abstract EventType Type {
			get;
		}
		public IReadOnlyList<Player> Players;
		public GameEvent(List<Player> players) {
			Players = players;
		}
	}
	public enum EventType {
		WerewolfKill,
		SeerReveal,
		JuryExecution
	}
}
