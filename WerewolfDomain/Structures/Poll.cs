using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WerewolfDomain.Play;

namespace WerewolfDomain.Structures {
	public class Poll<T> {

		private List<Player> Voted = new List<Player>();
		public Dictionary<T, int> Results = new Dictionary<T, int>();
		public bool Closed { get; set; } = false;
		public List<Vote<T>> Votes = new List<Vote<T>>();
		public List<Player> Voters;
		public List<T> Choices;
		public PollType Type;

		public Poll(List<Player> voters, List<T> nominees, PollType type) {
			Choices = nominees;
			Type = type;
			Voters = voters;
			foreach (T choice in Choices) {
				Results[choice] = 0;
			}
		}
		public bool Vote(Vote<T> vote) {
			if (Closed || !Voters.Contains(vote.Voter)) {
				return false;
			}

			if (Voted.Contains(vote.Voter)) {
				RemoveVote(vote);
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

		private void AddVote(Vote<T> vote) {
			Votes.Add(vote);
			++Results[vote.Choice];
		}

		private void RemoveVote(Vote<T> vote) {
			Votes.RemoveAll(x => x.Voter.Equals(vote.Voter));
			--Results[vote.Choice];
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
