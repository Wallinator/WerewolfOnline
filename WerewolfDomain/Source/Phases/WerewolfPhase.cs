using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class WerewolfPhase : PollPhase {
		public WerewolfPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 30;

		internal override PhaseType PhaseType => PhaseType.Werewolf;

		protected override List<Poll> ConstructPolls() {
			throw new NotImplementedException();
		}

		protected override List<Poll> GetPolls() {
			throw new NotImplementedException();
		}
	}
}
