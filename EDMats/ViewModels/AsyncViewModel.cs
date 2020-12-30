using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

namespace EDMats.ViewModels
{
    public class AsyncViewModel : ViewModel
    {
        private int _busyCount = 0;

        protected AsyncViewModel()
            : base()
        {
        }

        protected AsyncViewModel(IEnumerable<INotifyPropertyChanged> chainedObservables)
            : base(chainedObservables)
        {
        }

        protected AsyncViewModel(params INotifyPropertyChanged[] chainedObservables)
            : base(chainedObservables)
        {
        }

        public bool IsBusy
            => _busyCount > 0;

        public bool IsReady
            => !IsBusy;

        protected IDisposable BusySection()
            => new BusyState(this);

        private sealed class BusyState : IDisposable
        {
            private readonly AsyncViewModel _asyncViewModel;

            public BusyState(AsyncViewModel asyncViewModel)
            {
                _asyncViewModel = asyncViewModel;

                if (Interlocked.Increment(ref _asyncViewModel._busyCount) == 1)
                {
                    _asyncViewModel.NotifyPropertyChanged(nameof(IsBusy));
                    _asyncViewModel.NotifyPropertyChanged(nameof(IsReady));
                }
            }

            public void Dispose()
            {
                if (Interlocked.Decrement(ref _asyncViewModel._busyCount) == 0)
                {
                    _asyncViewModel.NotifyPropertyChanged(nameof(IsBusy));
                    _asyncViewModel.NotifyPropertyChanged(nameof(IsReady));
                }
            }
        }
    }
}