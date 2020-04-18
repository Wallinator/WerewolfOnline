using NUnit.Framework;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.PollTests {
	public class PollTests {
		private Poll<int> poll;
		private Player p1;
		private Player p2;
		private Player p3;
		private Player p4;
		private Player p5;

		[SetUp]
		public void Setup() {
			p1 = new Player("1", "a");
			p2 = new Player("2", "b");
			p3 = new Player("3", "c");
			p4 = new Player("4", "d");
			p5 = new Player("5", "e");
			var players = new List<Player>() { p1, p2, p3, p4, p5 };
			var choices = new List<int>() { 1, 2, 3, 4, 5 };
			poll = new Poll<int>(players, choices, PollType.Ready);
		}

		[Test]
		public void PollShouldCountVote() {
			bool result = poll.Vote(new Vote<int>(p1, 1));
			Assert.IsTrue(result);
			Assert.AreEqual(1, poll.Votes.Count);
		}
		[Test]
		public void PollShouldNotCountVoteWhenClosed() {
			poll.ClosePoll();
			bool result = poll.Vote(new Vote<int>(p1, 1));
			Assert.IsFalse(result);
		}
		[Test]
		public void PollShouldNotCountVoteWhenVoterNotInPollList() {
			var fakePlayer = new Player("33", "name");
			bool result = poll.Vote(new Vote<int>(fakePlayer, 1));
			Assert.IsFalse(result);
		}
		[Test]
		public void PollShouldNotCountVoteWhenChoiceNotInPollList() {
			bool result = poll.Vote(new Vote<int>(p1, 9));
			Assert.IsFalse(result);
		}
		[Test]
		public void PollShouldChangeVoteWhenPlayerAlreadyVoted() {
			int firstChoice = 5;
			int secondChoice = 4;
			poll.Vote(new Vote<int>(p1, firstChoice));
			poll.Vote(new Vote<int>(p1, secondChoice));
			Assert.AreEqual(1, poll.Votes.Count);
			Assert.AreEqual(secondChoice, poll.Votes.Find(x => x.Voter.Equals(p1)).Choice);
		}
		[Test]
		public void PollShouldCloseWhenAllVotesReceived() {
			poll.Vote(new Vote<int>(p1, 1));
			poll.Vote(new Vote<int>(p2, 1));
			poll.Vote(new Vote<int>(p3, 1));
			poll.Vote(new Vote<int>(p4, 1));
			poll.Vote(new Vote<int>(p5, 1));
			Assert.IsTrue(poll.Closed);
		}
		[Test]
		public void PollShouldReportVotes() {
			poll.Vote(new Vote<int>(p1, 1));
			poll.Vote(new Vote<int>(p2, 1));
			poll.Vote(new Vote<int>(p3, 2));
			poll.Vote(new Vote<int>(p4, 2));
			poll.Vote(new Vote<int>(p5, 1));
			Assert.AreEqual(3, poll.Results[1]);
			Assert.AreEqual(2, poll.Results[2]);
			Assert.AreEqual(0, poll.Results[3]);
		}
	}
}