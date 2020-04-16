using System;
using System.Threading;
namespace PhaseLibrary {
	public abstract class Phase {
		private PhaseFactory PhFactory;
		private Action<Phase> SetPhase;
		private Timer phaseTimer;
		private int Duration;
		private bool IsSetup = false;

		protected abstract int DefaultDuration { get; }

		protected Phase(PhaseFactory factory, Action<Phase> setPhaseAction) {
			PhFactory = factory;
			SetPhase = setPhaseAction;
			Duration = DefaultDuration;
		}

		public void StateHasChanged() {
			if (!IsSetup) {
				SetUp();
			}
			if (CanResolve()) {
				phaseTimer.Dispose();
				ResolveAndSetPhase();
			}
		}
		private void SetUp() {
			TimerSetUp();
			PhaseSetUp();
			IsSetup = true;
		}
		private void TimerSetUp() {
			if (Duration == 0) {
				Duration = Timeout.Infinite;
			}
			phaseTimer = new Timer(ForceResolve, new AutoResetEvent(true), Duration, Timeout.Infinite);
		}

		private void ForceResolve(object state) {
			phaseTimer.Dispose();
			PreForceResolve();
			ResolveAndSetPhase();
		}
		private void ResolveAndSetPhase() {
			PhaseResolve();
			SetPhase(PhFactory.MakeNextPhase(this, SetPhase));
		}

		protected abstract void PhaseSetUp();
		protected abstract bool CanResolve();
		protected abstract void PhaseResolve();
		protected abstract void PreForceResolve();
	}
}
