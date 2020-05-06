using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Entities {
	public class PersisterObject : Persister {

		public List<Poll> Polls = new List<Poll>();
		void PollPersister.AddPoll(Poll poll) {
			Polls.Add(poll);
		}
		Poll PollPersister.GetPoll(PollType type) {
			Poll poll = Polls.Find(p => p.Type == type);
			return new Poll(poll);
		}
		void PollPersister.RemovePoll(PollType type) {
			Polls.RemoveAll(p => p.Type == type);
		}
		void PollPersister.PlaceVote(Player player, object choice, PollType type) {
			Poll poll = Polls.Find(p => p.Type == type);
			poll.PlaceVote(player, choice);
		}


		public List<Player> AllPlayers;
		List<Player> PlayerPersister.GetAllPlayers() {
			return AllPlayers.ConvertAll(x => new Player(x));
		}
		void PlayerPersister.UpdatePlayer(Player player) {
			AllPlayers.RemoveAll(p => p.Name.Equals(player.Name));
			AllPlayers.Add(player);
		}


		public Stack<Phase> NextPhases = new Stack<Phase>();
		Phase PhasePersister.GetNextPhase(PhaseType currentPhaseType) {
			return NextPhases.Pop();
		}
		void PhasePersister.AddNextPhase(Phase phase) {
			NextPhases.Push(phase);
		}
		bool PhasePersister.NextPhaseExists() {
			return NextPhases.Count != 0;
		}


		public bool PhaseSetup = false;
		bool PhasePersister.IsPhaseSetup() {
			return PhaseSetup;
		}
		void PhasePersister.SetPhaseSetup(bool Setup) {
			PhaseSetup = Setup;
		}

	}
}
