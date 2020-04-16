﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WerewolfDomain.Structure;

namespace WerewolfDomain.Interfaces {
	public interface Presentor {
		void ShowPoll(List<Player> livingPlayers, Poll poll);
	}
}
