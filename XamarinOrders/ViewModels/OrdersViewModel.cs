﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using XamarinOrders.Models;
using XamarinOrders.Services;

namespace XamarinOrders.ViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        #region Fields

        WebAPIService webAPIService;
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<Order> items;

        #endregion

        #region Properties
        public ObservableCollection<Order> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                RaisepropertyChanged("Items");
            }
        }
        #endregion

        #region Constructor
        public OrdersViewModel()
        {
            webAPIService = new WebAPIService();
            //Item source which needs to be displayed on the list view.
            Items = new ObservableCollection<Order>();
            GetDataFromWebAPI();
        }
        #endregion

        #region Methods 
        async void GetDataFromWebAPI()
        {
            Items = await webAPIService.RefreshDataAsync();
        }
        void RaisepropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
