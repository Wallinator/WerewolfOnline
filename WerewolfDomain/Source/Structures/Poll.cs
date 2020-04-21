using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Entities;

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
		public bool PlaceVote(Player player, object choice) {
			Vote vote = new Vote(player, choice);
			if (Closed || !Voters.Contains(vote.Voter) || !Choices.Contains(vote.Choice)) {
				return false;
			}

			if (Voted.Contains(vote.Voter)) {

				Vote PreviousVote = new Vote(vote.Voter, Votes[vote.Voter]);
				if (PreviousVote.Equals(vote)) {
					return true;
				}
				else {
					RemoveVote(PreviousVote);
				}
			}
			else {
				Voted.Add(vote.Voter);
			}

			AddVote(vote);

			if (Voted.Count == Voters.Count) {
				ClosePoll();
			}

			return true;
		}

		private void RemoveVote(Vote vote) {
			Votes.Remove(vote.Voter);
			--Results[vote.Choice];
		}

		private void AddVote(Vote vote) {
			Votes.Add(vote.Voter, vote.Choice);
			++Results[vote.Choice];
		}



		public void ClosePoll() {
			Closed = true;
		}

		public List<object> Winners() {
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
			return Winners;
		}

		private readonly struct Vote  {
			public readonly Player Voter { get; }
			public readonly object Choice { get; }
			private readonly int Hash { get; }
			public Vote(Player voter, object choice) {
				Voter = voter;
				Choice = choice;
				Hash = HashCode.Combine(Voter, Choice);
			}
			public override int GetHashCode() {
				return Hash;
			}
			public override bool Equals(object obj) {
				return obj is Vote vote &&
					   EqualityComparer<Player>.Default.Equals(Voter, vote.Voter) &&
					   EqualityComparer<object>.Default.Equals(Choice, vote.Choice);
			}
		}
	}
	public enum PollType {
		Werewolf,
		Villager,
		Seer,
		Ready,
		Sleep
	}
}
