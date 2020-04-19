using PhaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal abstract class PollPhase : AbstractPhase {
		protected abstract List<Poll<object>> Polls { get; }
		protected PollPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		protected override bool CanResolve() {
			return false;
		}

		protected override void ConcreteResolve() {
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
