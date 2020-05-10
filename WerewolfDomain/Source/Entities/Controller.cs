using PhaseLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Entities {
	public class Controller {
		private readonly Persister persister;
		private readonly Presenter presenter;
		private readonly PhaseFactoryImpl factory;
		private readonly Action<object, int> StartTimer;
		private readonly Action<object> CancelTimer;
		private readonly object Timer;

		public Controller(Persister persister, Presenter presenter, PhaseFactoryImpl factory, Action<object, int> startTimer, Action<object> cancelTimer, object timer) {
			this.persister = persister;
			this.presenter = presenter;
			this.factory = factory;
			StartTimer = startTimer;
			CancelTimer = cancelTimer;
			Timer = timer;
		}

		private AbstractPhase Phase {
			get {
				if (Phase.Equals(null)) {
					Phase = factory.ConstructPhase(persister.GetCurrentPhaseType()) as AbstractPhase;
				}
				return Phase;
			}
			set {
				CancelTimer(Timer);
				if (!ReferenceEquals(Phase, value)) {
					Phase = value;
					Phase.SetUp();
					StartTimer(Timer, Phase.DefaultDurationSeconds);
				}
			}
		}
		public void PlaceVote(Player player, object choice, PollType type) {
			persister.PlaceVote(player, choice, type);
			StateHasChanged();
		}
		public void ForceResolve() {
			Phase = Phase.ForceResolve() as AbstractPhase;
		}
		public void StateHasChanged() {
			Phase = Phase.StateHasChanged() as AbstractPhase;
		}
	}
}
