using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerewolfDomain.Phases {
	public class VoteResultPhase : AbstractPhase {
		Poll poll;

		public VoteResultPhase(GameManager gameManager, AbstractPhase nextPhase) : base(gameManager, nextPhase) {
		}

		public VoteResultPhase(GameManager gameManager, PhaseType nextPhaseType) : base(gameManager, nextPhaseType) {
		}

		protected override int DefaultDuration => throw new NotImplementedException();

		protected override bool CanResolve() {
			return gm.gs.Complete.Contains(poll);
		}

		protected override void Resolve() {
			gm.gs.Pending.Clear();
			gm.gs.Complete.Clear();
			gm.gs.Resolved.Clear();
		}

		public override void SetUp() {
			poll = new Poll(gm.gs.Alive, new List<string>() { "Ready" }, PollType.Ready);
			gm.AddPoll(poll);
			IsSetup = true;
		}

		protected override void PreForceResolve() {

		}
	}
}
