using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhallaLabs.Services.PayU.Helpers;

namespace WhallaLabs.Services.PayU.Model
{
    public class PaymentDTO
    {
        [QueryProperty("pos_id")]
        public int PosId { get; set; }

        [QueryProperty("pos_auth_key")]
        public string PosAuthKey { get; set; }

        [QueryProperty("pay_type")]
        public string PaymentType { get; set; }

        [QueryProperty("session_id")]
        public string SessionId { get; set; }

        [QueryProperty("amount")]
        public uint Amount { get; set; }

        [QueryProperty("desc")]
        public string Description { get; set; }

        [QueryProperty("order_id")]
        public string OrderId { get; set; }

        [QueryProperty("desc2")]
        public string SecondDescription { get; set; }

        [QueryProperty("trsDesc")]
        public string TransactionDescription { get; set; }

        [QueryProperty("first_name")]
        public string FirstName { get; set; }

        [QueryProperty("last_name")]
        public string LastName { get; set; }

        [QueryProperty("street")]
        public string Street { get; set; }

        [QueryProperty("street_hn")]
        public string HomeNumber { get; set; }

        [QueryProperty("street_an")]
        public string ApartmentNumber { get; set; }

        [QueryProperty("city")]
        public string City { get; set; }

        [QueryProperty("post_code")]
        public string PostCode { get; set; }

        /// <summary>
        /// ISO-3166
        /// </summary>
        /// 
        [QueryProperty("country")]
        public string CountryIsoCode { get; set; }

        [QueryProperty("email")]
        public string EmailAddress { get; set; }

        [QueryProperty("phone")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// ISO-639
        /// </summary>
        /// 
        [QueryProperty("language")]
        public string Language { get; set; }

        [QueryProperty("client_ip")]
        public string ClientIpAddress { get; set; }

        [QueryProperty("js")]
        public bool IsJavaScriptEnabled { get; set; }

        [QueryProperty("sig")]
        public string Signature { get; set; }

        [QueryProperty("ts")]
        public string Timestamp { get; set; }
    }
}
