using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class SeerPhase : PollPhase {
		public SeerPhase(PhaseFactory factory, Persistor persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 30;

		internal override PhaseType PhaseType => PhaseType.Seer;

		protected override List<Poll> ConstructPolls() {
			List<Poll> polls = new List<Poll>();
			List<Player> livingPlayers = persistor.GetLivingPlayers();
			Poll poll = new Poll(
				livingPlayers.FindAll(x => x.Role.Name == RoleName.Seer),
				livingPlayers.FindAll(x => x.Role.Name != RoleName.Seer).ConvertAll(player => player.Name),
				PollType.Seer);
			polls.Add(poll);
			return polls;
		}

		protected override List<PollType> PollTypes() {
			List<PollType> polltypes = new List<PollType> {
				PollType.Seer
			};
			return polltypes;
		}
	}
}