using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases.Shared {
	internal class ExecutionPhase : PollPhase {
		public ExecutionPhase(PhaseFactory factory, Persister persistor, Presentor presentor) : base(factory, persistor, presentor) {
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