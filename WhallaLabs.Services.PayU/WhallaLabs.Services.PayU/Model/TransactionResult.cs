using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhallaLabs.Services.PayU.Model
{
    public class TransactionResult
    {
        public enum Status
        {
            Succeed,
            Failed,
            Cancelled
        }

        public Status TransactionStatus { get; private set; }
        public string ErrorMessage { get; private set; }

        public TransactionResult(Status status, string errorMessage)
        {
            TransactionStatus = status;
            ErrorMessage = errorMessage;
        }
    }
}
