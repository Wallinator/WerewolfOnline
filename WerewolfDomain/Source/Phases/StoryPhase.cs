using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	internal class StoryPhase : PollPhase {

		public static List<PollType> PollsResolvedByStoryteller = new List<PollType> {
			PollType.Werewolf
		};

		public StoryPhase(PhaseFactory factory, Persister persistor, Presentor presentor) : base(factory, persistor, presentor) {

		}

		internal override PhaseType PhaseType => PhaseType.Story;

		public override int DefaultDurationSeconds => 0;

		protected override Phase NextPhase() {
			if (GetRemainingPolls().Count == 0) {
				return base.NextPhase();
			}
			else {
				return new StoryPhase(PhFactory, persistor, presentor);
			}
		}

		protected override List<PollType> PollTypes() {
			return new List<PollType>{ PollType.Storyteller };
		}

		protected override List<Poll> ConstructPolls() {
			List<Player> storytellers = persistor.GetAllPlayers().FindAll(p => p.IsStoryteller);
			List<PollType> pollsToResolve = GetRemainingPolls();
			Poll poll = new Poll(storytellers, pollsToResolve, PollType.Storyteller);
			return new List<Poll> { poll };
		}

		private List<PollType> GetRemainingPolls() {
			List<PollType> pollsToResolve = new List<PollType>();
			foreach (PollType type in PollsResolvedByStoryteller) {
				Poll p = persistor.GetPoll(type);
				if (p != null) {
					pollsToResolve.Add(type);
				}
			}
			return pollsToResolve;
		}
	}
}
