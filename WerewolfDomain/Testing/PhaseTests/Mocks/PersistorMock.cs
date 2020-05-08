﻿using PhaseLibrary;
using System;
using System.Collections.Generic;
using WerewolfDomain.Entities;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;

namespace WerewolfDomainTests.PhaseTests.Mocks {
	internal class PersistorMock : Persister {

		public List<Poll> Polls = new List<Poll>();
		public void AddPoll(Poll poll) {
			Polls.Add(poll);
		}
		public Poll GetPoll(PollType type) {
			return Polls.Find(p => p.Type == type);
		}
		public void RemovePoll(PollType type) {
			Polls.RemoveAll(p => p.Type == type);
		}


		public List<Player> AllPlayers;
		public List<Player> GetAllPlayers() {
			return AllPlayers;
		}


		public Stack<Phase> NextPhases = new Stack<Phase>();
		public Phase GetNextPhase(PhaseType currentPhaseType) {
			return NextPhases.Pop();
		}
		public void AddNextPhase(Phase phase) {
			NextPhases.Push(phase);
		}

		public bool NextPhaseExists() {
			return NextPhases.Count != 0;
		}


		public bool PhaseSetup = false;

		public PersistorMock() {
			Player seer = new Player("1", "abby");
			Player werewolf1 = new Player("2", "bob");
			Player werewolf2 = new Player("3", "claire");
			Player villager = new Player("4", "debra");
			seer.Role = new Seer();
			werewolf1.Role = new Werewolf();
			werewolf2.Role = new Werewolf();
			villager.Role = new Villager();
			villager.IsStoryteller = true;
			AllPlayers = new List<Player>() {
				seer,
				werewolf1,
				werewolf2,
				villager,
			};
		}

		public bool IsPhaseSetup() {
			return PhaseSetup;
		}

		public void SetPhaseSetup(bool Setup) {
			PhaseSetup = Setup;
		}
		internal Dictionary<PhaseType, bool> AllPhasesExist() {
			Dictionary<PhaseType, bool> dict = new Dictionary<PhaseType, bool>();
			foreach (PhaseType type in Enum.GetValues(typeof(PhaseType))) {
				dict[type] = true;
			}
			return dict;
		}

		public void UpdatePlayer(Player player) {
			throw new NotImplementedException();
		}

		public void PlaceVote(Player player, object choice, PollType type) {
			throw new NotImplementedException();
		}
	}
}
