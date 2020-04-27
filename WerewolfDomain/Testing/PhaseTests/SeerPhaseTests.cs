using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests {
	public class SeerPhaseTests {

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
			phase = new PhaseFactoryImpl(mockPersistor, mockPresentor, mockPersistor.AllPhasesExist()).ConstructPhase(PhaseType.Seer);
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
		public void PollAddedShouldBeTypeSeer() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Seer);
			Assert.AreEqual(PollType.Seer, poll.Type);
		}
		[Test]
		public void PollAddedShouldBeForSeer() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Seer);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersistor.GetLivingPlayers().FindAll(x => x.Role.Name == RoleName.Seer)));
		}
		[Test]
		public void PollAddedShouldHaveChoicesNonSeers() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Seer);
			List<string> choicesactual = poll.Choices.ConvertAll(obj => (string) obj);
			choicesactual.Sort();
			List<string> choicesexpected = mockPersistor.GetLivingPlayers().FindAll(x => x.Role.Name != RoleName.Seer).ConvertAll(player => player.Name);
			choicesexpected.Sort();
			Assert.IsTrue(choicesactual.SequenceEqual(choicesexpected));
		}
		[Test]
		public void ShouldResolveWhenVoted() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf.Name);

			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}

		[Test]
		public void ShouldDoNothingWhenForceResolved() {

			phase.StateHasChanged();

			Phase newPhase = phase.ForceResolve();
			Assert.IsNull(mockPresentor.NameOfPlayerShownToSeer);
			Assert.IsNull(mockPresentor.SeerShownRole);
		}
		[Test]
		public void ShouldShowSeerChosenPlayerRoleWhenResolved() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf.Name);

			Phase newPhase = phase.StateHasChanged();
			Assert.AreEqual(werewolf.Name, mockPresentor.NameOfPlayerShownToSeer);
			Assert.AreEqual(seer, mockPresentor.SeerShownRole);
		}
		[Test]
		public void SeerPhaseShouldNotResolveWhenPollOpen() {
			phase.StateHasChanged();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
		}
		[Test]
		public void SeerPhaseShouldGiveNextPhaseWhenPollClosed() {
			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Seer);
			poll.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public void SeerPhaseShouldRemovePollWhenResolved() {
			phase.StateHasChanged();
			Poll poll = mockPersistor.GetPoll(PollType.Seer);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.AreEqual(0, mockPersistor.Polls.Count);
			Assert.AreEqual(poll, mockPresentor.PollHidden);
		}
	}
}