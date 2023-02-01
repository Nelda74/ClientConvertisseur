// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Service;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }

        private ObservableCollection<Devise> devises;

        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set { 
                devises = value;
                OnPropertyChanged("Devises");
            }
        }

        private Devise selectedCurrency;

        public Devise SelectedCurrency
        {
            get { return selectedCurrency; }
            set { selectedCurrency = value; }
        }

        private Double montantEuro;

        public Double MontantEuro
        {
            get { return montantEuro; }
            set { montantEuro = value; }
        }

        private Double res;

        public Double Res
        {
            get { return res; }
            set { 
                res = value;
                OnPropertyChanged("Res");
            }
        }


        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7139/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
            {
                ShowAsync("Erreur de réseau", "Impossible de charger les données depuis l'API");
            } else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }

        private async void ShowAsync(String title, String message)
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok"
            };

            noWifiDialog.XamlRoot = this.Content.XamlRoot;
            await noWifiDialog.ShowAsync();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCurrency== null)
            {
                ShowAsync("Erreur de votre part", "Vous devez sélectionner une devise !");
                return;
            }

            if (MontantEuro < 0)
            {
                ShowAsync("Erreur de votre part", "Vous devez entrer un montant supérieur à 0 !");
                return;
            }

            Res = MontantEuro * SelectedCurrency.Taux;
        }
    }
}
