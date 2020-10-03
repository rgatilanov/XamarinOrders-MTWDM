using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XamarinOrders.Models;

namespace XamarinOrders.Services
{
    /// <summary>
    /// Implementation of WebAPI service to be displayed.
    /// </summary>
    public class WebAPIService
    {
        #region Fields 

        System.Net.Http.HttpClient client;

        #endregion

        #region Properties 

        public ObservableCollection<Order> Items
        {
            get; private set;
        }

        public string WebAPIUrl
        {
            get; private set;
        }

        #endregion

        #region Constructor
        public WebAPIService()
        {
            client = new System.Net.Http.HttpClient();
        }

        #endregion

        #region Methods
        public async System.Threading.Tasks.Task<ObservableCollection<Order>> RefreshDataAsync()
        {
            //WebAPIUrl = "https://mtwdm-covid19.azurewebsites.net/api/order"; // Set your REST API url here

            //Con esta Api de ejemplo se muestra
            WebAPIUrl = "https://ej2services.syncfusion.com/production/web-services/api/Orders"; // Set your REST API url here
            var uri = new Uri(WebAPIUrl);
            try
            {
                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Items = JsonConvert.DeserializeObject<ObservableCollection<Order>>(content);
                    return Items;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        #endregion
    }
}
