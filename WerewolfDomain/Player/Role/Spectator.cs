namespace WerewolfDomain.Player.Role {
    public class Spectator : Role {
        public Role OldRole;
        public Spectator(Role oldRole = null) {
            OldRole = oldRole;
            Name = RoleName.Spectator;
        }
    }
}
























