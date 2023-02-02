using ClientConvertisseurV2.Models;
using ClientConvertisseurV2.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels
{
    public abstract class Convertisseur : ObservableObject
    {
        private ObservableCollection<Devise> devises;

        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set
            {
                devises = value;
                OnPropertyChanged();
            }
        }

        private Devise selectedCurrency;

        public Devise SelectedCurrency
        {
            get { return selectedCurrency; }
            set { selectedCurrency = value; }
        }

        private Double input;

        public Double Input
        {
            get { return input; }
            set { input = value; }
        }

        private Double res;

        public Double Res
        {
            get { return res; }
            set
            {
                res = value;
                OnPropertyChanged();
            }
        }

        public async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:7139/api/");
            List<Devise> result = await service.GetDevisesAsync("devises");
            if (result == null)
            {
                ShowAsync("Erreur de réseau", "Impossible de charger les données depuis l'API");
            }
            else
            {
                Devises = new ObservableCollection<Devise>(result);
            }
        }

        public IRelayCommand BtnSetConversion { get; }
        public Convertisseur()
        {
            //Boutons
            BtnSetConversion = new RelayCommand(ActionSetConversion);
            GetDataOnLoadAsync();
        }
        public void ActionSetConversion()
        {
            if (SelectedCurrency == null)
            {
                ShowAsync("Erreur de votre part", "Vous devez sélectionner une devise !");
                return;
            }

            if (Input < 0)
            {
                ShowAsync("Erreur de votre part", "Vous devez entrer un montant supérieur à 0 !");
                return;
            }

            Calcul();
        }

        public abstract void Calcul();

        private async void ShowAsync(String title, String message)
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = title,
                Content = message,
                CloseButtonText = "Ok"
            };

            noWifiDialog.XamlRoot = App.MainRoot.XamlRoot;
            await noWifiDialog.ShowAsync();
        }
    }
}
