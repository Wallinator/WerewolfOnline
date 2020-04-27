using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests {
	public class WerewolfPhaseTests {


		private Phase phase;
		private PersistorMock mockPersistor;
		private PresentorMock mockPresentor;
		private readonly Player seer = new Player("1", "abby");
		private readonly Player werewolf = new Player("2", "bob");
		private readonly Player villager = new Player("3", "claire");

		[SetUp]
		public void Setup() {
			mockPersistor = new PersistorMock();
			mockPresentor = new PresentorMock();
			phase = new PhaseFactoryImpl(mockPersistor, mockPresentor, mockPersistor.AllPhasesExist()).ConstructPhase(PhaseType.Werewolf);
			seer.Role = new Seer();
			werewolf.Role = new Werewolf();
			villager.Role = new Villager();
			mockPersistor.Players = new List<Player>() {
				seer,
				werewolf,
				villager,
			};
		}

		[Test]
		public void WhenPollAddedShouldBePresented() {
			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Werewolf);
			Assert.AreEqual(poll, mockPresentor.PollShown);
		}

		[Test]
		public void WhenPollAddedShouldBeTypeWerewolf() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Werewolf);
			Assert.AreEqual(PollType.Werewolf, poll.Type);
		}
		[Test]
		public void WhenPollAddedShouldBeForWerewolves() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Werewolf);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersistor.Players.FindAll(player => player.Role.Name == RoleName.Werewolf)));
		}
		[Test]
		public void WhenPollAddedShouldHaveChoicesNonWerewolves() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Werewolf);
			List<string> choicesactual = poll.Choices.ConvertAll(obj => (string) obj);
			choicesactual.Sort();
			List<string> choicesexpected = mockPersistor.Players.FindAll(player => player.Role.Name != RoleName.Werewolf).ConvertAll(player => player.Name);
			choicesexpected.Sort();
			Assert.IsTrue(choicesactual.SequenceEqual(choicesexpected));
		}


		[Test]
		public void GivenPollOpenShouldNotResolve() {
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
		}
		[Test]
		public void GivenPollClosedPhaseShouldResolve() {
			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Werewolf);
			poll.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public void GivenPhaseResolvedPollShouldNotBeRemoved() {
			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Werewolf);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.AreEqual(poll, mockPersistor.GetPoll(PollType.Werewolf));
		}
		[Test]
		public void GivenPhaseForceResolvedPollShouldBeClosed() {
			phase.StateHasChanged();
			phase.ForceResolve();
			Poll poll = mockPersistor.GetPoll(PollType.Werewolf);
			Assert.IsTrue(poll.Closed);
		}
	}
}