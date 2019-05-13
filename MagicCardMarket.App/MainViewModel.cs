using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MagicCardMarket.APIHelpers;
using MagicCardMarket.App.Buying;
using MagicCardMarket.App.Selling;
using MagicCardMarket.Models;
using MagicCardMarket.MVVM;

namespace MagicCardMarket.App
{
    public class MainViewModel : ViewModelBase
    {
        #region Account

        private int _id;
        public int Id
        {
            get { return _id; }
            set { Set(() => Id, ref _id, value); }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { Set(() => UserName, ref _userName, value); }
        }

        private decimal _accountBalance;
        public decimal AccountBalance
        {
            get { return _accountBalance; }
            set { Set(() => AccountBalance, ref _accountBalance, value); }
        }

        private decimal _bankRecharge;
        public decimal BankRecharge
        {
            get { return _bankRecharge; }
            set { Set(() => BankRecharge, ref _bankRecharge, value); }
        }

        private decimal _payPalRecharge;
        public decimal PayPalRecharge
        {
            get { return _payPalRecharge; }
            set { Set(() => PayPalRecharge, ref _payPalRecharge, value); }
        }

        private int _unreadMessages;
        public int UnreadMessages
        {
            get { return _unreadMessages; }
            set { Set(() => UnreadMessages, ref _unreadMessages, value); }
        }

        private async Task LoadAccountAsync(bool forceReload)
        {
            try
            {
                ShowWaitingScreen();

                AccountManagement helper = new AccountManagement();
                Account account = await helper.GetAccountAsync(forceReload);
                Id = account.Id;
                UserName = account.UserName;
                UnreadMessages = account.UnreadMessages;
                AccountBalance = account.AccountBalance;
                BankRecharge = account.BankRecharge;
                PayPalRecharge = account.PayPalRecharge;
            }
            finally
            {
                HideWaitingScreen();
            }
        }

        #endregion

        public enum Modes
        {
            Buying,
            Selling
        }

        public Modes SelectedMode { get; protected set; }

        public bool IsBuyingSelected => SelectedMode == Modes.Buying;
        public bool IsSellingSelected => SelectedMode == Modes.Selling;

        private ICommand _switchToBuyingCommand;
        public ICommand SwitchToBuyingCommand
        {
            get
            {
                _switchToBuyingCommand = _switchToBuyingCommand ?? new RelayCommand(() => SelectMode(Modes.Buying));
                return _switchToBuyingCommand;
            }
        }

        private ICommand _switchToSellingCommand;
        public ICommand SwitchToSellingCommand
        {
            get
            {
                _switchToSellingCommand = _switchToSellingCommand ?? new RelayCommand(() => SelectMode(Modes.Selling));
                return _switchToSellingCommand;
            }
        }

        private void SelectMode(Modes mode)
        {
            SelectedMode = mode;
            RaisePropertyChanged(() => IsBuyingSelected);
            RaisePropertyChanged(() => IsSellingSelected);
        }

        public BuyingViewModel BuyingViewModel { get; protected set; } = new BuyingViewModel();

        public SellingViewModel SellingViewModel { get; protected set; } = new SellingViewModel();

        public async Task InitializeAsync()
        {
            try
            {
                SelectedMode = Modes.Buying;
                await LoadAccountAsync(true);
                await BuyingViewModel.InitializeAsync();
                await SellingViewModel.InitializeAsync();
            }
            catch (Exception ex)
            {
                OnError(true, ex);
            }
        }
    }

    public class MainViewModelDesignData : MainViewModel
    {
        public MainViewModelDesignData()
        {
            Id = 12345;
            UserName = "SinaC";
            AccountBalance = 2.48m;
            BankRecharge = 3.57m;
            PayPalRecharge = 0m;
            UnreadMessages = 2;

            BuyingViewModel = new BuyingViewModelDesignData();
            SellingViewModel = new SellingViewModelDesignData();
        }
    }
}
