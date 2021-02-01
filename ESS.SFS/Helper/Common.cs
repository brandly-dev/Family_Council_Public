using MatSource.Library.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
using MailKit.Net.Smtp;
using System.Collections;
using ESS.SFS.ViewModel;
using System.Linq;

namespace ESS.SFS.Helper
{

    public static class Common
    {
        //public readonly IConfiguration configuration;
        //public Common(IConfiguration configuration)
        //{
        //    configuration = _configuration;
        //}

        public static String Encryption(string TextToBeEncrypted)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            String Password = "CSC";
            Byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(TextToBeEncrypted);
            Byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric encryptor object. 
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            //Defines a stream that links data streams to cryptographic transformations
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            //Writes the final state and clears the buffer
            cryptoStream.FlushFinalBlock();
            Byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            String EncryptedData = Convert.ToBase64String(CipherBytes);
            return EncryptedData;
        }
        public static String Decryption(string TextToBeDecrypted)
        {
            TextToBeDecrypted = TextToBeDecrypted.Replace(" ", "+");
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            String Password = "CSC";
            String DecryptedData;
            try
            {
                Byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted);
                Byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                //Making of the key for decryption
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
                //Creates a symmetric Rijndael decryptor object.
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                //Defines the cryptographics stream for decryption.THe stream contains decrpted data
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
                Byte[] PlainText = new Byte[EncryptedData.Length];
                Int32 DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();
                //Converting to string
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                DecryptedData = TextToBeDecrypted;
            }
            return DecryptedData;
        }

        public static bool sendEmail(string body, string subject, string toEmail)
        {
            bool IsSend = false;
            try
            {
                MailMessage message = new MailMessage();
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                message.From = new MailAddress("nidamalikg9@gmail.com");
                message.To.Add(new MailAddress(toEmail));
                message.Subject = subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("nidamalikg9@gmail.com", "nid111222333");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                IsSend = true;

            }
            catch (Exception ex)
            {

            }
            return IsSend;
        }

        public static void sendEmailFromAWS(string body, string subject, string toEmail, string ccEmail = "")
        {
            var Host = SettingsConfigHelper.GetCurrentSettings("Host").appSettingValue;
            var FromEmail = SettingsConfigHelper.GetCurrentSettings("Email").appSettingValue;
            var UserName = SettingsConfigHelper.GetCurrentSettings("UserName").appSettingValue;
            var Password = SettingsConfigHelper.GetCurrentSettings("Password").appSettingValue;
            var Port = int.Parse(SettingsConfigHelper.GetCurrentSettings("Port").appSettingValue);

            try
            {
              
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress(FromEmail);
                message.To.Add(new MailAddress(toEmail));
                if (!string.IsNullOrEmpty(ccEmail))
                {
                    string[] ccid = ccEmail.Split(',');
                    foreach (var item in ccid)
                    {
                        message.CC.Add(new MailAddress(item));

                    }
                }
                message.Subject = subject;
                message.Body = body;

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
                client.Host = Host;     // These are the host connection properties  
                client.Port = Port;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential(UserName, Password);    // here you need to declare the credentials of the sender  
                try
                {
                    client.Send(message);      // And finally this is the line which executes our process and sends mail             
                }
                catch (Exception ex)
                {
                   
                    throw ex;
                }

            }
            catch
            {
                throw;
            }
        }

        public static String GetContextFromHTML(Hashtable paramLst, string Path)
        {
            string context = "";
            using (StreamReader sr = new StreamReader(Path))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    context += line;
                }
            }
            if (context.Length > 0)
            {
                foreach (DictionaryEntry key in paramLst)
                {
                    context = context.Replace(key.Key.ToString(), Convert.ToString(key.Value));
                }
            }
            return context;
        }

        public static string EncryptPassword(string Password)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] hashedPassword = mySHA256.ComputeHash(encoder.GetBytes(Password));
            return Convert.ToBase64String(hashedPassword);
        }

        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query,
                                         int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public static List<DropdownListForText> GetGender()
        {
            List<DropdownListForText> lstGender = new List<DropdownListForText>()
                                                                                     {
                                                                                      new DropdownListForText() {Value = "M", Text = "Male" },
                                                                                     new DropdownListForText() {Value = "F", Text = "Female" }
            };
            return lstGender;
        }

        public static List<DropdownListForText> GetCountry()
        {
            List<DropdownListForText> lstCountry = new List<DropdownListForText>()
                                                                                     {
                                                                                      new DropdownListForText() {Value = "Bulgaria", Text = "Bulgaria" },
                                                                                      new DropdownListForText() {Value = "LivingAbroad", Text = "I am living abroad" }
            };
            return lstCountry;
        }


        public class DropdownList
        {
            public long Value { get; set; }
            public string Text { get; set; }
        }
        public class DropdownListForText
        {
            public string Value { get; set; }
            public string Text { get; set; }
        }
        public static readonly string SendToSurveyHtml = "<p>Dear [Name],<br />You received the new family survey [SurveyName]. Please login to the system and submit the survey.<br />Thanks<br />Regarding<br />Smart Family Survey</p>";



    }
}
