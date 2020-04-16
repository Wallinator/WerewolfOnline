using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerewolfDomain.Phases {
	public class VotePhase : AbstractPhase {
		Poll poll;

		protected override int DefaultDuration => throw new NotImplementedException();

		public VotePhase(GameManager gameManager, AbstractPhase nextPhase) : base(gameManager, nextPhase) {
		}

		public VotePhase(GameManager gameManager, PhaseType nextPhaseType) : base(gameManager, nextPhaseType) {
		}

		protected override bool CanResolve() {
			return gm.gs.Complete.Contains(poll);
		}

		protected override void Resolve() {
			gm.gs.Complete.Remove(poll);
			gm.gs.Resolved.Add(poll);
		}

		public override void SetUp() {
			List<string> options = new List<string>(gm.gs.Alive
				.Select(x => x.Name)
				);
			poll = new Poll(gm.gs.Alive, options, PollType.Villager);
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
