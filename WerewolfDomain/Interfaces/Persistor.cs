using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces {
	public interface Persistor {
		List<Player> GetLivingPlayers();
		void AddPoll(Poll<string> poll);
		Poll<string> GetPoll(PollType type);
		Phase GetNextPhase();
		bool NextPhaseExists();
		void RemovePoll(PollType type);
	}
}
