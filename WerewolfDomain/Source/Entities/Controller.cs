using System;
using WerewolfDomain.Interfaces;
using WerewolfDomain.Interfaces.Persisters;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Structures;

namespace WerewolfDomain.Entities {
	public class Controller {
		private readonly Persister persister;
		private readonly Presenter presenter;
		private readonly PhaseFactoryImpl factory;
		private readonly PhaseTimer Timer;

		public Controller(Persister persister, Presenter presenter, PhaseFactoryImpl factory, PhaseTimer timer) {
			this.persister = persister;
			this.presenter = presenter;
			this.factory = factory;
			Timer = timer;
		}

		private AbstractPhase Phase {
			get {
				if (Phase.Equals(null)) {
					Phase = (AbstractPhase) factory.ConstructPhase(persister.GetCurrentPhaseType()) ;
				}
				return Phase;
			}
			set {
				if (!ReferenceEquals(Phase, value)) {
					Timer.Cancel();
					Phase = value;
					Phase.SetUp();
					Timer.Start(Phase.DefaultDurationSeconds);
				}
			}
		}
		public void PlaceVote(Player player, object choice, PollType type) {
			persister.PlaceVote(player, choice, type);
			StateHasChanged();
		}
		public void ForceResolve() {
			Phase = (AbstractPhase) Phase.ForceResolve();
		}
		public void StateHasChanged() {
			Phase = (AbstractPhase) Phase.StateHasChanged();
		}
	}
}
