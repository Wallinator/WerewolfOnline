using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests {
	public class IntroductionPhaseTests {

		private Phase phase;
		private MockPersistor mockPersistor;
		private MockPresentor mockPresentor;

		[SetUp]
		public void Setup() {
			mockPersistor = new MockPersistor();
			mockPresentor = new MockPresentor();
			phase = new PhaseFactoryImpl(mockPersistor, mockPresentor, mockPersistor.AllPhasesExist()).MakeFirstPhase();
			mockPersistor.LivingPlayers = new List<Player>() {
				new Player("1", "abby"),
				new Player("2", "bob"),
				new Player("3", "claire"),
			};
			mockPersistor.PollToBeGot = new Poll(mockPersistor.LivingPlayers, new string[] { }, PollType.Ready);
		}

		[Test]
		public void IntroductionPhaseShouldAddPollOnSetup() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.PollAdded;
			Assert.AreEqual(PollType.Ready, poll.Type);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersistor.LivingPlayers));

		}
		[Test]
		public void IntroductionPhaseShouldNotResolve() {
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
		}
		[Test]
		public void IntroductionPhaseShouldResolveWhenPollCLosed() {
			mockPersistor.PollToBeGot.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public void IntroductionPhaseShouldRemovePollWhenResolved() {
			mockPersistor.PollToBeGot.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreEqual(PollType.Ready, mockPersistor.PollTypeRemoved);
			Assert.AreEqual(mockPersistor.PollToBeGot, mockPresentor.PollHidden);
		}
	}
}