using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces {
	public interface Persistor {
		List<Player> GetLivingPlayers();
		void AddPoll(Poll poll);
		Poll GetPoll(PollType type);
		Phase GetNextPhase();
		bool NextPhaseExists();
		void RemovePoll(PollType type);
		void SetNextPhase(Phase phase);
	}
}
