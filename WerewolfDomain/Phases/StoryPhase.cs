using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerewolfDomain.Phases {
	public class StoryPhase : AbstractPhase {
		public StoryPhase(GameManager gameManager, AbstractPhase nextPhase) : base(gameManager, nextPhase) {
		}

		public StoryPhase(GameManager gameManager, PhaseType nextPhaseType) : base(gameManager, nextPhaseType) {
		}

		protected override int DefaultDuration => throw new NotImplementedException();

		public override void SetUp() {
			throw new NotImplementedException();
		}

		protected override bool CanResolve() {
			throw new NotImplementedException();
		}

		protected override void PreForceResolve() {
			throw new NotImplementedException();
		}

		protected override void Resolve() {
			throw new NotImplementedException();
		}
	}
}
