using System.Collections.Generic;
using WerewolfDomain.Entities;

namespace WerewolfDomain.Structures {
	public class Poll<T> {

		private readonly HashSet<Player> Voted = new HashSet<Player>();
		public Dictionary<T, int> Results = new Dictionary<T, int>();
		public bool Closed { get; private set; } = false;
		public List<Vote<T>> Votes = new List<Vote<T>>();
		public HashSet<Player> Voters;
		public List<T> Choices;
		public PollType Type;

		public Poll(IEnumerable<Player> voters, IEnumerable<T> nominees, PollType type) {
			Choices = new List<T>(nominees);
			Type = type;
			Voters = new HashSet<Player>(voters);
			foreach (T choice in Choices) {
				Results[choice] = 0;
			}
		}
		public bool Vote(Vote<T> vote) {
			if (Closed || !Voters.Contains(vote.Voter) || !Choices.Contains(vote.Choice)) {
				return false;
			}

			if (Voted.Contains(vote.Voter)) {

				Vote<T> PreviousVote = Votes.Find(x => x.Voter.Equals(vote.Voter));
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

		private void RemoveVote(Vote<T> vote) {
			Votes.Remove(vote);
			--Results[vote.Choice];
		}

		private void AddVote(Vote<T> vote) {
			Votes.Add(vote);
			++Results[vote.Choice];
		}



		public void ClosePoll() {
			Closed = true;
		}

	}
	public enum PollType {
		Werewolf,
		Villager,
		Seer,
		Ready,
		Sleep
	}
	/*
	public static class PollDetails {
		public static Dictionary<PollType, string> titles = new Dictionary<PollType, string> {
			{ PollType.Werewolf, "werewolf poll title" },
			{ PollType.Villager, "villager poll title" },
			{ PollType.Seer, "seer poll title" },
			{ PollType.Ready, "Are you ready?" },
			{ PollType.Sleep, "Night falls..." }
		};
		public static Dictionary<PollType, string> instructions = new Dictionary<PollType, string> {
			{ PollType.Werewolf, "vote villager kill" },
			{ PollType.Villager, "vote suspect" },
			{ PollType.Seer, "whos role to see" },
			{ PollType.Ready, "Ready up!" },
			{ PollType.Sleep, "Close your eyes!" }
		};
	}*/
}
