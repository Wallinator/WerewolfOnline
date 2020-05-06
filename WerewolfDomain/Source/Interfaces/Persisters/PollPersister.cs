using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces.Persisters {
	public interface PollPersister {
		void AddPoll(Poll poll);
		Poll GetPoll(PollType type);
		void PlaceVote(Player player, object choice, PollType type);
		void RemovePoll(PollType type);
	}
}