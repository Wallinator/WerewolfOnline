using System;
using System.Collections.Generic;
using System.Text;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;

namespace WerewolfDomain.Helpers {
	internal static class PlayerKiller {
		internal static void Kill(Player player, EventType method, Persister persistor, Presenter presentor) {

			GameEvent gameEvent = method switch
			{
				EventType.WerewolfKill => MakeWerewolfKillEvent(player, persistor),
				EventType.SeerReveal => throw new NotImplementedException(),
				EventType.JuryExecution => MakeJuryExecutionEvent(player, persistor),
				_ => throw new NotImplementedException()
			};
			presentor.ShowEvent(gameEvent);
		}

		private static GameEvent MakeJuryExecutionEvent(Player player, Persister persistor) {
			player.Role = new Spectator(player.Role);
			return new JuryExecutionEvent(persistor.GetAllPlayers(), player.Name);
		}

		private static GameEvent MakeWerewolfKillEvent(Player player, Persister persistor) {
			player.Role = new Spectator(player.Role);
			return new WerewolfKillEvent(persistor.GetAllPlayers(), player.Name);
		}
	}
}
