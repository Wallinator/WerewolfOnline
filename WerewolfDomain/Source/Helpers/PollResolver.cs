﻿using System;
using System.Collections.Generic;
using System.Linq;
using WerewolfDomain.Exceptions;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;

namespace WerewolfDomain.Helpers {
	internal static class PollResolver {
		public static void Resolve(Poll poll, Persister persistor, Presentor presentor) {
			switch (poll.Type) {
				case PollType.Ready:
					ResolveReady(poll, persistor, presentor);
					break;
				case PollType.Werewolf:
					ResolveWerewolf(poll, persistor, presentor);
					break;
				case PollType.Villager:
					ResolveVillager(poll, persistor, presentor);
					break;
				case PollType.Seer:
					ResolveSeer(poll, persistor, presentor);
					break;
				case PollType.Sleep:
					ResolveSleep(poll, persistor, presentor);
					break;
				case PollType.Storyteller:
					ResolveStory(poll, persistor, presentor);
					break;
				default:
					throw new InvalidPollTypeException();
			}
		}
		private static void ResolveReady(Poll poll, Persister persistor, Presentor presentor) {
			persistor.RemovePoll(poll.Type);
			return;
		}
		private static void ResolveWerewolf(Poll poll, Persister persistor, Presentor presentor) {
			List<Player> players = persistor.GetAllPlayers();
			persistor.RemovePoll(poll.Type);
			if (poll.Winners().Count != 1) {
				WerewolfKillEvent gameEvent = new WerewolfKillEvent(players, null);
				presentor.ShowEvent(gameEvent);
				return;
			}
			string chosenName = poll.Winners().First().ToString();
			Player chosen = players.Find(x => x.Name.Equals(chosenName));
			PlayerKiller.Kill(chosen, EventType.WerewolfKill, persistor, presentor);
		}

		private static void ResolveVillager(Poll poll, Persister persistor, Presentor presentor) {
			throw new NotImplementedException();
		}

		private static void ResolveSeer(Poll poll, Persister persistor, Presentor presentor) {
			List<Player> players = persistor.GetAllPlayers();
			foreach (Player seer in poll.Voters) {
				object choice;
				SeerRevealEvent gameEvent;
				if (poll.Votes.TryGetValue(seer, out choice)) {
					Player chosen = players.Find(x => x.Name.Equals(choice));

					gameEvent = new SeerRevealEvent(new[] { seer }.ToList(), chosen.Name, chosen.Role.Name);
				}
				else {
					gameEvent = new SeerRevealEvent(new[] { seer }.ToList(), null, RoleName.None);
				}
				presentor.ShowEvent(gameEvent);
			}
			persistor.RemovePoll(poll.Type);
		}

		private static void ResolveSleep(Poll poll, Persister persistor, Presentor presentor) {
			throw new NotImplementedException();
		}
		private static void ResolveStory(Poll poll, Persister persistor, Presentor presentor) {
			persistor.RemovePoll(poll.Type);
			Poll chosenPoll = persistor.GetPoll((PollType) poll.Winners().First());
			Resolve(chosenPoll, persistor, presentor);
		}


	}
}