using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class WerewolfPhase : PollPhase {
		public WerewolfPhase(PhaseFactory factory, Persister persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 30;

		internal override PhaseType PhaseType => PhaseType.Werewolf;


		protected override List<PollType> PollTypes() {
			List<PollType> polltypes = new List<PollType> {
				PollType.Werewolf
			};
			return polltypes;
		}

		protected override List<Poll> ConstructPolls() {
			List<Player> wolves = persistor.GetAllPlayers().FindAll(player => player.Role.Name == RoleName.Werewolf);
			List<Player> nonWolves = persistor.GetAllPlayers().FindAll(player => player.Role.Name != RoleName.Werewolf && player.Role.Name != RoleName.Spectator);
			List<Poll> polls = new List<Poll>() {
				new Poll(wolves, nonWolves.ConvertAll(player => player.Name), PollType.Werewolf)
			};
			return polls;
		}

		protected override void PhaseResolve() {
			Poll poll = persistor.GetPoll(PollType.Werewolf);
			presentor.HidePoll(poll);
			return;
		}
	}
}
