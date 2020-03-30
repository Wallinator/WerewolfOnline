using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WerewolfOnline.Model.Phases {
	public class IntroductionPhase : AbstractPhase {
		private Poll poll;

		protected override int DefaultDuration => throw new NotImplementedException();

		public IntroductionPhase(GameManager gameManager, AbstractPhase nextPhase, int durationSeconds = 0) : base(gameManager, nextPhase) {
		}

		public IntroductionPhase(GameManager gameManager, PhaseType nextPhaseType, int durationSeconds = 0) : base(gameManager, nextPhaseType) {
		}

		protected override bool CanResolve() {
			return gm.gs.Complete.Contains(poll);
		}

		protected override void Resolve() {
			gm.gs.Complete.Remove(poll);
			gm.gs.Resolved.Add(poll);
		}

		public override void SetUp() {               
			poll = new Poll(gm.gs.Alive, new List<string>() { "Ready" }, PollType.Ready);
			gm.AddPoll(poll);
			IsSetup = true;
		}

		protected override void PreForceResolve() {
			if (gm.gs.Pending.Contains(poll)) {
				gm.ForceClose(poll);
			}
		}
	}
}
