using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.IntroductionPhaseTests.Mocks {
	internal class IntroductionPhaseMockPersistor : Persistor {
		public bool AddPollCalled { get; internal set; }

		public void AddPoll(Poll<string> poll) {
			AddPollCalled = true;
		}

		public List<Player> GetLivingPlayers() {
			return new List<Player>();
		}

		public Phase GetNextPhase() {
			throw new NotImplementedException();
		}

		public Poll<string> PollToBeGot { get; set; }
		public Poll<string> GetPoll(PollType type) {
			return PollToBeGot;
		}

		public bool NextPhaseExists() {
			return false;
		}

		public bool RemovePollCalled { get; internal set; }
		public void RemovePoll(PollType type) {
			RemovePollCalled = true;
		}
	}
}
