using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class SeerPhase : PollPhase {
		public SeerPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 30;

		internal override PhaseType PhaseType => PhaseType.Seer;

		protected override List<Poll> ConstructPolls() {
			throw new System.NotImplementedException();
		}

		protected override List<Poll> GetMyPolls() {
			throw new System.NotImplementedException();
		}
	}
}