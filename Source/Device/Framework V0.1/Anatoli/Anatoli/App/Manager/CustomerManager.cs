﻿using Anatoli.App.Model;
using Anatoli.App.Model.AnatoliUser;
using Anatoli.Framework.AnatoliBase;
using Anatoli.Framework.DataAdapter;
using Anatoli.Framework.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anatoli.App.Manager
{
    public class CustomerManager : BaseManager<BaseDataAdapter<CustomerViewModel>, CustomerViewModel>
    {
        public static async Task SaveCustomerAsync(CustomerViewModel customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException("Could not save null user!");
            }
            string content = customer.Address +
                Environment.NewLine + customer.BirthDay +
                Environment.NewLine + customer.CustomerCode +
                Environment.NewLine + customer.CustomerName +
                Environment.NewLine + customer.Email +
                Environment.NewLine + customer.Mobile +
                Environment.NewLine + customer.NationalCode +
                Environment.NewLine + customer.Phone +
                Environment.NewLine + customer.PostalCode +
                Environment.NewLine + customer.UniqueId;
            bool wResult = await Task.Run(() =>
            {
                var cipherText = Crypto.EncryptAES(content);
                bool result = AnatoliClient.GetInstance().FileIO.WriteAllBytes(cipherText, AnatoliClient.GetInstance().FileIO.GetDataLoction(), Configuration.customerInfoFile);
                return result;
            });
        }

        public static async Task<CustomerViewModel> ReadCustomerAsync()
        {

            byte[] cipherText = await Task.Run(() =>
            {
                byte[] result = AnatoliClient.GetInstance().FileIO.ReadAllBytes(AnatoliClient.GetInstance().FileIO.GetDataLoction(), Configuration.customerInfoFile);
                return result;
            });
            byte[] plainText = Crypto.DecryptAES(cipherText);
            string cInfo = Encoding.Unicode.GetString(plainText, 0, plainText.Length);
            string[] cInfoFields = cInfo.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            CustomerViewModel customer = new CustomerViewModel();
            customer.Address = cInfoFields[0];
            customer.BirthDay = new DateTime();
            customer.CustomerCode = cInfoFields[2];
            customer.CustomerName = cInfoFields[3];
            customer.Email = cInfoFields[4];
            customer.Mobile = cInfoFields[5];
            customer.NationalCode = cInfoFields[6];
            customer.Phone = cInfoFields[7];
            customer.PostalCode = cInfoFields[8];
            customer.UniqueId = cInfoFields[9];
            return customer;
        }

        public static async Task<CustomerViewModel> DownloadCustomerAsync(AnatoliUserModel user)
        {
            var userModel = await AnatoliClient.GetInstance().WebClient.SendGetRequestAsync<CustomerViewModel>(TokenType.AppToken, Configuration.WebService.Users.ViewProfileUrl,
                new Tuple<string, string>("Id", user.Id),
                new Tuple<string, string>("PrivateOwnerId", user.PrivateOwnerId));
            return userModel;
        }

        public static async Task<CustomerViewModel> UploadCustomerAsync(CustomerViewModel user)
        {
            var userModel = await AnatoliClient.GetInstance().WebClient.SendPostRequestAsync<CustomerViewModel>(TokenType.AppToken, Configuration.WebService.Users.SaveProfileUrl + "?PrivateOwnerId=" + user.PrivateOwnerId,
                user
                );
            return userModel;

        }
    }
}