using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests.StoryPhase {
	public class WerewolfResolutionTests {
		private Phase phase;
		private PersistorMock mockPersistor;
		private PresentorMock mockPresentor;
		private readonly Player seer = new Player("1", "abby");
		private readonly Player werewolf1 = new Player("2", "bob");
		private readonly Player werewolf2 = new Player("3", "claire");
		private readonly Player villager = new Player("4", "debra");
		private Poll werewolfpoll;
		PhaseFactoryImpl factory;


		[SetUp]
		public void Setup() {
			mockPersistor = new PersistorMock();
			mockPresentor = new PresentorMock();
			seer.Role = new Seer();
			werewolf1.Role = new Werewolf();
			werewolf2.Role = new Werewolf();
			villager.Role = new Villager();
			villager.IsStoryteller = true;
			List<Player> players = new List<Player>() {
					seer,
					werewolf1,
					werewolf2,
					villager
				};
			mockPersistor.AllPlayers = players;

			factory = new PhaseFactoryImpl(mockPersistor, mockPresentor, mockPersistor.AllPhasesExist());

			phase = factory.ConstructPhase(PhaseType.Werewolf);
			phase.SetUp();
			mockPersistor.PhaseSetup = false;
			werewolfpoll = mockPersistor.GetPoll(PollType.Werewolf);

			phase = factory.ConstructPhase(PhaseType.Story);
			phase.SetUp();
			Poll storypoll = mockPersistor.GetPoll(PollType.Storyteller);
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
			Assert.IsFalse(mockPersistor.Polls.Exists(p => p.Type == PollType.Werewolf));
		}
		[Test]
		public void WhenWerewolfPollHasNoVotesShouldPresentNoKill() {
			phase.StateHasChanged();
			WerewolfKillEvent gameEvent = (WerewolfKillEvent) mockPresentor.visibleEvents.Find(e => e.Type == EventType.WerewolfKill);
			Assert.IsNull(gameEvent.VictimName);
		}
		[Test]
		public void WhenWerewolfPollHasNoVotesShouldNotKill() {
			phase.StateHasChanged();
			Assert.IsTrue(mockPersistor.GetAllPlayers().TrueForAll(p => p.Role.Name != RoleName.Spectator));
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
			Assert.IsFalse(mockPersistor.Polls.Exists(p => p.Type == PollType.Werewolf));
		}
		[Test]
		public void WhenWerewolfPollHasTiedVotesShouldPresentNoKill() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, seer.Name);
			phase.StateHasChanged();
			WerewolfKillEvent gameEvent = (WerewolfKillEvent) mockPresentor.visibleEvents.Find(e => e.Type == EventType.WerewolfKill);
			Assert.IsNull(gameEvent.VictimName);
		}
		[Test]
		public void WhenWerewolfPollHasTiedVotesShouldNotKill() {
			werewolfpoll.PlaceVote(werewolf1, villager.Name);
			werewolfpoll.PlaceVote(werewolf2, seer.Name);
			phase.StateHasChanged();
			Assert.IsTrue(mockPersistor.GetAllPlayers().TrueForAll(p => p.Role.Name != RoleName.Spectator));
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
			Assert.IsFalse(mockPersistor.Polls.Exists(p => p.Type == PollType.Werewolf));
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
			WerewolfKillEvent gameEvent = mockPresentor.visibleEvents.Find(e => e.Type == EventType.WerewolfKill) as WerewolfKillEvent;
			Assert.AreEqual(villager.Name, gameEvent.VictimName);
		}
	}
}