using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WerewolfOnline.Model.Structure {
	public class Poll {
		public bool Closed { get; set; } = false;
		public List<string> Highest = new List<string>();
		public Dictionary<string, int> Results = null;
		public Dictionary<string, List<Player>> Votes = new Dictionary<string, List<Player>>();
		public List<Player> Voted = new List<Player>();
		public List<Player> Voters;
		public List<string> Nominees;
		public PollType Type;
		public Poll(List<Player> voters, List<string> nominees, PollType type) {
			Nominees = nominees;
			Type = type;
			Voters = voters;
			foreach (string s in Nominees) {
				Votes[s] = new List<Player>();
			}
		}
		public bool Vote(Player voter, string vote) {
			if (Closed || !Voters.Contains(voter)) {
				return false;
			}

			if (Voted.Contains(voter)) {
				foreach (List<Player> list in Votes.Values) {
					list.Remove(voter);
				}
			}
			else {
				Voted.Add(voter);
			}
			Votes[vote].Add(voter);

			if (Voted.Count == Voters.Count) {
				ClosePoll();
			}
			return true;
		}

		public void ClosePoll() {
			Closed = true;
			Results = new Dictionary<string, int>();
			int highestNum = 0;
			foreach (string vote in Votes.Keys) {
				Results[vote] = Votes[vote].Count;
				if (Results[vote] > highestNum) {
					Highest.Clear();
					Highest.Add(vote);
					highestNum = Results[vote];
				}
				if (Results[vote] == highestNum) {
					Highest.Add(vote);
				}
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
	}
}
