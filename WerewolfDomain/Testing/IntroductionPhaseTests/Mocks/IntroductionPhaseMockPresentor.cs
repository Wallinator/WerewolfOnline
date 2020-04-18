using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.IntroductionPhaseTests.Mocks {
	internal class IntroductionPhaseMockPresentor : Presentor {
		public bool ShowPollCalled { get; internal set; } = false;
		public bool HidePollCalled { get; internal set; } = false;


		public void HidePoll(Poll<string> poll) {
			HidePollCalled = true;
		}

		public void ShowPoll(Poll<string> poll) {
			ShowPollCalled = true;
		}
	}
}
