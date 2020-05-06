using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests.StoryPhase {
	public class StoryPhaseTests {
		public static List<PollType> PollsResolvedByStoryPhase = new List<PollType> { PollType.Werewolf };


		private Phase phase;
		private PersistorMock mockPersistor;
		private PresentorMock mockPresentor;
		private readonly Player storyteller = new Player("3", "claire");
		PhaseFactoryImpl factory;


		[SetUp]
		public void Setup() {
			mockPersistor = new PersistorMock();
			mockPresentor = new PresentorMock();
			factory = new PhaseFactoryImpl(mockPersistor, mockPresentor, mockPersistor.AllPhasesExist());
			phase = factory.ConstructPhase(PhaseType.Story);
			storyteller.IsStoryteller = true;
			List<Player> players = new List<Player>() {
					storyteller
				};
			mockPersistor.AllPlayers = players;
		}

		[Test]
		public void WhenPollAddedShouldBePresented() {
			phase.SetUp();
			Poll poll = mockPersistor.GetPoll(PollType.Storyteller);
			Assert.AreEqual(poll, mockPresentor.PollShown);
		}
		[Test]
		public void PollAddedShouldBeTypeStoryteller() {
			phase.SetUp();
			Poll poll = mockPersistor.GetPoll(PollType.Storyteller);
			Assert.AreEqual(PollType.Storyteller, poll.Type);
		}
		[Test]
		public void PollAddedShouldBeForStoryteller() {
			phase.SetUp();
			Poll poll = mockPersistor.GetPoll(PollType.Storyteller);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersistor.GetAllPlayers().FindAll(x => x.IsStoryteller)));
		}
		[Test, Combinatorial]
		public void PollAddedShouldHaveChoicesPollsToResolve([Values] bool werewolf) {
			if (!werewolf) {
				return;
			}
			AddPollsToMockPersistor(PollsForStoryPhase(werewolf));
			phase.SetUp();
			Poll poll = mockPersistor.GetPoll(PollType.Storyteller);
			List<PollType> choicesactual = poll.Choices.ConvertAll(obj => (PollType) obj);
			choicesactual.Sort();
			List<PollType> choicesexpected = PollsForStoryPhase(werewolf).ConvertAll(p => p.Type);
			choicesexpected.Sort();
			Assert.IsTrue(choicesactual.SequenceEqual(choicesexpected));
		}

		private void AddPollsToMockPersistor(List<Poll> list) {
			for (int i = 0; i < list.Count; i++) {
				Poll p = list[i];
				mockPersistor.AddPoll(p);
			}
		}

		private List<Poll> PollsForStoryPhase(bool werewolf) {
			List<Poll> list = new List<Poll>();
			if (werewolf) {
				list.Add(new Poll(new List<Player>(), new object[0], PollType.Werewolf));
			}
			return list;
		}
	}
}