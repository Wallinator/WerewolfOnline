using System;
using System.Collections.Generic;
using System.Text;

namespace WerewolfDomain.Roles {
    internal static class RoleFactory {

        internal static Role MakeRole(RoleName name) {
            return name switch
            {
                RoleName.None => new Role(),
                RoleName.Spectator => new Spectator(),
                RoleName.Villager => new Villager(),
                RoleName.Werewolf => new Werewolf(),
                RoleName.Seer => new Seer(),
                _ => null,
            };
        }
    }

    public enum RoleName {
        //None must be on top
        None,
        Spectator,
        Villager,
        Werewolf,
        Seer
    }
}
