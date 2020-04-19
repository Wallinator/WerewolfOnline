using PhaseLibrary;
using WerewolfDomain.Interfaces;

namespace WerewolfDomain.Phases {
	public class PhaseFactoryImpl : PhaseFactory {
		private readonly Persistor persistor;
		private readonly Presentor presentor;

		public PhaseFactoryImpl(Persistor persistor, Presentor presentor) {
			this.persistor = persistor;
			this.presentor = presentor;
		}

		public override Phase MakeFirstPhase() {
			return new IntroductionPhase(this, persistor, presentor);
		}

		public override Phase MakeNextPhase(Phase phase) {
			if (persistor.NextPhaseExists()) {
				return persistor.GetNextPhase();
			}
			AbstractPhase abstractPhase = (AbstractPhase)phase;
			return NewPhase(abstractPhase.PhaseType);
		}

		private Phase NewPhase(PhaseType phaseType) {
			switch (phaseType) {
				case PhaseType.Introduction:
					return new WerewolfPhase(this, persistor, presentor);
				case PhaseType.Werewolf:

				case PhaseType.Seer:

				case PhaseType.Story:

				case PhaseType.Discussion:

				default:
					return null;
			}
		}
	}
	public enum PhaseType {
		Introduction,
		Werewolf,
		Seer,
		Story,
		Discussion
	}
}