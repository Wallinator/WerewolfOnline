using PhaseLibrary;
using WerewolfDomain.Interfaces;

namespace WerewolfDomain.Phases {
	public abstract class AbstractPhase : Phase {

		internal bool Resolved = false;
		protected readonly Persistor persistor;
		protected readonly Presentor presentor;
		internal abstract PhaseType PhaseType { get; }


		protected AbstractPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory) {
			this.persistor = persistor;
			this.presentor = presentor;
		}
		protected sealed override void PhaseResolve() {
			ConcreteResolve();
			Resolved = true;
		}
		protected abstract void ConcreteResolve();

	}
}
