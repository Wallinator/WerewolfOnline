using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Entities {
	public class PersisterObject : Persister {

		public List<Poll> Polls = new List<Poll>();
		void Persister.AddPoll(Poll poll) {
			Polls.Add(poll);
		}
		Poll Persister.GetPoll(PollType type) {
			Poll poll = Polls.Find(p => p.Type == type);
			return new Poll(poll);
		}
		void Persister.RemovePoll(PollType type) {
			Polls.RemoveAll(p => p.Type == type);
		}
		void Persister.PlaceVote(Player player, object choice, PollType type) {
			Poll poll = Polls.Find(p => p.Type == type);
			poll.PlaceVote(player, choice);
		}

		public List<Player> AllPlayers;
		List<Player> Persister.GetAllPlayers() {
			return AllPlayers.ConvertAll(x => new Player(x));
		}
		void Persister.UpdatePlayer(Player player) {
			AllPlayers.RemoveAll(p => p.Name.Equals(player.Name));
			AllPlayers.Add(player);
		}

		public Stack<Phase> NextPhases = new Stack<Phase>();
		Phase Persister.GetNextPhase(PhaseType currentPhaseType) {
			return NextPhases.Pop();
		}
		public void AddNextPhase(Phase phase) {
			NextPhases.Push(phase);
		}

		bool Persister.NextPhaseExists() {
			return NextPhases.Count != 0;
		}


		public bool PhaseSetup = false;
		bool Persister.IsPhaseSetup() {
			return PhaseSetup;
		}

		void Persister.SetPhaseSetup(bool Setup) {
			PhaseSetup = Setup;
		}

	}
}
