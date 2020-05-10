using NUnit.Framework;
using PhaseLibrary;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;
using WerewolfDomainTests.PhaseTests.Shared;

namespace WerewolfDomainTests.PhaseTests.StoryPhase {
	internal class WerewolfResolutionTests : PhaseSetup {

		private Poll werewolfpoll;

		[SetUp]
		public void Setup() {

			phase = factory.ConstructPhase(PhaseType.Werewolf);
			phase.SetUp();
			mockPersister.PhaseSetup = false;
			werewolfpoll = mockPersister.GetPoll(PollType.Werewolf);

			phase = factory.ConstructPhase(PhaseType.Story);
			phase.SetUp();
			Poll storypoll = mockPersister.GetPoll(PollType.Storyteller);
			storypoll.PlaceVote(villager, PollType.Werewolf);
		}


		[Test]
		public void WhenWerewolfPollHasNoVotesShouldResolve() {
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public void WhenWerewolfPollHasNoVotesShouldRemovePoll() {
			phase.StateHasChanged();
			Assert.IsFalse(mockPersister.Polls.Exists(p => p.Type == PollType.Werewolf));
		}
		[Test]
		public void WhenWerewolfPollHasNoVotesShouldPresentNoKill() {
			phase.StateHasChanged();
			WerewolfKillEvent gameEvent = (WerewolfKillEvent) mockPresenter.visibleEvents.Find(e => e.Type == EventType.WerewolfKill);
			Assert.IsNull(gameEvent.VictimName);
		}
		[Test]
		public void WhenWerewolfPollHasNoVotesShouldNotKill() {
			phase.StateHasChanged();
			Assert.IsTrue(mockPersister.GetAllPlayers().TrueForAll(p => p.Role.Name != RoleName.Spectator));
		}


		[Test]
		public void WhenWerewolfPollHasTiedVotesShouldResolve() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, seer.Name);
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public void WhenWerewolfPollHasTiedVotesShouldRemovePoll() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, seer.Name);
			phase.StateHasChanged();
			Assert.IsFalse(mockPersister.Polls.Exists(p => p.Type == PollType.Werewolf));
		}
		[Test]
		public void WhenWerewolfPollHasTiedVotesShouldPresentNoKill() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, seer.Name);
			phase.StateHasChanged();
			WerewolfKillEvent gameEvent = (WerewolfKillEvent) mockPresenter.visibleEvents.Find(e => e.Type == EventType.WerewolfKill);
			Assert.IsNull(gameEvent.VictimName);
		}
		[Test]
		public void WhenWerewolfPollHasTiedVotesShouldNotKill() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, seer.Name);
			phase.StateHasChanged();
			Assert.IsTrue(mockPersister.GetAllPlayers().TrueForAll(p => p.Role.Name != RoleName.Spectator));
		}


		[Test]
		public void WhenWerewolfPollHasValidVictimShouldResolve() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, villager.Name);
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public void WhenWerewolfPollHasValidVictimShouldRemovePoll() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, villager.Name);
			phase.StateHasChanged();
			Assert.IsFalse(mockPersister.Polls.Exists(p => p.Type == PollType.Werewolf));
		}
		[Test]
		public void WhenWerewolfPollHasValidVictimShouldKill() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, villager.Name);
			phase.StateHasChanged();
			Assert.AreEqual(RoleName.Spectator, villager.Role.Name);
		}
		[Test]
		public void WhenWerewolfPollHasValidVictimShouldPresentKill() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, villager.Name);
			phase.StateHasChanged();
			WerewolfKillEvent gameEvent = mockPresenter.visibleEvents.Find(e => e.Type == EventType.WerewolfKill) as WerewolfKillEvent;
			Assert.AreEqual(villager.Name, gameEvent.VictimName);
		}
	}
}