using System.Collections.Generic;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces.Persisters {
	public interface PlayerPersister {
		List<Player> GetAllPlayers();
		void UpdatePlayer(Player player);
	}
}