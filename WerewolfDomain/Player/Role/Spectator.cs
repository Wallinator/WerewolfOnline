namespace WerewolfDomain.Roles {
    public class Spectator : Role {
        public Role OldRole;
        public Spectator(Role oldRole = null) {
            OldRole = oldRole;
            Name = RoleName.Spectator;
        }
    }
}
























