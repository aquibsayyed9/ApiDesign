using EzPay.Business.Interfaces;
using EzPay.DTO;
using Microsoft.Extensions.Options;
using NLog;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EzPay.Business.Classes
{
    public class CyberSource : ICyberSource
    {
        static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly AppSettings _appSettings;
        public CyberSource(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        private string CommaSeparate(IList<string> dataToSign)
        {
            return string.Join(",", dataToSign);
        }
        private string BuildDataToSign(IDictionary<string, string> paramsArray)
        {
            String[] signedFieldNames = paramsArray["signed_field_names"].Split(',');
            IList<string> dataToSign = new List<string>();

            foreach (string signedFieldName in signedFieldNames)
            {
                dataToSign.Add(signedFieldName + "=" + paramsArray[signedFieldName]);
            }

            return CommaSeparate(dataToSign);
        }
        private string Sign(IDictionary<string, string> paramsArray, string secretKey)
        {
            string data = BuildDataToSign(paramsArray);
            UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = encoding.GetBytes(secretKey);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] messageBytes = encoding.GetBytes(data);
            return Convert.ToBase64String(hmacsha256.ComputeHash(messageBytes));
        }

        public CyberSourceDTO GenerateCSModel(PaymentTransactionsDTO paymentDTO, string amount)
        {
            try
            {
                logger.Info("Generating ADCB_CS model");
                string[] customerName = paymentDTO.Name.Split(new[] { " " }, StringSplitOptions.None);
                DateTime time = DateTime.Now.ToUniversalTime();

                CyberSourceDTO model = new CyberSourceDTO
                {
                    amount = amount,
                    bill_to_address_line1 = string.Empty,
                    bill_to_address_city = string.Empty,
                    bill_to_address_country = "AE",
                    bill_to_address_state = string.Empty,
                    bill_to_address_postal_code = string.Empty,
                    bill_to_email = paymentDTO.EmailID,
                    bill_to_forename = customerName[0].ToString(),
                    bill_to_surname = customerName.Length > 1 ? customerName[1].ToString() : string.Empty,
                    currency = "AED",
                    //CompanyCode = Convert.ToInt32(paymentDTO.CompanyCode),
                    //ProfitCenter = paymentDTO.ProfitCenter,
                    //CustomerDetails = customerDetails,
                    customer_ip_address = paymentDTO.IPAddress,
                    locale = "en",
                    //payment_method = "card",
                    reference_number = paymentDTO.OrderID,
                    //reference_number = "51000315-1010800030-382",
                    signed_date_time = time.ToString("yyyy-MM-dd'T'HH:mm:ss'Z'"),
                    //signed_date_time = "2020-05-20T09:53:42Z",
                    signed_field_names = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency,bill_to_forename,bill_to_surname,bill_to_email,bill_to_address_line1,bill_to_address_city,bill_to_address_state,bill_to_address_country,bill_to_address_postal_code,customer_ip_address",
                    //signed_field_names = "access_key,profile_id,transaction_uuid,signed_field_names,unsigned_field_names,signed_date_time,locale,transaction_type,reference_number,amount,currency",
                    submit = "Submit",
                    transaction_type = "sale",
                    transaction_uuid = Guid.NewGuid().ToString(),
                    //transaction_uuid = "3dc20c07-8b6c-4ce8-9e6c-66e3603773af",
                    unsigned_field_names = "",
                };

                logger.Info("Cybser Source DTO model generated, exiting method");
                return model;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }

        public CyberSourceDTO GetSignedData(CyberSourceDTO model, out bool status)
        {
            status = false;
            try
            {
                logger.Info("Inside Method: GetSignedData, Creating parameters object");
                IDictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "access_key", model.access_key },
                    { "amount", model.amount },
                    { "bill_to_address_line1", model.bill_to_address_line1 },
                    { "bill_to_address_city", model.bill_to_address_city },
                    { "bill_to_address_country", model.bill_to_address_country },
                    { "bill_to_address_state", model.bill_to_address_state },
                    { "bill_to_address_postal_code", model.bill_to_address_postal_code },
                    { "bill_to_email", model.bill_to_email },
                    { "bill_to_forename", model.bill_to_forename },
                    { "bill_to_surname", model.bill_to_surname },
                    { "currency", model.currency },
                    { "customer_ip_address", model.customer_ip_address },
                    { "locale", model.locale },
                    { "profile_id", model.profile_id },
                    { "reference_number", model.reference_number },
                    { "signed_date_time", model.signed_date_time },
                    { "signed_field_names", model.signed_field_names },
                    { "transaction_type", model.transaction_type },
                    { "transaction_uuid", model.transaction_uuid },
                    { "unsigned_field_names", model.unsigned_field_names }
                };

                logger.Info("Signing parameter with Secret Key");
                string requestParam = Sign(parameters, model.SecretKey);

                model.PGType = "CyberSource";
                model.RequestParams = requestParam;
                model.PGUrl = _appSettings.CyberSourceGatewayURL;

                status = true;
                logger.Info("Returning Request param for payment gateway transaction");

                return model;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return null;
            }
        }
    }
}
