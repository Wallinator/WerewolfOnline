﻿using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class DiscussionPhase : PollPhase {
		internal DiscussionPhase(PhaseFactory factory, Persister persistor, Presenter presentor) : base(factory, persistor, presentor) {
		}

		internal override int DefaultDurationSeconds => 0;

		internal override PhaseType PhaseType => PhaseType.Discussion;

		protected override List<Poll> ConstructPolls() {
			List<Player> players = persistor.GetAllPlayers().FindAll(p => p.Role.Name != RoleName.Spectator);
			List<Poll> polls = new List<Poll>() {
				new Poll(players, presentor.GetDiscussionPollOptions(), PollType.Ready)
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