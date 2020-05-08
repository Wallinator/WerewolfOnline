using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;
using WerewolfDomainTests.PhaseTests.Mocks;
using WerewolfDomainTests.PhaseTests.Shared;

namespace WerewolfDomainTests.PhaseTests {
	internal class SeerPhaseTests : PollPhaseTests {
		protected override Poll SamplePoll => new Poll(	mockPersister.GetAllPlayers()	.FindAll(x => x.Role.Name == RoleName.Seer),
														mockPersister.GetAllPlayers()	.FindAll(x =>	x.Role.Name != RoleName.Seer &&
																									x.Role.Name != RoleName.Spectator)
																						.ConvertAll(player => player.Name),
														PollType.Seer);

		protected override PhaseType PhaseType => PhaseType.Seer;

		[SetUp]
		public void Setup() {
		}

		[Test]
		public void ShouldShowNothingWhenForceResolved() {

			phase.SetUp();
			phase.ForceResolve();
			SeerRevealEvent seerRevealEvent = (SeerRevealEvent) mockPresenter.visibleEvents.Find(x => x is SeerRevealEvent);
			Assert.IsTrue(seerRevealEvent.Players.Contains(seer));
			Assert.IsNull(seerRevealEvent.RevealedName);
			Assert.AreEqual(RoleName.None, seerRevealEvent.RevealedRole);
		}
		[Test]
		public void ShouldShowSeerWhenResolved() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf1.Name);
			phase.StateHasChanged();
			SeerRevealEvent seerRevealEvent = (SeerRevealEvent) mockPresenter.visibleEvents.Find(x => x is SeerRevealEvent);
			Assert.IsTrue(seerRevealEvent.Players.Contains(seer));
		}
		[Test]
		public void ShouldShowChosenPlayerNameWhenResolved() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf1.Name);
			phase.StateHasChanged();
			SeerRevealEvent seerRevealEvent = (SeerRevealEvent) mockPresenter.visibleEvents.Find(x => x is SeerRevealEvent);
			Assert.AreEqual(werewolf1.Name, seerRevealEvent.RevealedName);
		}
		[Test]
		public void ShouldShowChosenPlayerRoleWhenResolved() {

			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf1.Name);
			phase.StateHasChanged();
			SeerRevealEvent seerRevealEvent = (SeerRevealEvent) mockPresenter.visibleEvents.Find(x => x is SeerRevealEvent);
			Assert.AreEqual(RoleName.Werewolf, seerRevealEvent.RevealedRole);
		}
		[Test]
		public void SeerPhaseShouldNotRemoveSeerEventWhenResolved() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Seer);
			poll.PlaceVote(seer, werewolf1.Name);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.IsTrue(mockPresenter.visibleEvents.Any(x => x is SeerRevealEvent));
		}
	}
}