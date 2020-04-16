using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerewolfDomain.Structure;

namespace WerewolfDomain.Interfaces {
	interface Persistor {
		List<Player> GetLivingPlayers();
		void AddPoll(Poll poll);
		Poll GetPoll(PollType ready);
	}
}
