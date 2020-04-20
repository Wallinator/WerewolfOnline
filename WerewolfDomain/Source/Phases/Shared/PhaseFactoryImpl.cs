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
			if (persistor.NextPhaseExists()) {
				return persistor.GetNextPhase();
			}
			AbstractPhase abstractPhase = (AbstractPhase)phase;
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
				PhaseType.Vote => throw new NotImplementedException(),
				PhaseType.VoteResults => throw new NotImplementedException(),
				PhaseType.Wrapper => new WerewolfPhase(this, persistor, presentor),
				_ => throw new InvalidPhaseTypeException(),
			};
		}
	}
	public enum PhaseType {
		//phases should be in order of execution
		Introduction,
		Werewolf,
		Seer,
		Story,
		Discussion,
		Vote,
		VoteResults,
		Wrapper
	}
}