using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Phases;
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
			phase = new PhaseFactoryImpl(mockPersistor, mockPresentor).MakeFirstPhase();
			mockPersistor.PollToBeGot = new Poll<string>(new List<Player>(), new string[] { }, PollType.Ready);
		}

		[Test]
		public void IntroductionPhaseShouldAddPollOnSetup() {
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