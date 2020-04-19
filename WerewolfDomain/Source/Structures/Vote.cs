using System;
using System.Collections.Generic;
using WerewolfDomain.Entities;

namespace WerewolfDomain.Structures {
	public readonly struct Vote {
		public readonly Player Voter { get; }
		public readonly object Choice { get; }
		public Vote(Player voter, object choice) {
			Voter = voter;
			Choice = choice;
		}

		public override int GetHashCode() {
			return HashCode.Combine(Voter, Choice);
		}

		public override bool Equals(object obj) {
			return obj is Vote vote &&
				   EqualityComparer<Player>.Default.Equals(Voter, vote.Voter) &&
				   EqualityComparer<object>.Default.Equals(Choice, vote.Choice);
		}
	}
}
