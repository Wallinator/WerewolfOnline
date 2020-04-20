using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Entities;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests {
	public class WerewolfPhaseTests {


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
			mockPersistor.PollToBeGot = new Poll(new List<Player>(), new string[] { }, PollType.Werewolf);
			mockPersistor.LivingPlayers = new List<Player>() {
				new Player("1", "abby"),
				new Player("2", "bob"),
				new Player("3", "claire"),
			};
			mockPersistor.NonWerewolves = new List<Player>() {
				new Player("1", "abby"),
				new Player("2", "bob"),
			};
			mockPersistor.Werewolves = new List<Player>() {
				new Player("3", "claire")
			};
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
		}
		[Test]
		public void WerewolfPhaseShouldNotResolveWhenPollOpen() {
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
		}
		[Test]
		public void WerewolfPhaseShouldNotResolvePollWhenPollClosed() {
			mockPersistor.PollToBeGot.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.IsFalse(mockPersistor.PollTypeRemoved.HasValue);
			Assert.IsNull(mockPresentor.PollHidden);
		}
		[Test]
		public void WerewolfPhaseShouldGiveNextPhaseWhenPollClosed() {
			mockPersistor.PollToBeGot.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
	}
}