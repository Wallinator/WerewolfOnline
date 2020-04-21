using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Entities;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests {
	public class StoryPhaseTests {

		private Phase phase;
		private MockPersistor mockPersistor;
		private MockPresentor mockPresentor;
		private readonly Player seer = new Player("1", "abby");
		private readonly Player werewolf = new Player("2", "bob");
		private readonly Player villager = new Player("3", "claire");

		[SetUp]
		public void Setup() {
			mockPersistor = new MockPersistor();
			mockPresentor = new MockPresentor();
			PhaseFactoryImpl factory = new PhaseFactoryImpl(mockPersistor, mockPresentor, mockPersistor.AllPhasesExist());
			phase = factory.MakeFirstPhase();
			phase = factory.MakeNextPhase(phase);
			phase = factory.MakeNextPhase(phase);
			phase = factory.MakeNextPhase(phase);
			seer.Role = new Seer();
			werewolf.Role = new Werewolf();
			villager.Role = new Villager();
			mockPersistor.LivingPlayers = new List<Player>() {
				seer,
				werewolf,
				villager,
			};
			mockPersistor.PollToBeGot = new Poll(new Player[0], new object[0], PollType.Seer);
		}

		[Test]
		public void SeerPhaseShouldAddPollCorrectlyWhenSetup() {

			phase.StateHasChanged();
			Poll poll = mockPersistor.PollAdded;
			Assert.AreEqual(PollType.Seer, poll.Type);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersistor.GetLivingPlayers().FindAll(x => x.Role.Name == RoleName.Seer)));
			List<string> choicesactual = poll.Choices.ConvertAll(obj => (string)obj);
			choicesactual.Sort();
			List<string> choicesexpected = mockPersistor.GetLivingPlayers().FindAll(x => x.Role.Name != RoleName.Seer).ConvertAll(player => player.Name);
			choicesexpected.Sort();
			Assert.IsTrue(choicesactual.SequenceEqual(choicesexpected));
		}
		[Test]
		public void SeerPhaseShouldShowSeerPlayerRoleWhenResolved() {

			phase.StateHasChanged();
			mockPersistor.PollToBeGot = mockPersistor.PollAdded;
			mockPersistor.PollToBeGot.PlaceVote(seer, werewolf.Name);

			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
			Assert.AreEqual(werewolf.Name, mockPresentor.NameOfPlayerShownToSeer);
			Assert.AreEqual(seer, mockPresentor.SeerShownRole);
		}
		[Test]
		public void SeerPhaseShouldNotResolveWhenPollOpen() {
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
		}
		[Test]
		public void SeerPhaseShouldGiveNextPhaseWhenPollClosed() {
			mockPersistor.PollToBeGot.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public void SeerPhaseShouldRemovePollWhenResolved() {
			mockPersistor.PollToBeGot.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreEqual(PollType.Seer, mockPersistor.PollTypeRemoved);
			Assert.AreEqual(mockPersistor.PollToBeGot, mockPresentor.PollHidden);
		}
	}
}