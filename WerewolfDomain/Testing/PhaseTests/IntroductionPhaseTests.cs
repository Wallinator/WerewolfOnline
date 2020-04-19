using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Phases;
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
			phase = new PhaseFactoryImpl(mockPersistor, mockPresentor).MakeFirstPhase();
			mockPersistor.PollToBeGot = new Poll<string>(new List<Player>(), new string[] { }, PollType.Ready);
			mockPersistor.LivingPlayers = new List<Player>() {
				new Player("1", "abby"),
				new Player("2", "bob"),
				new Player("3", "claire"),
			};
		}

		[Test]
		public void IntroductionPhaseShouldAddPollOnSetup() {

			phase.StateHasChanged();
			Poll<string> poll = mockPersistor.PollAdded;
			Assert.AreEqual(PollType.Ready, poll.Type);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersistor.LivingPlayers));

		}
		[Test]
		public void IntroductionPhaseShouldNotResolve() {
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
		}
		[Test]
		public void IntroductionPhaseShouldResolve() {
			mockPersistor.PollToBeGot.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
	}
}