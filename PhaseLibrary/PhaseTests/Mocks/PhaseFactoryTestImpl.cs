using PhaseLibrary;
using System;

namespace PhaseTests.Mocks {
	class PhaseFactoryTestImpl : PhaseFactory {

		public override Phase MakeFirstPhase(Action<Phase> SetPhase) {
			throw new NotImplementedException();
		}

		protected override Phase MakePhase(Phase phase, Action<Phase> SetPhase) {
			return new PhaseTestImpl(this, SetPhase, 0, ((PhaseTestImpl) phase).Id + 1, true);
		}
	}
}
