using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces {
	public interface Persister {

		List<Player> GetAllPlayers();
		void UpdatePlayer(Player player);
		void AddPoll(Poll poll);
		void PlaceVote(Player player, object choice, PollType type);
		Poll GetPoll(PollType type);
		Phase GetNextPhase(PhaseType currentPhaseType);
		bool NextPhaseExists();
		void RemovePoll(PollType type);
		bool IsPhaseSetup();
		void SetPhaseSetup(bool IsSetup);
	}
}
