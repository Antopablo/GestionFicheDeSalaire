using System;
using System.Collections.Generic;
using System.Text;

namespace FicheDePayeJSON
{
    class Contrat : Salarié
    {
        private int _tauxHoraire;
        public int TauxHoraire
        {
            get { return _tauxHoraire; }
            set { _tauxHoraire = value; }
        }

        public Contrat (Salarié s, int TauxHoraire) : base(s.Nom, s.Prenom, s.DateDeNaissance)
        {
            _tauxHoraire = TauxHoraire;
        }

    }
}
