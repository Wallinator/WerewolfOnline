using NUnit.Framework;
using PhaseLibrary;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;
using WerewolfDomainTests.PhaseTests.Shared;

namespace WerewolfDomainTests.PhaseTests {
	internal class IntroductionPhaseTests : PollPhaseTests {

		protected override PhaseType PhaseType => PhaseType.Introduction;

		protected override Poll SamplePoll => new Poll(	mockPersister.GetAllPlayers().FindAll(p => p.Role.Name!= RoleName.Spectator),
														mockPresenter.GetIntroductionPollOptions(),
														PollType.Ready);
	}
}