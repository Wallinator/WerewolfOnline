using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;
using WerewolfDomainTests.PhaseTests.Shared;

namespace WerewolfDomainTests.PhaseTests {
	internal class DiscussionPhaseTests : PollPhaseTests {


		protected override PhaseType PhaseType => PhaseType.Discussion;
		protected override Poll SamplePoll => new Poll(mockPersister.GetAllPlayers().FindAll(p => p.Role.Name != RoleName.Spectator),
														mockPresenter.GetDiscussionPollOptions(),
														PollType.Ready);
	}
}