using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerewolfDomain.Play;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces {
	public interface Persistor {
		List<Player> GetLivingPlayers();
		void AddPoll(Poll poll);
		Poll GetPoll(PollType ready);
	}
}
