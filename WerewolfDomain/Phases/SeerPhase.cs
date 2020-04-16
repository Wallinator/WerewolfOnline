using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerewolfDomain.Phases {
	public class SeerPhase : AbstractPhase {
		List<Poll> polls = new List<Poll>();

		public SeerPhase(GameManager gameManager, AbstractPhase nextPhase) : base(gameManager, nextPhase) {
		}

		public SeerPhase(GameManager gameManager, PhaseType nextPhaseType) : base(gameManager, nextPhaseType) {
		}

		protected override int DefaultDuration => throw new NotImplementedException();

		protected override bool CanResolve() {

			return polls.All(x => gm.gs.Complete.Contains(x));
		}

		protected override void Resolve() {
			foreach (Poll poll in polls) {
				Player seer = poll.Voters.First();
				//show seer who role gm._hubContext.Clients.Client(seer.Id)(poll.Highest.First())
				gm.gs.Complete.Remove(poll);
				gm.gs.Resolved.Add(poll);
			}
		}

		public override void SetUp() {
			Poll poll;
			foreach (Player seer in gm.gs.PlayersByRole[RoleName.Seer]) {
				List<string> options = new List<string>(gm.gs.Alive
					.Except(((Seer)seer.Role).Checked)
					.Select(x => x.Name)
					);
				poll = new Poll(gm.gs.PlayersByRole[RoleName.Seer], options, PollType.Seer);
				gm.AddPoll(poll);
				polls.Add(poll);
			}
			IsSetup = true;
		}

		protected override void PreForceResolve() {
			foreach(Poll poll in polls) {
				if (gm.gs.Pending.Contains(poll)) {
					gm.ForceClose(poll);
				}
			}
		}
	}
}
