using NUnit.Framework;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Shared;

namespace WerewolfDomainTests.PhaseTests {
	internal class WerewolfPhaseTests : PollPhaseTests{

		protected override Poll SamplePoll => new Poll(mockPersister.GetAllPlayers().FindAll(x => x.Role.Name == RoleName.Werewolf),
														mockPersister.GetAllPlayers().FindAll(x => x.Role.Name != RoleName.Werewolf &&
																								 x.Role.Name != RoleName.Spectator)
																						.ConvertAll(player => player.Name),
														PollType.Werewolf);

		protected override PhaseType PhaseType => PhaseType.Werewolf;


		[Test]
		public void GivenPhaseResolvedPollShouldNotBeRemoved() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Werewolf);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.AreEqual(poll, mockPersister.GetPoll(PollType.Werewolf));
		}

		public override void ShouldRemovePollWhenPhaseResolved() {

		}
	}
}