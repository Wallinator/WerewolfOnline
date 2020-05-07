using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests {
	public class SeerPhaseTests {

		private Phase phase;
		private PersistorMock mockPersister;
		private PresentorMock mockPresenter;
		private readonly Player seer = new Player("1", "abby");
		private readonly Player werewolf = new Player("2", "bob");
		private readonly Player villager = new Player("3", "claire");

		[SetUp]
		public void Setup() {
			mockPersister = new PersistorMock();
			mockPresenter = new PresentorMock();
			phase = new PhaseFactoryImpl(mockPersister, mockPresenter, mockPersister.AllPhasesExist()).ConstructPhase(PhaseType.Seer);
			seer.Role = new Seer();
			werewolf.Role = new Werewolf();
			villager.Role = new Villager();
			mockPersister.AllPlayers = new List<Player>() {
				seer,
				werewolf,
				villager,
			};
		}


		[Test]
		public void WhenPollAddedShouldBePresented() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			Assert.AreEqual(poll, mockPresenter.PollShown);
		}

		[Test]
		public void PollAddedShouldBeTypeSeer() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			Assert.AreEqual(PollType.Seer, poll.Type);
		}
		[Test]
		public void PollAddedShouldBeForSeer() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersister.GetAllPlayers().FindAll(x => x.Role.Name == RoleName.Seer)));
		}
		[Test]
		public void PollAddedShouldHaveChoicesNonSeers() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			List<string> choicesactual = poll.Choices.ConvertAll(obj => (string) obj);
			choicesactual.Sort();
			List<string> choicesexpected = mockPersister.GetAllPlayers()
				.FindAll(x =>	x.Role.Name != RoleName.Seer &&
								x.Role.Name != RoleName.Spectator)
				.ConvertAll(player => player.Name);

			choicesexpected.Sort();
			Assert.IsTrue(choicesactual.SequenceEqual(choicesexpected));
		}
		[Test]
		public void ShouldResolveWhenVoted() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf.Name);

			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}

		[Test]
		public void ShouldDoNothingWhenForceResolved() {

			phase.SetUp();

			Phase newPhase = phase.ForceResolve();
			Assert.IsEmpty(mockPresenter.visibleEvents);
		}
		[Test]
		public void ShouldShowSeerWhenResolved() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf.Name);
			Phase newPhase = phase.StateHasChanged();
			SeerRevealEvent seerRevealEvent = (SeerRevealEvent) mockPresenter.visibleEvents.Find(x => x is SeerRevealEvent);
			Assert.IsTrue(seerRevealEvent.Players.Contains(seer));
		}
		[Test]
		public void ShouldShowChosenPlayerNameWhenResolved() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf.Name);
			Phase newPhase = phase.StateHasChanged();
			SeerRevealEvent seerRevealEvent = (SeerRevealEvent) mockPresenter.visibleEvents.Find(x => x is SeerRevealEvent);
			Assert.AreEqual(werewolf.Name, seerRevealEvent.RevealedName);
		}
		[Test]
		public void ShouldShowChosenPlayerRoleWhenResolved() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf.Name);
			Phase newPhase = phase.StateHasChanged();
			SeerRevealEvent seerRevealEvent = (SeerRevealEvent) mockPresenter.visibleEvents.Find(x => x is SeerRevealEvent);
			Assert.AreEqual(RoleName.Werewolf, seerRevealEvent.RevealedRole);
		}
		[Test]
		public void SeerPhaseShouldNotResolveWhenPollOpen() {
			phase.SetUp();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
		}
		[Test]
		public void SeerPhaseShouldGiveNextPhaseWhenPollClosed() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public void SeerPhaseShouldRemovePollWhenResolved() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.AreEqual(0, mockPersister.Polls.Count);
		}
		[Test]
		public void SeerPhaseShouldNotRemoveSeerEventWhenResolved() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf.Name);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.IsTrue(mockPresenter.visibleEvents.Any(x => x is SeerRevealEvent));
		}

		[Test]
		public void WhenPhaseResolvedShouldHidePoll() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.IsTrue(mockPresenter.PollHidden);
		}
		[Test]
		public void WhenPhaseForceResolvedShouldHidePoll() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.ClosePoll();
			phase.ForceResolve();
			Assert.IsTrue(mockPresenter.PollHidden);
		}
	}
}