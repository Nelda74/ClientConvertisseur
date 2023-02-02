﻿using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.Models;
using Microsoft.UI.Xaml;
using ClientConvertisseurV2.Service;
using CommunityToolkit.Mvvm.Input;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurEuroViewModel : Convertisseur
    {
        public override void Calcul()
        {
            //Code du calcul à écrire
            Res = Input * SelectedCurrency.Taux;
        }
    }
}
