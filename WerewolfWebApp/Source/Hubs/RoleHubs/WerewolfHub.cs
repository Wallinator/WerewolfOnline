﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WerewolfWebApp.Model;
using WerewolfWebApp.Services;

namespace WerewolfWebApp.Hubs {
    public partial class PlayerHub : Hub<IPlayerHub> {

    }

    public static partial class PlayerHubMethod {
        //public static readonly string Relog = "Relog";
    }
    public static partial class PlayerClientMethod {
        //public static readonly string ReceivePoll = "ReceivePoll";
    }
    public partial interface IPlayerHub {
        //Task ReceivePoll(PollType type, List<string> nominees);
    }
}