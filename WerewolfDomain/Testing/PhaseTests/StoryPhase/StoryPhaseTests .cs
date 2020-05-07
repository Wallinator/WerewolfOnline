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
		private PersistorMock mockPersister;
		private PresentorMock mockPresenter;
		private readonly Player storyteller = new Player("3", "claire");
		PhaseFactoryImpl factory;


		[SetUp]
		public void Setup() {
			mockPersister = new PersistorMock();
			mockPresenter = new PresentorMock();
			factory = new PhaseFactoryImpl(mockPersister, mockPresenter, mockPersister.AllPhasesExist());
			phase = factory.ConstructPhase(PhaseType.Story);
			storyteller.IsStoryteller = true;
			List<Player> players = new List<Player>() {
					storyteller
				};
			mockPersister.AllPlayers = players;
		}

		[Test]
		public void WhenPollAddedShouldBePresented() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Storyteller);
			Assert.AreEqual(poll, mockPresenter.PollShown);
		}
		[Test]
		public void PollAddedShouldBeTypeStoryteller() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Storyteller);
			Assert.AreEqual(PollType.Storyteller, poll.Type);
		}
		[Test]
		public void PollAddedShouldBeForStoryteller() {
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Storyteller);
			Assert.IsTrue(poll.Voters.SetEquals(mockPersister.GetAllPlayers().FindAll(x => x.IsStoryteller)));
		}
		[Test, Combinatorial]
		public void PollAddedShouldHaveChoicesPollsToResolve([Values] bool werewolf) {

			if (!werewolf) {
				return;
			}

			AddPollsToMockPersistor(PollsForStoryPhase(werewolf));
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Storyteller);


			List<PollType> choicesactual = poll.Choices.ConvertAll(obj => (PollType) obj);
			choicesactual.Sort();
			List<PollType> choicesexpected = PollsForStoryPhase(werewolf).ConvertAll(p => p.Type);
			choicesexpected.Sort();
			Assert.IsTrue(choicesactual.SequenceEqual(choicesexpected));
		}
		private void AddPollsToMockPersistor(List<Poll> list) {
			for (int i = 0; i < list.Count; i++) {
				Poll p = list[i];
				mockPersister.AddPoll(p);
			}
		}

		private List<Poll> PollsForStoryPhase(bool werewolf) {
			List<Poll> list = new List<Poll>();
			if (werewolf) {
				list.Add(ConstructPoll(PollType.Werewolf));
			}
			return list;
		}

		private static Poll ConstructPoll(PollType type) {
			return type switch
			{
				PollType.Werewolf => new Poll(new List<Player>(), new object[0], PollType.Werewolf),
				_ => throw new System.NotImplementedException()
			};
		}

		[Test, Combinatorial]
		public void ShouldResolveWhenLastPoll([Values(PollType.Werewolf)] PollType type) {

			mockPersister.AddPoll(ConstructPoll(type));
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Storyteller);
			poll.PlaceVote(storyteller, type);
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test, Combinatorial]
		public void ShouldHidePollWhenResolved([Values(PollType.Werewolf)] PollType type) {

			mockPersister.AddPoll(ConstructPoll(type));
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Storyteller);
			poll.PlaceVote(storyteller, type);
			phase.StateHasChanged();
			Assert.IsTrue(mockPresenter.PollHidden);
		}
	}
}