using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Helpers;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases.Shared {
	internal abstract class PollPhase : AbstractPhase {

		private List<Poll> GetMyPolls() {
			List<Poll> polls = new List<Poll>();
			foreach (PollType type in PollTypes()) {
				polls.Add(persistor.GetPoll(type));
			}
			return polls;
		}
		protected abstract List<PollType> PollTypes();
		protected PollPhase(PhaseFactory factory, Persister persistor, Presenter presentor) : base(factory, persistor, presentor) {
		}

		protected override bool CanResolve() {
			return GetMyPolls().TrueForAll(poll => poll.Closed);
		}

		protected override void PhaseResolve() {
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
		protected override void CleanUp() {
			presentor.HidePolls();
		}
		protected abstract List<Poll> ConstructPolls();

		protected override void PreForceResolve() {
			GetMyPolls().ForEach(poll => {
				poll.ClosePoll();
			});
		}
	}
}
