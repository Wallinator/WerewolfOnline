﻿using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class JuryPhase : PollPhase {
		public JuryPhase(PhaseFactory factory, Persister persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => throw new System.NotImplementedException();

		internal override PhaseType PhaseType => throw new System.NotImplementedException();

		protected override List<Poll> ConstructPolls() {
			throw new System.NotImplementedException();
		}

		protected override List<PollType> PollTypes() {
			throw new System.NotImplementedException();
		}
	}
}