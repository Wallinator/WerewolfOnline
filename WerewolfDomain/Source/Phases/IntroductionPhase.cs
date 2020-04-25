﻿using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class IntroductionPhase : PollPhase {
		public IntroductionPhase(PhaseFactory factory, Persister persistor, Presentor presentor) : base(factory, persistor, presentor) {
		}

		public override int DefaultDurationSeconds => 0;


		internal override PhaseType PhaseType => PhaseType.Introduction;

		protected override List<Poll> ConstructPolls() {
			List<Player> players = persistor.GetLivingPlayers();
			List<Poll> polls = new List<Poll>() {
				new Poll(players, new List<string> { "Ready" }, PollType.Ready)
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
