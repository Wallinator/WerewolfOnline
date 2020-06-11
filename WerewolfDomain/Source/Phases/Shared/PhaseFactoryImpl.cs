using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Exceptions;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;

namespace WerewolfDomain.Phases.Shared {
	public class PhaseFactoryImpl : PhaseFactory {
		private readonly Persister persistor;
		private readonly Presenter presentor;
		private readonly Dictionary<PhaseType, bool> PhaseExists;

		public PhaseFactoryImpl(Persister persistor, Presenter presentor, Dictionary<PhaseType, bool> phaseExists) {
			this.persistor = persistor;
			this.presentor = presentor;
			PhaseExists = phaseExists;
		}

		public override Phase MakeFirstPhase() {
			return NewPhase(PhaseType.Introduction);
		}

		public override Phase MakeNextPhase(Phase phase) {
			if (persistor.InterruptingPhaseTypeExists()) {
				return ConstructPhase(persistor.PopInterruptingPhaseType());
			}
			PhaseType currentPhaseType = persistor.GetLastOrderedPhaseType();
			return NewPhase(currentPhaseType + 1);
		}

		private Phase NewPhase(PhaseType phaseType) {
			if (PhaseExists[phaseType]) {
				persistor.SetLastOrderedPhaseType(phaseType);
				return ConstructPhase(phaseType);
			}
			else {
				return NewPhase(++phaseType);
			}
		}

		public Phase ConstructPhase(PhaseType phaseType) {
			persistor.SetPhaseSetup(false);
			persistor.SetCurrentPhaseType(phaseType);
			return phaseType switch
			{
				PhaseType.Introduction => new IntroductionPhase(this, persistor, presentor),
				PhaseType.Werewolf => new WerewolfPhase(this, persistor, presentor),
				PhaseType.Seer => new SeerPhase(this, persistor, presentor),
				PhaseType.Story => new StoryPhase(this, persistor, presentor),
				PhaseType.Discussion => new DiscussionPhase(this, persistor, presentor),
				PhaseType.Jury => new JuryPhase(this, persistor, presentor),
				PhaseType.Bedtime => new BedtimePhase(this, persistor, presentor),
				PhaseType.Wrapper => new WerewolfPhase(this, persistor, presentor),
				//wrapper should point to first phase in night
				_ => throw new InvalidPhaseTypeException(),
			};
		}
	}
	public enum PhaseType {
		//phases should be in order of resolution
		Introduction,
		Werewolf,
		Seer,
		Story,
		Discussion,
		Jury,
		Bedtime,
		Wrapper
		//wrapper must be on bottom of ordered phases

	}
}