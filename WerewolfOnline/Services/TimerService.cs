using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WerewolfOnline.Services {
	public static class TimerService {
		public static CancellationTokenSource StartTimer(int timerMs, Action OnCancel, Action OnTimeOut) {

			CancellationTokenSource source = new CancellationTokenSource();
			if (timerMs > 0) {
				StartTimerAwaitingCancel(timerMs, OnCancel, OnTimeOut, source);
			}
			else {
				StartAwaitingCancel(OnCancel, source);
			}
			return source;
		}

		private static void StartAwaitingCancel(Action OnCancel, CancellationTokenSource source) {
			_ = Task.Run(() => {
				while (true) {
					if (source.Token.IsCancellationRequested) {
						OnCancel();
						return;
					}
				}
			});
		}

		private static void StartTimerAwaitingCancel(int timerMs, Action OnCancel, Action OnTimeOut, CancellationTokenSource source) {
			_ = Task.Run(() => {
				Thread.Sleep(timerMs);
				if (source.Token.IsCancellationRequested) {
					OnCancel();
					return;
				}
				else {
					OnTimeOut();
					return;
				}
			});
		}
	}
}
