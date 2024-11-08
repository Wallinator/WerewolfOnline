﻿using System.Collections.Generic;
using WerewolfDomain.Structures;
using WerewolfDomain.Structures.GameEvents;

namespace WerewolfDomain.Interfaces {
	public interface Presenter {
		void ShowPoll(List<Player> players, List<string> options);
		void HidePolls();
		void ShowEvent(GameEvent gameEvent);
		IEnumerable<string> GetDiscussionPollOptions();
		IEnumerable<string> GetIntroductionPollOptions();
		IEnumerable<string> GetSleepPollOptions();
	}
}
