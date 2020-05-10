using System;
using WerewolfDomain.Roles;

namespace WerewolfDomain.Structures {
	public class Player {
		public string Name;
		public bool IsStoryteller = false;
		public Role Role = new Role();
		public Player(string name) {
			Name = name;
		}
		public Player(Player player) : this(player.Name) {
			Role = player.Role;
			IsStoryteller = player.IsStoryteller;
		}

		public void SetRole(RoleName name) {
			Role = RoleFactory.MakeRole(name);
		}

		public override bool Equals(object obj) {
			return obj is Player player &&
				   Name == player.Name;
		}

		public override int GetHashCode() {
			return HashCode.Combine(Name);
		}
	}
}

























