using PhaseLibrary;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;

namespace WerewolfDomain.Phases.Shared {
	internal abstract class AbstractPhase : Phase {


		protected readonly Persister persistor;
		protected readonly Presentor presentor;
		internal abstract PhaseType PhaseType {
			get;
		}
		protected override bool IsSetup {
			get => persistor.IsPhaseSetup();
			set => persistor.SetPhaseSetup(value);
		}

		protected AbstractPhase(PhaseFactory factory, Persister persistor, Presentor presentor) : base(factory) {
			this.persistor = persistor;
			this.presentor = presentor;
		}
	}
}
