using WerewolfDomain.Entities;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces {
	public interface Presentor {
		void ShowPoll(Poll poll);
		void HidePoll(Poll poll);
		void ShowSeerPlayerRole(Player seer, string playerName);
	}
}
