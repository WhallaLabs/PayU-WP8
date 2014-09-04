using Microsoft.Phone.Controls;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WhallaLabs.Services.PayU.Model;

namespace WhallaLabs.Services.PayU
{
    public class PayuService
    {
        internal static Uri _startUri;
        internal static Uri _successUri;
        internal static Uri _errorUri;
        internal static PaymentDTO _model;
        private static TransactionResult _transactionResult;
        private static readonly AutoResetEvent _transactionFinishedEvent = new AutoResetEvent(false);

        public static Task<TransactionResult> ProceedTransactionAsync(PaymentDTO model, Uri startUri, Uri successUri, Uri errorUri)
        {
            if (model == null)
                throw new ArgumentException("model");

            _startUri = startUri;
            _errorUri = errorUri;
            _successUri = successUri;
            _model = model;

            var rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
            if (rootFrame == null)
            {
                throw new InvalidOperationException("RootFrame wasn't found in current app domain");
            }

            rootFrame.Navigate(new Uri("/WhallaLabs.Services.PayU;component/View/PayuTransactionView.xaml", UriKind.RelativeOrAbsolute));

            Task<TransactionResult> transactionTask = Task<TransactionResult>.Factory.StartNew(() =>
            {
                _transactionFinishedEvent.WaitOne();
                return _transactionResult;
            });


            return transactionTask;
        }

        internal static void CompleteTransaction(TransactionResult result)
        {
            _transactionResult = result;
            _transactionFinishedEvent.Set();
        }
    }
}
