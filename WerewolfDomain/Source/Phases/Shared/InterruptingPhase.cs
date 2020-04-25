using PhaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace WerewolfDomain.Phases.Shared {
	public interface InterruptingPhase {
		PhaseType PhaseInterrupted { get; }
	}
}
