using PhaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WerewolfDomain.Exceptions;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structure;

namespace WerewolfDomain.Phases {
	internal class IntroductionPhase : PresentingPhase {

		public IntroductionPhase(PhaseFactory factory, Action<Phase> setPhaseAction, Presentor presentor, Persistor persistor) : base(factory, setPhaseAction, presentor, persistor) {
		}

		protected override int DefaultDuration => 0;

		protected override bool CanResolve() {
			Poll? poll;
			try {
				poll = persistor.GetPoll(PollType.Ready);
			}
			catch(PollNotFoundException) {
				return false;
			}

			return gm.gs.Complete.Contains(poll);
		}


		protected override void PreForceResolve() {
			if (gm.gs.Pending.Contains(poll)) {
				gm.ForceClose(poll);
			}
		}

		protected override void PhaseSetUp() {
			List<Player> LivingPlayers = persistor.GetLivingPlayers();
			Poll poll = new Poll(LivingPlayers, new List<string>() { "Ready" }, PollType.Ready);
			persistor.AddPoll(poll);
			presentor.ShowPoll(LivingPlayers, poll);
		}

		protected override void PhaseResolve() {
			gm.gs.Complete.Remove(poll);
			gm.gs.Resolved.Add(poll);
		}
	}
}
