using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV1.Models
{
    public class Devise
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String nomDevise;

        [Required]
        public String NomDevise
        {
            get { return nomDevise; }
            set { nomDevise = value; }
        }

        private Double taux;

        public Double Taux
        {
            get { return taux; }
            set { taux = value; }
        }

        public Devise()
        {
        }

        public Devise(int id, String? nomDevise, Double taux)
        {
            Taux = taux;
            NomDevise = nomDevise != null ? nomDevise : "Rien";
            Id = id;
        }
    }
}
