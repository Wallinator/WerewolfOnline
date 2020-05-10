using System.Collections.Generic;

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
