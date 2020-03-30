using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerewolfOnline.Model.Phases {
	public class DiscussionPhase : AbstractPhase {
		private readonly static int defaultDuration = 0;

		public DiscussionPhase(GameManager gameManager, AbstractPhase nextPhase) : base(gameManager, nextPhase) {
		}

		public DiscussionPhase(GameManager gameManager, PhaseType nextPhaseType) : base(gameManager, nextPhaseType) {
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
