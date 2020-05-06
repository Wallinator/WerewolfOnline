using System;
using System.Collections.Generic;
using System.Text;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;

namespace WerewolfDomain.Helpers {
	internal static class PlayerKiller {
		internal static void Kill(Player player, EventType method, Persister persistor, Presentor presentor) {

			GameEvent gameEvent = method switch
			{
				EventType.WerewolfKill => MakeWerewolfKillEvent(player, persistor),
				_ => throw new NotImplementedException()
			};
			presentor.ShowEvent(gameEvent);
		}

		private static GameEvent MakeWerewolfKillEvent(Player player, Persister persistor) {
			player.Role = new Spectator(player.Role);
			return new WerewolfKillEvent(persistor.GetAllPlayers(), player.Name);
		}
	}
}
