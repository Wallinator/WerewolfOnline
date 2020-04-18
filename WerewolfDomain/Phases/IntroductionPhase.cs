using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class IntroductionPhase : AbstractPhase {
		public IntroductionPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 0;

		internal override PhaseType PhaseType => PhaseType.Introduction;

		protected override bool CanResolve() {
			Poll<string> poll = persistor.GetPoll(PollType.Ready);
			return poll.Closed;
		}

		protected override void ConcreteResolve() {
			persistor.RemovePoll(PollType.Ready);
			Poll<string> poll = persistor.GetPoll(PollType.Ready);
			presentor.HidePoll(poll);
		}

		protected override void PhaseSetUp() {
			var players = persistor.GetLivingPlayers();
			Poll<string> poll = new Poll<string>(players, new List<string> { "Ready" }, PollType.Ready);
			persistor.AddPoll(poll);
			presentor.ShowPoll(poll);
		}

		protected override void PreForceResolve() {
			throw new NotImplementedException();
		}
	}
}
