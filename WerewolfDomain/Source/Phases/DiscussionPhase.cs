﻿using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class DiscussionPhase : PollPhase {
		public DiscussionPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 0;

		internal override PhaseType PhaseType => PhaseType.Discussion;

		protected override List<Poll> ConstructPolls() {
			throw new System.NotImplementedException();
		}

		protected override List<Poll> GetMyPolls() {
			throw new System.NotImplementedException();
		}
	}
}