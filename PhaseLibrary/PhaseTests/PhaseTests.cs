using NUnit.Framework;
using PhaseTests.Mocks;

namespace PhaseTests {
	public class PhaseTests {
		PhaseFactoryTestImpl factory;
		bool SetPhaseCalled = false;
		PhaseTestImpl phase;

		[SetUp]
		public void Setup() {
			factory = new PhaseFactoryTestImpl();
			phase = ConstructPhase(0, 0, true);
		}
		private PhaseTestImpl ConstructPhase(int id, int duration, bool shouldResolve) {
			return new PhaseTestImpl(factory, (phase) => { SetPhaseCalled = true; }, duration, id, shouldResolve);
		}
		[Test]
		public void CanResolveCalled() {
			phase.SetUp();
			phase.StateHasChanged();
			Assert.True(phase.CanResolveCalled);
		}
		[Test]
		public void PhaseSetUpCalled() {
			phase.SetUp();
			Assert.True(phase.PhaseSetUpCalled);
		}
		[Test]
		public void PhaseResolved() {
			phase.SetUp();
			phase.StateHasChanged();
			Assert.True(phase.PhaseResolveCalled);
		}
		[Test]
		public void PhaseNotResolved() {
			phase = ConstructPhase(0, 0, false);
			phase.SetUp();
			phase.StateHasChanged();
			Assert.False(phase.PhaseResolveCalled);
		}
		[Test]
		public void PreResolveCalledTest() {
			phase = ConstructPhase(0, 1000, true);
			phase.SetUp();
			Assert.False(phase.PreForceResolveCalled);
			System.Threading.Thread.Sleep(1050);
			Assert.True(phase.PreForceResolveCalled);
		}
	}
}