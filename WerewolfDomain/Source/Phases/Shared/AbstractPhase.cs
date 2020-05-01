using PhaseLibrary;
using WerewolfDomain.Interfaces;

namespace WerewolfDomain.Phases.Shared {
	internal abstract class AbstractPhase : Phase {


		protected readonly Persister persistor;
		protected readonly Presentor presentor;
		internal abstract PhaseType PhaseType {
			get;
		}


		protected AbstractPhase(PhaseFactory factory, Persister persistor, Presentor presentor) : base(factory) {
			this.persistor = persistor;
			this.presentor = presentor;
		}
		public sealed override void SetUp() {
			if (!persistor.IsPhaseSetup()) {
				PhaseSetUp();
				persistor.SetPhaseSetup(true);
			}
		}
	}
}
