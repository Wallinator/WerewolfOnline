using System;
using System.Collections.Generic;
using System.Text;
using WerewolfDomain.Entities;

namespace WerewolfDomain.Structures {
	public readonly struct Vote<T> {
		public readonly Player Voter { get; }
		public readonly T Choice { get; }
		public Vote(Player voter, T choice) {
			Voter = voter;
			Choice = choice;
		}
	}
}
