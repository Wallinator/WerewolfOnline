using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;

namespace WerewolfDomain.Interfaces {
	public interface Presentor {
		void ShowPoll(Poll poll);
		void HidePoll(Poll poll);
		void ShowEvent(GameEvent gameEvent);
	}
}
