using NUnit.Framework;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;
using WerewolfDomainTests.PhaseTests.Shared;

namespace WerewolfDomainTests.PhaseTests {
	internal class JuryPhaseTests : PollPhaseTests {

		protected override Poll SamplePoll => new Poll(mockPersister.GetAllPlayers().FindAll(x => x.Role.Name != RoleName.Spectator),
														mockPersister.GetAllPlayers().FindAll(x => x.Role.Name != RoleName.Spectator)
																					 .ConvertAll(player => player.Name),
														PollType.Jury);

		protected override PhaseType PhaseType => PhaseType.Jury;
		private Poll juryPoll;

		[SetUp]
		public void Setup() {
			juryPoll = mockPersister.GetPoll(PollType.Jury);
		}

		[Test]
		public void WhenPollHasNoVotesShouldPresentNoExecute() {
			juryPoll.ClosePoll();
			phase.StateHasChanged();
			JuryExecutionEvent gameEvent = (JuryExecutionEvent) mockPresenter.visibleEvents.Find(e => e.Type == EventType.JuryExecution);
			Assert.IsNull(gameEvent.VictimName);
		}
		[Test]
		public void WhenPollHasNoVotesShouldNotExecute() {
			juryPoll.ClosePoll();
			phase.StateHasChanged();
			Assert.IsTrue(mockPersister.GetAllPlayers().TrueForAll(p => p.Role.Name != RoleName.Spectator));
		}


		[Test]
		public void WhenPollHasTiedVotesShouldPresentNoExecute() {
			juryPoll.PlaceVote(werewolf1, villager.Name);
			juryPoll.PlaceVote(werewolf2, seer.Name);
			juryPoll.ClosePoll();
			phase.StateHasChanged();
			JuryExecutionEvent gameEvent = (JuryExecutionEvent) mockPresenter.visibleEvents.Find(e => e.Type == EventType.JuryExecution);
			Assert.IsNull(gameEvent.VictimName);
		}
		[Test]
		public void WhenPollHasTiedVotesShouldNotExecute() {
			juryPoll.PlaceVote(werewolf1, villager.Name);
			juryPoll.PlaceVote(werewolf2, seer.Name);
			juryPoll.ClosePoll();
			phase.StateHasChanged();
			Assert.IsTrue(mockPersister.GetAllPlayers().TrueForAll(p => p.Role.Name != RoleName.Spectator));
		}

		[Test]
		public void WhenPollHasValidVictimShouldExecute() {
			juryPoll.PlaceVote(werewolf1, villager.Name);
			juryPoll.PlaceVote(werewolf2, villager.Name);
			juryPoll.ClosePoll();
			phase.StateHasChanged();
			Assert.AreEqual(RoleName.Spectator, villager.Role.Name);
		}
		[Test]
		public void WhenPollHasValidVictimShouldPresentExecute() {
			juryPoll.PlaceVote(werewolf1, villager.Name);
			juryPoll.PlaceVote(werewolf2, villager.Name);
			juryPoll.ClosePoll();
			phase.StateHasChanged();
			JuryExecutionEvent gameEvent = mockPresenter.visibleEvents.Find(e => e.Type == EventType.JuryExecution) as JuryExecutionEvent;
			Assert.AreEqual(villager.Name, gameEvent.VictimName);
		}
	}
}