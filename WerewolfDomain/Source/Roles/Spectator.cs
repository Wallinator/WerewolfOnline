namespace WerewolfDomain.Roles {
	internal class Spectator : Role {
		internal Role OldRole;
		internal Spectator() {
			OldRole = new Role();
			Name = RoleName.Spectator;
		}
		internal Spectator(Role oldRole) {
			OldRole = oldRole;
			Name = RoleName.Spectator;
		}
	}
}
