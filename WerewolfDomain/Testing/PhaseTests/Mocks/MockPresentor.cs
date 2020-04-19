using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class MockPresentor : Presentor {


		public Poll<string> PollHidden { get; set; } = null;
		public void HidePoll(Poll<string> poll) {
			PollHidden = poll;
		}

		public Poll<string> PollShown { get; set; } = null;
		public void ShowPoll(Poll<string> poll) {
			PollShown = poll;
		}
	}
}
