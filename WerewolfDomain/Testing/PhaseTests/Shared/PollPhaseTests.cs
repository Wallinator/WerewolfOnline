﻿using NUnit.Framework;
using PhaseLibrary;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.PhaseTests.Shared {
	internal abstract class PollPhaseTests : PhaseSetup {
		private PollType PollType;
		private HashSet<Player> PollVoters;
		private IEnumerable PollChoices;
		protected abstract Poll SamplePoll {
			get;
		}
		protected abstract PhaseType PhaseType {
			get;
		}


		[SetUp]
		public void PollPhaseSetup() {
			phase = factory.ConstructPhase(PhaseType);
			Poll p = SamplePoll;
			PollType = p.Type;
			PollVoters = p.Voters;
			PollChoices = p.Choices;
			phase.SetUp();
		}

		[Test]
		public virtual void WhenPollAddedShouldBePresented() {
			Assert.IsTrue(mockPresenter.PollShown);
		}
		[Test]
		public virtual void WhenPollAddedShouldBePresentedToCorrectPlayers() {
			Poll poll = mockPersister.GetPoll(PollType);
			Assert.IsTrue(mockPresenter.PlayersShownPoll.SequenceEqual(poll.Voters));
		}
		[Test]
		public virtual void WhenPollAddedShouldBeTypeReady() {
			Poll poll = mockPersister.GetPoll(PollType);
			Assert.AreEqual(PollType, poll.Type);
		}
		[Test]
		public virtual void WhenPollAddedShouldBeForAllLivingPlayers() {
			Poll poll = mockPersister.GetPoll(PollType);
			Assert.IsTrue(poll.Voters.SetEquals(PollVoters));
		}
		[Test]
		public virtual void WhenPollAddedShouldHaveChoices() {
			Poll poll = mockPersister.GetPoll(PollType);
			Assert.AreEqual(PollChoices, poll.Choices);
		}

		[Test]
		public virtual void GivenPollOpenPhaseShouldNotResolve() {
			Phase newPhase = phase.StateHasChanged();
			Assert.AreSame(phase, newPhase);
		}

		[Test]
		public virtual void GivenPollClosedPhaseShouldResolve() {
			Poll poll = mockPersister.GetPoll(PollType);
			poll.ClosePoll();
			Phase newPhase = phase.StateHasChanged();
			Assert.AreNotSame(phase, newPhase);
		}
		[Test]
		public virtual void WhenPhaseResolvedShouldHidePoll() {
			Poll poll = mockPersister.GetPoll(PollType);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.AreEqual(0, mockPresenter.PlayersShownPoll.Count);
		}
		[Test]
		public virtual void ShouldRemovePollWhenPhaseResolved() {
			Poll poll = mockPersister.GetPoll(PollType);
			poll.ClosePoll();
			phase.StateHasChanged();
			Assert.AreEqual(0, mockPersister.Polls.Count);
		}
	}
}
