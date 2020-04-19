using PhaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal abstract class PollPhase : AbstractPhase {
		protected abstract List<Poll> GetPolls();
		protected PollPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		protected override bool CanResolve() {
			return GetPolls().TrueForAll(poll => poll.Closed);
		}

		protected override void ConcreteResolve() {
			GetPolls().ForEach(poll => Resolver.ResolvePoll(poll, persistor, presentor));
		}

		protected override void PhaseSetUp() {
			ConstructPolls().ForEach(poll => {
				persistor.AddPoll(poll);
				presentor.ShowPoll(poll);
			});
		}

		protected abstract List<Poll> ConstructPolls();

		protected override void PreForceResolve() {
			GetPolls().ForEach(poll => poll.ClosePoll());
		}
	}
}
