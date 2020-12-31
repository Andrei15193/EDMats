using System;
using System.Threading;

namespace EDMats.ViewModels
{
    public sealed class AsyncState : ViewModel, IAsyncState
    {
        private int _busyCount;

        public bool IsBusy
            => _busyCount > 0;

        public bool IsReady
            => _busyCount == 0;

        public IDisposable BusySection()
            => new BusySectionMarker(this);

        private sealed class BusySectionMarker : IDisposable
        {
            private readonly AsyncState _state;

            public BusySectionMarker(AsyncState factory)
            {
                _state = factory;

                if (Interlocked.Increment(ref _state._busyCount) == 1)
                {
                    _state.NotifyPropertyChanged(nameof(IsBusy));
                    _state.NotifyPropertyChanged(nameof(IsReady));
                }
            }

            public void Dispose()
            {
                if (Interlocked.Decrement(ref _state._busyCount) == 0)
                {
                    _state.NotifyPropertyChanged(nameof(IsBusy));
                    _state.NotifyPropertyChanged(nameof(IsReady));
                }
            }
        }
    }
}