using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class PresentorMock : Presenter {

		public List<Player> PlayersShownPoll = new List<Player>();
		void Presenter.HidePolls() {
			PollShown = false;
			PlayersShownPoll.Clear();
		}

		public bool PollShown { get; set; } = false;

		void Presenter.ShowPoll(List<Player> players, List<string> options) {
			PlayersShownPoll.AddRange(players);
			PollShown = true;
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
