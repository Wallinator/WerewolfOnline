using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class IntroductionPhase : PollPhase {
		public IntroductionPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 0;

		protected override List<Poll> GetPolls() {
			List<Poll> polls = new List<Poll> {
				persistor.GetPoll(PollType.Ready)
			};
			return polls;
		}

		internal override PhaseType PhaseType => PhaseType.Introduction;

		protected override List<Poll> ConstructPolls() {
			List<Entities.Player> players = persistor.GetLivingPlayers();
			List<Poll> polls = new List<Poll>() { 
				new Poll(players, new List<string> { "Ready" }, PollType.Ready)
			};
			return polls;
		}

		protected override void ConcreteResolve() {
			return;
		}
	}
}
