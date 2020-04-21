using System;
using System.Collections.Generic;
using System.Text;

namespace WerewolfDomain.Phases.Shared {
	internal interface InterruptingPhase {
		PhaseType PhaseInterrupted { get; }
	}
}
