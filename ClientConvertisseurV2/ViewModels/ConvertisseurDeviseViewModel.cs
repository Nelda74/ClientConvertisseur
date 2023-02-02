using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.ViewModels
{
    public class ConvertisseurDeviseViewModel : Convertisseur
    {
        public override void Calcul()
        {
            //Code du calcul à écrire
            Res = Input / SelectedCurrency.Taux;
        }
    }
}
