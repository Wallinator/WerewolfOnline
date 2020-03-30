using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WerewolfOnline.Services {
	public static class TimerService {
		public static CancellationTokenSource StartTimer(int timerMs, Action OnCancel, Action OnTimeOut) {

			var source = new CancellationTokenSource();
			_ = Task.Run(() => {
				if (timerMs > 0) {
					Thread.Sleep(timerMs);
					if (source.Token.IsCancellationRequested) {
						OnCancel();
						return;
					}
					else {
						OnTimeOut();
						return;
					}
				}

				else {
					while (true) {
						if (source.Token.IsCancellationRequested) {
							OnCancel();
							return;
						}
					}
				}
			});
			return source;
		}
	}
}
