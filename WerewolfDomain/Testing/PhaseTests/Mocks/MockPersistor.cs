using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class MockPersistor : Persistor {
		public Poll<string> PollAdded { get; set; } = null;

		public void AddPoll(Poll<string> poll) {
			PollAdded = poll;
		}

		public List<Player> LivingPlayers { get; set; } = null;
		public List<Player> GetLivingPlayers() {
			return LivingPlayers;
		}


		public Phase GetNextPhase() {
			throw new NotImplementedException();
		}

		public Poll<string> PollToBeGot { get; set; } = null;
		public Poll<string> GetPoll(PollType type) {
			return PollToBeGot;
		}

		public bool NextPhaseExists() {
			return false;
		}

		public PollType? PollTypeRemoved { get; set; } = null;
		public void RemovePoll(PollType type) {
			PollTypeRemoved = type;
		}
	}
}
