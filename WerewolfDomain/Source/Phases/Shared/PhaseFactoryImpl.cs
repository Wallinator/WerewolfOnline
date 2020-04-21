using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Exceptions;

namespace WerewolfDomain.Phases.Shared {
	public class PhaseFactoryImpl : PhaseFactory {
		private readonly Persistor persistor;
		private readonly Presentor presentor;
		private readonly Dictionary<PhaseType, bool> PhaseExists;

		public PhaseFactoryImpl(Persistor persistor, Presentor presentor, Dictionary<PhaseType, bool> phaseExists) {
			this.persistor = persistor;
			this.presentor = presentor;
			PhaseExists = phaseExists;
		}

		public override Phase MakeFirstPhase() {
			return NewPhase(PhaseType.Introduction);
		}

		public override Phase MakeNextPhase(Phase phase) {
			AbstractPhase abstractPhase = (AbstractPhase)phase;
			PhaseType currentPhaseType;
			if (phase is InterruptingPhase) {
				currentPhaseType = ((InterruptingPhase)phase).PhaseInterrupted;
			}
			else {
				currentPhaseType = abstractPhase.PhaseType;
			}

			if (persistor.NextPhaseExists()) {
				return persistor.GetNextPhase(currentPhaseType);
			}
			return NewPhase(abstractPhase.PhaseType + 1);
		}

		private Phase NewPhase(PhaseType phaseType) {
			if (PhaseExists[phaseType]) {
				return ConstructPhase(phaseType);
			}
			else {
				return NewPhase(++phaseType);
			}
		}

		private Phase ConstructPhase(PhaseType phaseType) {
			return phaseType switch
			{
				PhaseType.Introduction => new IntroductionPhase(this, persistor, presentor),
				PhaseType.Werewolf => new WerewolfPhase(this, persistor, presentor),
				PhaseType.Seer => new SeerPhase(this, persistor, presentor),
				PhaseType.Story => new StoryPhase(this, persistor, presentor),
				PhaseType.Discussion => new DiscussionPhase(this, persistor, presentor),
				PhaseType.Jury => throw new NotImplementedException(),
				PhaseType.Execution => throw new NotImplementedException(),
				PhaseType.Wrapper => new WerewolfPhase(this, persistor, presentor),
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
		Execution,
		//wrapper must be on button
		Wrapper
	}
}