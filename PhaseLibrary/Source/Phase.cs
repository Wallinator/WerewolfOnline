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

		public virtual Phase StateHasChanged() {
			if (CanResolve()) {
				Resolve();
				return NextPhase();
			}
			return this;
		}


		public virtual void SetUp() {
			if (!IsSetup) {
				PhaseSetUp();
				IsSetup = true;
			}
		}

		public virtual Phase ForceResolve() {
			PreForceResolve();
			Resolve();
			return NextPhase();
		}
		private void Resolve() {
			PhaseResolve();
		}
		protected Phase NextPhase() {
			return PhFactory.MakeNextPhase(this);
		}

		protected abstract void PhaseSetUp();
		protected abstract bool CanResolve();
		protected abstract void PhaseResolve();
		protected abstract void PreForceResolve();
	}
}
