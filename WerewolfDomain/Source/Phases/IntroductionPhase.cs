using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class IntroductionPhase : PollPhase {
		public IntroductionPhase(PhaseFactory factory, Persister persistor, Presenter presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 0;

		internal override PhaseType PhaseType => PhaseType.Introduction;

		protected override List<Poll> ConstructPolls() {
			List<Player> players = persistor.GetAllPlayers().FindAll(p => p.Role.Name != RoleName.Spectator);
			List<Poll> polls = new List<Poll>() {
				new Poll(players, presentor.GetIntroductionPollOptions(), PollType.Ready)
			};
			return polls;
		}

		protected override List<PollType> PollTypes() {
			List<PollType> polltypes = new List<PollType> {
				PollType.Ready
			};
			return polltypes;
		}
	}
}
