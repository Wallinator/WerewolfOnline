using PhaseLibrary;
using System;
using WerewolfDomain.Interfaces;

namespace WerewolfDomain.Phases {
	internal class StoryPhase : AbstractPhase {
		public StoryPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		protected override bool CanResolve() {
			throw new NotImplementedException();
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

		internal override PhaseType PhaseType => throw new NotImplementedException();

		public override int DefaultDurationSeconds => 0;
	}
}
