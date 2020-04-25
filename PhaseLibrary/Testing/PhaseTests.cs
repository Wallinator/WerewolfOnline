using NUnit.Framework;
using PhaseTests.Mocks;

namespace PhaseTests {
	public class PhaseTests {
		PhaseFactoryTestImpl factory;
		PhaseTestImpl phase;

		[SetUp]
		public void Setup() {
			factory = new PhaseFactoryTestImpl();
			phase = ConstructPhase(0, true);
		}
		private PhaseTestImpl ConstructPhase(int id, bool shouldResolve) {
			return new PhaseTestImpl(factory, id, shouldResolve);
		}
		[Test]
		public void CanResolveCalled() {
			phase.StateHasChanged();
			Assert.True(phase.CanResolveCalled);
		}
		[Test]
		public void PhaseSetUpCalled() {
			phase.StateHasChanged();
			Assert.True(phase.PhaseSetUpCalled);
		}
		[Test]
		public void PhaseResolved() {
			phase.StateHasChanged();
			Assert.True(phase.PhaseResolveCalled);
		}
		[Test]
		public void PhaseNotResolved() {
			phase = ConstructPhase(0, false);
			phase.StateHasChanged();
			Assert.False(phase.PhaseResolveCalled);
		}
		[Test]
		public void PreResolveCalledTest() {
			phase = ConstructPhase(0, false);
			phase.StateHasChanged();
			Assert.False(phase.PreForceResolveCalled);
			phase.ForceResolve();
			Assert.True(phase.PreForceResolveCalled);
		}
		[Test]
		public void NextPhaseReturnedWhenResolved() {
			int oldId = phase.Id;
			phase = (PhaseTestImpl)phase.StateHasChanged();
			Assert.AreEqual(oldId + 1, phase.Id);
		}
		[Test]
		public void NextTwoPhasesReturnedWhenResolvedTwice() {
			int oldId = phase.Id;
			phase = (PhaseTestImpl)phase.StateHasChanged();
			phase = (PhaseTestImpl)phase.StateHasChanged();
			Assert.AreEqual(oldId + 2, phase.Id);
		}
		[Test]
		public void NextPhaseReturnedWhenForceResolved() {
			int oldId = phase.Id;
			phase = (PhaseTestImpl)phase.ForceResolve();
			Assert.AreEqual(oldId + 1, phase.Id);
		}
	}
}