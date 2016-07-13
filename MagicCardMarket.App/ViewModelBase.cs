using System;
using System.Threading;
using System.Windows;
using MagicCardMarket.Log;
using MagicCardMarket.MVVM;

namespace MagicCardMarket.App
{
    public class ViewModelBase : ObservableObject
    {
        private int _busyCount;

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            private set { Set(() => IsBusy, ref _isBusy, value); }
        }

        public void ShowWaitingScreen()
        {
            Interlocked.Increment(ref _busyCount);
            IsBusy = true;
        }

        public void HideWaitingScreen()
        {
            int newValue = Interlocked.Decrement(ref _busyCount);
            if (newValue == 0)
                IsBusy = false;
        }

        public void OnError(bool isFatal, Exception ex)
        {
            Log.Log.Default.WriteLine(LogLevels.Error, ex.ToString());
            // TODO: better error popup
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            if (isFatal)
                Application.Current.Shutdown();
        }
    }
}
