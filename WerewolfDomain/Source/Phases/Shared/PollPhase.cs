using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases.Shared {
	internal abstract class PollPhase : AbstractPhase {
		protected abstract List<Poll> GetMyPolls();
		protected PollPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		protected override bool CanResolve() {
			return GetMyPolls().TrueForAll(poll => poll.Closed);
		}

		protected override void ConcreteResolve() {
			GetMyPolls().ForEach((poll) => {
				PollResolver.Resolve(poll, persistor, presentor);
			});
		}

		protected override void PhaseSetUp() {
			ConstructPolls().ForEach(poll => {
				persistor.AddPoll(poll);
				presentor.ShowPoll(poll);
			});
		}

		protected abstract List<Poll> ConstructPolls();

		protected override void PreForceResolve() {
			GetMyPolls().ForEach(poll => poll.ClosePoll());
		}
	}
}
