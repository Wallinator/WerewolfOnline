using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class PresentorMock : Presentor {

		public Poll PollHidden { get; set; } = null;
		public void HidePoll(Poll poll) {
			PollHidden = poll;
		}

		public Poll PollShown { get; set; } = null;
		public void ShowPoll(Poll poll) {
			PollShown = poll;
		}

		public Player SeerShownRole { get; set; } = null;
		public string NameOfPlayerShownToSeer { get; set; } = null;
		public void ShowSeerPlayerRole(Player seer, string playerName) {
			SeerShownRole = seer;
			NameOfPlayerShownToSeer = playerName;
		}
	}
}
