using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class MockPersistor : Persistor {

		public Poll PollAdded { get; set; } = null;
		public void AddPoll(Poll poll) {
			PollAdded = poll;
		}

		public List<Player> LivingPlayers { get; set; } = null;
		public List<Player> GetLivingPlayers() {
			return LivingPlayers;
		}


		public Phase GetNextPhase() {
			throw new NotImplementedException();
		}


		internal Dictionary<PhaseType, bool> AllPhasesExist() {
			Dictionary<PhaseType, bool> dict = new Dictionary<PhaseType, bool>();
			foreach (PhaseType type in Enum.GetValues(typeof(PhaseType))) {
				dict[type] = true;
			}
			return dict;
		}

		public Poll PollToBeGot { get; set; } = null;
		public Poll GetPoll(PollType type) {
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
