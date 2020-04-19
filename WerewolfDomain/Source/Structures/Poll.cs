using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Entities;

namespace WerewolfDomain.Structures {
	public class Poll {

		private readonly HashSet<Player> Voted = new HashSet<Player>();
		public Dictionary<object, int> Results = new Dictionary<object, int>();
		public bool Closed { get; private set; } = false;
		public List<Vote> Votes = new List<Vote>();
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
		public bool Vote(Vote vote) {
			if (Closed || !Voters.Contains(vote.Voter) || !Choices.Contains(vote.Choice)) {
				return false;
			}

			if (Voted.Contains(vote.Voter)) {

				Vote PreviousVote = Votes.Find(x => x.Voter.Equals(vote.Voter));
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
			Votes.Remove(vote);
			--Results[vote.Choice];
		}

		private void AddVote(Vote vote) {
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
