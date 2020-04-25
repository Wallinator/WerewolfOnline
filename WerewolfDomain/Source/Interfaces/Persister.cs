using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces {
	public interface Persister {
		List<Player> GetLivingPlayers();
		void AddPoll(Poll poll);
		Poll GetPoll(PollType type);
		Phase GetNextPhase(PhaseType currentPhaseType);
		bool NextPhaseExists();
		void RemovePoll(PollType type);
	}
}
