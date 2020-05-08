using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WerewolfDomain.Structures {
	public class Poll {

		private readonly HashSet<Player> Voted = new HashSet<Player>();
		public Dictionary<object, int> Results = new Dictionary<object, int>();
		public bool Closed { get; private set; } = false;
		public Dictionary<Player, object> Votes = new Dictionary<Player, object>();
		public HashSet<Player> Voters;
		public List<object> Choices;
		public PollType Type;

		public Poll(IEnumerable<Player> voters, IEnumerable nominees, PollType type) {
			Choices = new List<object>(nominees.Cast<object>());
			Type = type;
			Voters = new HashSet<Player>(voters);
			foreach (object choice in Choices) {
				Results[choice] = 0;
			}
		}
		public Poll(Poll poll) : this(poll.Voters, poll.Choices, poll.Type) {
			foreach (KeyValuePair<Player, object> vote in poll.Votes) {
				PlaceVote(vote.Key, vote.Value);
			}
		}
		public bool PlaceVote(Player voter, object choice) {
			if (Closed || !Voters.Contains(voter) || !Choices.Contains(choice)) {
				return false;
			}

			if (Voted.Contains(voter)) {
				object PreviousVote = Votes[voter];
				if (PreviousVote.Equals(choice)) {
					return true;
				}
				else {
					RemoveVote(voter, choice);
				}
			}
			else {
				Voted.Add(voter);
			}

			AddVote(voter, choice);

			if (Voted.Count == Voters.Count) {
				ClosePoll();
			}

			return true;
		}

		private void RemoveVote(Player voter, object choice) {
			Votes.Remove(voter);
			--Results[choice];
		}

		private void AddVote(Player voter, object choice) {
			Votes.Add(voter, choice);
			++Results[choice];
		}



		public void ClosePoll() {
			Closed = true;
		}

		private List<object> winners = null;
		public List<object> Winners() {
			if (winners == null) {
				List<object> Winners = new List<object>();
				int highestNum = 0;
				foreach (object choice in Results.Keys) {
					if (Results[choice] > highestNum) {
						Winners.Clear();
						Winners.Add(choice);
						highestNum = Results[choice];
					}
					else if (Results[choice] == highestNum) {
						Winners.Add(choice);
					}
				}
				winners = Winners;
			}
			return winners;
		}

	}
	public enum PollType {
		Werewolf,
		Villager,
		Seer,
		Ready,
		Sleep,
		Storyteller,
		Jury
	}
}
