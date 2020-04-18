using PhaseLibrary;
using System;
using WerewolfDomain.Interfaces;

namespace WerewolfDomain.Phases {
	internal class WerewolfPhase : AbstractPhase {
		public WerewolfPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 30;

		internal override PhaseType PhaseType => PhaseType.Werewolf;

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
	}
}
