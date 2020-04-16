using PhaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WerewolfDomain.Interfaces;

namespace WerewolfDomain.Phases {
	internal class DiscussionPhase : PresentingPhase {

		public DiscussionPhase(PhaseFactory factory, Action<Phase> setPhaseAction, Presentor presentor) : base(factory, setPhaseAction, presentor) {
		}

		protected override int DefaultDuration => 0;

		protected override bool CanResolve() {
			throw new NotImplementedException();
		}

		protected override void PhaseResolve() {
			throw new NotImplementedException();
		}

		protected override void PhaseSetUp() {
			throw new NotImplementedException();
		}

		protected override void PreForceResolve() {
			throw new NotImplementedException();
		}
	}
}
