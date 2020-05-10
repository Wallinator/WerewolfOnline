using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.PollTests {
	public class PollTests {
		private Poll poll;
		private Player p1;
		private Player p2;
		private Player p3;
		private Player p4;
		private Player p5;

		[SetUp]
		public void Setup() {
			p1 = new Player("a");
			p2 = new Player("b");
			p3 = new Player("c");
			p4 = new Player("d");
			p5 = new Player("e");
			var players = new List<Player>() { p1, p2, p3, p4, p5 };
			var choices = new List<int>() { 1, 2, 3, 4, 5 };
			poll = new Poll(players, choices, PollType.Ready);
		}

		[Test]
		public void PollShouldCountVote() {
			bool result = poll.PlaceVote(p1, 1);
			Assert.IsTrue(result);
			Assert.AreEqual(1, poll.Votes.Count);
		}
		[Test]
		public void PollShouldNotCountVoteWhenClosed() {
			poll.ClosePoll();
			bool result = poll.PlaceVote(p1, 1);
			Assert.IsFalse(result);
		}
		[Test]
		public void PollShouldNotCountVoteWhenVoterNotInPollList() {
			var fakePlayer = new Player("name");
			bool result = poll.PlaceVote(fakePlayer, 1);
			Assert.IsFalse(result);
		}
		[Test]
		public void PollShouldNotCountVoteWhenChoiceNotInPollList() {
			bool result = poll.PlaceVote(p1, 9);
			Assert.IsFalse(result);
		}
		[Test]
		public void PollShouldChangeVoteWhenPlayerAlreadyVoted() {
			int firstChoice = 5;
			int secondChoice = 4;
			poll.PlaceVote(p1, firstChoice);
			poll.PlaceVote(p1, secondChoice);
			Assert.AreEqual(1, poll.Votes.Count);
			Assert.AreEqual(secondChoice, poll.Votes[p1]);
		}
		[Test]
		public void PollShouldCloseWhenAllVotesReceived() {
			poll.PlaceVote(p1, 1);
			poll.PlaceVote(p2, 1);
			poll.PlaceVote(p3, 1);
			poll.PlaceVote(p4, 1);
			poll.PlaceVote(p5, 1);
			Assert.IsTrue(poll.Closed);
		}
		[Test]
		public void PollResultsShouldReportVotes() {
			poll.PlaceVote(p1, 1);
			poll.PlaceVote(p2, 1);
			poll.PlaceVote(p3, 2);
			poll.PlaceVote(p4, 2);
			poll.PlaceVote(p5, 1);
			Assert.AreEqual(3, poll.Results[1]);
			Assert.AreEqual(2, poll.Results[2]);
			Assert.AreEqual(0, poll.Results[3]);
		}
		[Test]
		public void PollShouldReportWinnersWhenOneWinner() {
			poll.PlaceVote(p1, 1);
			poll.PlaceVote(p2, 1);
			poll.PlaceVote(p3, 2);
			poll.PlaceVote(p4, 2);
			poll.PlaceVote(p5, 1);
			Assert.AreEqual(1, (int) poll.Winners().First());
		}
		[Test]
		public void PollShouldReportWinnersWhenMultipleWinners() {
			poll.PlaceVote(p1, 1);
			poll.PlaceVote(p2, 1);
			poll.PlaceVote(p3, 2);
			poll.PlaceVote(p4, 2);
			poll.PlaceVote(p5, 3);
			List<int> actual = poll.Winners().ConvertAll(x => (int) x);
			List<int> expected = new List<int>() { 1, 2 };
			actual.Sort();
			expected.Sort();
			Assert.IsTrue(actual.SequenceEqual(expected));
		}
	}
}