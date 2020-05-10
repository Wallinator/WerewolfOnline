namespace WerewolfDomain.Roles {
	public class Spectator : Role {
		public Role OldRole;
		public Spectator() {
			OldRole = new Role();
			Name = RoleName.Spectator;
		}
		public Spectator(Role oldRole) {
			OldRole = oldRole;
			Name = RoleName.Spectator;
		}
	}
}
