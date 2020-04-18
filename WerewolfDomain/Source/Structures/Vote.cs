using System;
using System.Collections.Generic;
using WerewolfDomain.Entities;

namespace WerewolfDomain.Structures {
	public readonly struct Vote<T> {
		public readonly Player Voter { get; }
		public readonly T Choice { get; }
		public Vote(Player voter, T choice) {
			Voter = voter;
			Choice = choice;
		}

		public override bool Equals(object obj) {
			return obj is Vote<T> vote &&
				   EqualityComparer<Player>.Default.Equals(Voter, vote.Voter) &&
				   EqualityComparer<T>.Default.Equals(Choice, vote.Choice);
		}

		public override int GetHashCode() {
			return HashCode.Combine(Voter, Choice);
		}
	}
}
