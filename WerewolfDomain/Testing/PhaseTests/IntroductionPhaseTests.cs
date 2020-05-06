using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests {
	public class IntroductionPhaseTests {
		private Phase phase;
		private PersistorMock mockPersister;
		private PresentorMock mockPresentor;
		private readonly Player p1 = new Player("1", "abby");
		private readonly Player p2 = new Player("2", "bob");
		private readonly Player p3 = new Player("3", "claire");

		[SetUp]
		public void Setup() {
			mockPersister = new PersistorMock();
			mockPresentor = new PresentorMock();
			phase = new PhaseFactoryImpl(mockPersister, mockPresentor, mockPersister.AllPhasesExist()).ConstructPhase(PhaseType.Introduction);
			mockPersister.AllPlayers = new List<Player>() {
				p1,
				p2,
				p3,
			};
		}

		[Test]
		public void WhenPollAddedShouldBePresented() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Ready);
			Assert.AreEqual(poll, mockPresentor.PollShown);
		}
		[Test]
		public void WhenPollAddedShouldBeTypeReady() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Ready);
			Assert.AreEqual(PollType.Ready, poll.Type);
		}
		[Test]
		public void WhenPollAddedShouldBeForAllPlayers() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Ready);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersister.AllPlayers.FindAll(x => x.Role.Name != RoleName.Spectator)));
		}

		[Test]
		public void GivenPollOpenPhaseShouldNotResolve() {
			phase.SetUp();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
		}

		[Test]
		public void GivenPollClosedPhaseShouldResolve() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Ready);
			poll.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public void ShouldRemovePollWhenResolved() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Ready);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.AreEqual(0, mockPersister.Polls.Count);
		}
	}
}