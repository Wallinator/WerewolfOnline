using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerewolfOnline.Model.Phases {
	public class WerewolfPhase : AbstractPhase {
		Poll poll;

		public WerewolfPhase(GameManager gameManager, AbstractPhase nextPhase) : base(gameManager, nextPhase) {
		}

		public WerewolfPhase(GameManager gameManager, PhaseType nextPhaseType) : base(gameManager, nextPhaseType) {
		}

		protected override int DefaultDuration => throw new NotImplementedException();

		protected override bool CanResolve() {
			return gm.gs.Complete.Contains(poll);
		}

		protected override void Resolve() {

		}

		public override void SetUp() {
			List<string> options = new List<string>(gm.gs.Alive
				.Except(gm.gs.PlayersByRole[RoleName.Werewolf])
				.Select(x => x.Name)
				);
			poll = new Poll(gm.gs.PlayersByRole[RoleName.Werewolf], options, PollType.Werewolf);
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
