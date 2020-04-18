using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces {
	public interface Presentor {
		void ShowPoll(Poll<string> poll);
		void HidePoll(Poll<string> poll);
	}
}
