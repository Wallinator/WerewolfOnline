using System;
using WerewolfDomain.Exceptions;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases.Shared {
	internal static class PollResolver {
		public static void Resolve(Poll poll, Persistor persistor, Presentor presentor) {
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
				default:
					throw new InvalidPollTypeException();
			}
		}
		private static void ResolveReady(Poll poll, Persistor persistor, Presentor presentor) {
			persistor.RemovePoll(poll.Type);
			presentor.HidePoll(poll);
			return;
		}
		private static void ResolveWerewolf(Poll poll, Persistor persistor, Presentor presentor) {
			throw new NotImplementedException();
		}

		private static void ResolveVillager(Poll poll, Persistor persistor, Presentor presentor) {
			throw new NotImplementedException();
		}

		private static void ResolveSeer(Poll poll, Persistor persistor, Presentor presentor) {
			throw new NotImplementedException();
		}

		private static void ResolveSleep(Poll poll, Persistor persistor, Presentor presentor) {
			throw new NotImplementedException();
		}


	}
}