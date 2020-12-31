using System.ComponentModel;

namespace EDMats.ViewModels
{
    public interface IAsyncState : INotifyPropertyChanged
    {
        bool IsBusy { get; }

        bool IsReady { get; }
    }
}