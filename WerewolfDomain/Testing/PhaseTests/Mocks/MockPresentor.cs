using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class MockPresentor : Presentor {


		public Poll PollHidden { get; set; } = null;
		public void HidePoll(Poll poll) {
			PollHidden = poll;
		}

		public Poll PollShown { get; set; } = null;
		public void ShowPoll(Poll poll) {
			PollShown = poll;
		}
	}
}
