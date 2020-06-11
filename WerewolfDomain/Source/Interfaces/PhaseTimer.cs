using System;
using System.Collections.Generic;
using System.Text;

namespace WerewolfDomain.Interfaces {
	public interface PhaseTimer {
		void Start(int seconds);
		void Cancel();
	}
}
