using System;
using System.Threading;
namespace PhaseLibrary {
	public abstract class Phase {
		private PhaseFactory PhFactory;
		private bool IsSetup = false;
		public abstract int DefaultDurationSeconds { get; }


		protected Phase(PhaseFactory factory) {
			PhFactory = factory;
		}

		public Phase StateHasChanged() {
			if (!IsSetup) {
				SetUp();
			}
			if (CanResolve()) {
				Resolve();
				return PhFactory.MakeNextPhase(this);
			}
			return this;
		}
		private void SetUp() {
			PhaseSetUp();
			IsSetup = true;
		}

		public void ForceResolve() {
			PreForceResolve();
			Resolve();
		}
		private void Resolve() {
			PhaseResolve();
		}

		protected abstract void PhaseSetUp();
		protected abstract bool CanResolve();
		protected abstract void PhaseResolve();
		protected abstract void PreForceResolve();
	}
}
