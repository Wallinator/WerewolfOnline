﻿using WerewolfDomain.Structures;

namespace WerewolfDomain.Interfaces {
	public interface Presentor {
		void ShowPoll(Poll poll);
		void HidePoll(Poll poll);
	}
}
