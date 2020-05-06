using System;
using WerewolfDomain.Roles;

namespace WerewolfDomain.Structures {
	public class Player {
		public string Id;
		public string Name;
		public bool IsStoryteller = false;
		public Role Role = new Role();
		public Player(string id, string name) {
			Id = id;
			Name = name;
		}
		public Player(Player player) : this(player.Id, player.Name) {
			Role = player.Role;
			IsStoryteller = player.IsStoryteller;
		}

		public void SetRole(RoleName name) {
			Role = RoleFactory.MakeRole(name);
		}

		public override bool Equals(object obj) {
			return obj is Player player &&
				   Id == player.Id &&
				   Name == player.Name;
		}

		public override int GetHashCode() {
			return HashCode.Combine(Id, Name);
		}
	}
}

























