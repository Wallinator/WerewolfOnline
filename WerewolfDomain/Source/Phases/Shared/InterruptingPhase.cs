namespace WerewolfDomain.Phases.Shared {
	internal interface InterruptingPhase {
		PhaseType PhaseInterrupted {
			get;
		}
	}
}
