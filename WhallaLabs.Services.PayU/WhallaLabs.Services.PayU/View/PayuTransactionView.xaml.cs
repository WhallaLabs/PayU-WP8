using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WhallaLabs.Services.PayU.Model;
using System.Text;
using System.Diagnostics;
using WhallaLabs.Services.PayU.Helpers;

namespace WhallaLabs.Services.PayU.View
{
    public partial class PayuTransactionView : PhoneApplicationPage
    {
        public PayuTransactionView()
        {
            InitializeComponent();
        }

         #region Events
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Browser.LoadCompleted += Browser_LoadCompleted;
            Browser.Navigating += Browser_Navigating;
            Browser.NavigationFailed += Browser_NavigationFailed;

            if (e.NavigationMode == NavigationMode.Back)
                return;

            SetVisualState(true);
            Browser.Navigate(PayuService._startUri, PrepareRequestBody(PayuService._model), "Content-Type: application/x-www-form-urlencoded");

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Browser.LoadCompleted -= Browser_LoadCompleted;
            Browser.Navigating -= Browser_Navigating;
            Browser.NavigationFailed -= Browser_NavigationFailed;
            base.OnNavigatedFrom(e);
        }

        void Browser_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            SetTransactionResult(TransactionResult.Status.Failed, Translations.LanguageResources.NoInternetConnectionError);
        }

        void Browser_Navigating(object sender, NavigatingEventArgs e)
        {
            SetVisualState(true);
            Debug.WriteLine("PayU internal navigation: {0}", e.Uri.OriginalString);

            if (e.Uri.OriginalString.Contains(PayuService._successUri.OriginalString))
            {
                SetTransactionResult(TransactionResult.Status.Succeed);
            }
            else if (e.Uri.OriginalString.Contains(PayuService._errorUri.OriginalString))
            {
                SetTransactionResult(TransactionResult.Status.Failed, Translations.LanguageResources.TransactionFailedError);
            }
        }

        void Browser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            SetVisualState(false);
        }

        #endregion

        #region Methods
        private void SetVisualState(bool turnLoadingModel)
        {
            if (turnLoadingModel)
            {
                VisualStateManager.GoToState((Control)this, "Loading", true);
            }
            else
            {
                VisualStateManager.GoToState((Control)this, "Normal", true);
            }
        }

        private void SetTransactionResult(TransactionResult.Status status, string message = null)
        {
            PayuService.CompleteTransaction(new TransactionResult(status, message));

            if(NavigationService.CanGoBack)
                NavigationService.GoBack();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            SetTransactionResult(TransactionResult.Status.Cancelled);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }

        private byte[] PrepareRequestBody(PaymentDTO model)
        {
            string queryModel = new QueryConverter().Serialize(model);
            return Encoding.UTF8.GetBytes(queryModel);
        }
	    #endregion
    }
}