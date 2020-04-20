using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class WerewolfPhase : PollPhase {
		public WerewolfPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 30;

		internal override PhaseType PhaseType => PhaseType.Werewolf;

		protected override List<Poll> GetMyPolls() {
			List<Poll> polls = new List<Poll> {
				persistor.GetPoll(PollType.Werewolf)
			};
			return polls;
		}

		protected override List<Poll> ConstructPolls() {
			List<Player> wolves = persistor.GetLivingWerewolves();
			List<Player> nonWolves = persistor.GetLivingNonWerewolves();
			List<Poll> polls = new List<Poll>() {
				new Poll(wolves, nonWolves.ConvertAll(player => player.Name), PollType.Werewolf)
			};
			return polls;
		}

		protected override void ConcreteResolve() {
			return;
		}
	}
}
