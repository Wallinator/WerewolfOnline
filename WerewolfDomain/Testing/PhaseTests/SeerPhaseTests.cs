using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Entities;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests {
	public class SeerPhaseTests {



		private Phase phase;
		private MockPersistor mockPersistor;
		private MockPresentor mockPresentor;

		[SetUp]
		public void Setup() {
			mockPersistor = new MockPersistor();
			mockPresentor = new MockPresentor();
			PhaseFactoryImpl factory = new PhaseFactoryImpl(mockPersistor, mockPresentor, mockPersistor.AllPhasesExist());
			phase = factory.MakeFirstPhase();
			phase = factory.MakeNextPhase(phase);
			phase = factory.MakeNextPhase(phase);
			Player p1 = new Player("1", "abby");
			Player p2 = new Player("2", "bob");
			Player p3 = new Player("3", "claire");
			mockPersistor.LivingPlayers = new List<Player>() {
				p1,
				p2,
				p3,
			};
			mockPersistor.NonWerewolves = new List<Player>() {
				p1,
				p2,
			};
			mockPersistor.Werewolves = new List<Player>() {
				p3
			};
			mockPersistor.PollToBeGot = new Poll(mockPersistor.Werewolves, mockPersistor.NonWerewolves.ConvertAll(v => v.Name), PollType.Werewolf);
			mockPersistor.PollToBeGot.Vote(new Vote(p3, p2.Name));
		}

		[Test]
		public void WerewolfPhaseShouldAddPollCorrectlyWhenSetup() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.PollAdded;
			Assert.AreEqual(PollType.Werewolf, poll.Type);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersistor.Werewolves));
			List<string> choicesactual = poll.Choices.ConvertAll(obj => (string)obj);
			choicesactual.Sort();
			List<string> choicesexpected = mockPersistor.NonWerewolves.ConvertAll(player => player.Name);
			choicesexpected.Sort();
			Assert.IsTrue(choicesactual.SequenceEqual(choicesexpected));
			Assert.Fail();
		}
		[Test]
		public void WerewolfPhaseShouldNotResolveWhenPollOpen() {
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
			Assert.Fail();
		}
		[Test]
		public void WerewolfPhaseShouldNotResolvePollWhenPollClosed() {
			mockPersistor.PollToBeGot.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.IsFalse(mockPersistor.PollTypeRemoved.HasValue);
			Assert.IsNull(mockPresentor.PollHidden);
			Assert.Fail();
		}
		[Test]
		public void WerewolfPhaseShouldGiveNextPhaseWhenPollClosed() {
			mockPersistor.PollToBeGot.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
			Assert.Fail();
		}
	}
}