using System;
using WerewolfDomain.Exceptions;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Phases {
	public static class Resolver {
		public static void ResolvePoll<T>(Poll<T> poll, Persistor persistor, Presentor presentor) {
			switch (poll.Type) {
				case PollType.Werewolf:
					ResolveWerewolf();
					break;
				default:
					throw new InvalidPollTypeException();
			}
		}

		private static void ResolveWerewolf() {
			throw new NotImplementedException();
		}
	}
}