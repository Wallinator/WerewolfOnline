using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class PresentorMock : Presenter {

		public bool PollHidden { get; set; } = false;
		void Presenter.HidePolls() {
			PollHidden = true;
		}

		public Poll PollShown { get; set; } = null;
		void Presenter.ShowPoll(Poll poll) {
			PollHidden = false;
			PollShown = poll;
		}

		public List<GameEvent> visibleEvents = new List<GameEvent>();

		public IEnumerable<string> GetSleepPollOptions() {
			return new List<string> { "Ready To Sleep" };
		}

		void Presenter.ShowEvent(GameEvent gameEvent) {
			visibleEvents.Add(gameEvent);
		}

		public IEnumerable<string> GetDiscussionPollOptions() {
			return new List<string> { "Ready To Vote" };
		}

		public IEnumerable<string> GetIntroductionPollOptions() {
			return new List<string> { "Ready" };
		}
	}
}
