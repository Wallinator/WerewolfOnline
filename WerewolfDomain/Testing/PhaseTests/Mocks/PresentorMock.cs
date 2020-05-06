using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class PresentorMock : Presentor {

		public Poll PollHidden { get; set; } = null;
		void Presentor.HidePoll(Poll poll) {
			PollHidden = poll;
		}

		public Poll PollShown { get; set; } = null;
		void Presentor.ShowPoll(Poll poll) {
			PollShown = poll;
		}

		public List<GameEvent> visibleEvents = new List<GameEvent>();

		void Presentor.ShowEvent(GameEvent gameEvent) {
			visibleEvents.Add(gameEvent);
		}
	}
}
