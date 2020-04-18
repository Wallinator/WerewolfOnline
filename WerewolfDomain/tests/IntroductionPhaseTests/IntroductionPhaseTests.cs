using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Phases;
using WerewolfDomain.Structures;
using WerewolfDomainTests.IntroductionPhaseTests.Mocks;

namespace WerewolfDomainTests.IntroductionPhaseTests {
	public class IntroductionPhaseTests {

		private Phase phase;
		private IntroductionPhaseMockPersistor mockPersistor;
		private IntroductionPhaseMockPresentor mockPresentor;

		[SetUp]
		public void Setup() {
			mockPersistor = new IntroductionPhaseMockPersistor();
			mockPresentor = new IntroductionPhaseMockPresentor();
			phase = new PhaseFactoryImpl(mockPersistor, mockPresentor).MakeFirstPhase();
			mockPersistor.PollToBeGot = new Poll<string>(new List<Player>(), new string[] { }, PollType.Ready);
		}

		[Test]
		public void IntroductionPhaseShouldAddPollOnSetup() {
			phase.StateHasChanged();
			Assert.IsTrue(mockPersistor.AddPollCalled);
			Assert.IsTrue(mockPresentor.ShowPollCalled);
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