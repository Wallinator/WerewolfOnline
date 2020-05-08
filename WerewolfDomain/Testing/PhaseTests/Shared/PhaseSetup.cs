using NUnit.Framework;
using PhaseLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WerewolfDomain.Phases.Shared;
using WerewolfDomain.Roles;
using WerewolfDomain.Structures;
using WerewolfDomainTests.PhaseTests.Mocks;

namespace WerewolfDomainTests.PhaseTests.Shared {
	internal abstract class PhaseSetup {
		protected Phase phase;
		protected internal PersistorMock mockPersister;
		protected PresentorMock mockPresenter;
		protected Player seer;
		protected Player werewolf1;
		protected Player werewolf2;
		protected Player villager;
		protected PhaseFactoryImpl factory;

		[SetUp]
		public void BaseSetup() {
			mockPersister = new PersistorMock();
			mockPresenter = new PresentorMock();
			seer = mockPersister.AllPlayers.Find(p => p.Role.Name == RoleName.Seer);
			villager = mockPersister.AllPlayers.Find(p => p.Role.Name == RoleName.Villager);
			List<Player> wolves = mockPersister.AllPlayers.FindAll(p => p.Role.Name == RoleName.Werewolf);
			werewolf1 = wolves.First();
			werewolf2 = wolves.ElementAt(1);
			factory = new PhaseFactoryImpl(mockPersister, mockPresenter, mockPersister.AllPhasesExist());
		}
	}
}
