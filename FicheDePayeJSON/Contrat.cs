using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace FicheDePayeJSON
{
    [DataContract]
    class Contrat : Salarié
    {
        
        private int _tauxHoraire;
        [DataMember]
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
