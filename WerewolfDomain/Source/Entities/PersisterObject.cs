using PhaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Entities {
	public class PersisterObject : Persister {

		public List<Poll> Polls = new List<Poll>();
		public void AddPoll(Poll poll) {
			Polls.Add(poll);
		}
		public Poll GetPoll(PollType type) {
			return Polls.Find(p => p.Type == type);
		}
		public void RemovePoll(PollType type) {
			Polls.RemoveAll(p => p.Type == type);
		}

		public List<Player> Players;
		public List<Player> GetLivingPlayers() {
			return Players;
		}


		public Stack<Phase> NextPhases = new Stack<Phase>();
		public Phase GetNextPhase(PhaseType currentPhaseType) {
			return NextPhases.Pop();
		}
		public void AddNextPhase(Phase phase) {
			NextPhases.Push(phase);
		}

		public bool NextPhaseExists() {
			return NextPhases.Count != 0;
		}

	}
}
