using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Shared;

namespace WerewolfDomainTests.PhaseTests {
	internal class BedtimePhaseTests : PollPhaseTests {

		protected override PhaseType PhaseType => PhaseType.Bedtime;

		protected override Poll SamplePoll => new Poll(mockPersister.GetAllPlayers().FindAll(p => p.Role.Name != RoleName.Spectator),
														mockPresenter.GetSleepPollOptions(),
														PollType.Ready);
	}
}