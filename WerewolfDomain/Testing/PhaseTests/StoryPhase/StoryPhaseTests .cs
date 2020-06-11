using NUnit.Framework;
using PhaseLibrary;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Shared;

namespace WerewolfDomainTests.PhaseTests.StoryPhase {
	internal class StoryPhaseTests : PollPhaseTests {

		private Player storyteller;

		protected override Poll SamplePoll => new Poll(mockPersister.GetAllPlayers().FindAll(p => p.IsStoryteller), new object[0], PollType.Storyteller);

		protected override PhaseType PhaseType => PhaseType.Story;

		[SetUp]
		public void Setup() {

			storyteller = villager;
		}

		[Test, Combinatorial]
		public void PollAddedShouldHaveChoicesPollsToResolve([Values] bool werewolf) {

			if (!werewolf) {
				return;
			}

			mockPersister.Polls.Clear();
			mockPersister.SetPhaseSetup(false);
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

			mockPersister.Polls.Clear();
			mockPersister.SetPhaseSetup(false);
			mockPersister.AddPoll(ConstructPoll(type));
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Storyteller);
			poll.PlaceVote(storyteller, type);
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}

		[Test, Combinatorial]
		public void ShouldHidePollWhenResolved([Values(PollType.Werewolf)] PollType type) {

			mockPersister.Polls.Clear();
			mockPersister.SetPhaseSetup(false);
			mockPersister.AddPoll(ConstructPoll(type));
			phase.SetUp();
			Poll poll = mockPersister.GetPoll(PollType.Storyteller);
			poll.PlaceVote(storyteller, type);
			phase.StateHasChanged();
			Assert.AreEqual(0, mockPresenter.PlayersShownPoll.Count);
		}

		public override void GivenPollClosedPhaseShouldResolve() {
		}
		public override void WhenPhaseResolvedShouldHidePoll() {
		}
		public override void ShouldRemovePollWhenPhaseResolved() {
		}
	}
}