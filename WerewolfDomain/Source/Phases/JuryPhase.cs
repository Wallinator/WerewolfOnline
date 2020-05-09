using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class JuryPhase : PollPhase {
		public JuryPhase(PhaseFactory factory, Persister persistor, Presenter presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 30;

		internal override PhaseType PhaseType => PhaseType.Werewolf;


		protected override List<PollType> PollTypes() {
			List<PollType> polltypes = new List<PollType> {
				PollType.Jury
			};
			return polltypes;
		}

		protected override List<Poll> ConstructPolls() {
			List<Player> livingPlayers = persistor.GetAllPlayers().FindAll(player => player.Role.Name != RoleName.Spectator);
			List<Poll> polls = new List<Poll>() {
				new Poll(livingPlayers, livingPlayers.ConvertAll(player => player.Name), PollType.Jury)
			};
			return polls;
		}

	}
}