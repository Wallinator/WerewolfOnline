using PhaseLibrary;
using System;

namespace PhaseTests.Mocks {
	class PhaseFactoryTestImpl : PhaseFactory {

		public override Phase MakeFirstPhase() {
			throw new NotImplementedException();
		}

		protected override Phase MakeNextPhase(Phase phase) {
			return new PhaseTestImpl(this, ((PhaseTestImpl) phase).Id + 1, true);
		}
	}
}
