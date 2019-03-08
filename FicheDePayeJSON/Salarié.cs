using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace FicheDePayeJSON
{
    [DataContract]
    class Salarié
    {
        private string _nom;
        [DataMember]
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _prenom;
        [DataMember]
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        private DateTime _dateDeNaissance;
        [DataMember]
        public DateTime DateDeNaissance
        {
            get { return _dateDeNaissance; }
            set { _dateDeNaissance = value; }
        }


        public Salarié(string nom, string prenom, DateTime dateDeNaissance)
        {
            _nom = nom;
            _prenom = prenom;
            _dateDeNaissance = dateDeNaissance;
        }
    }
}
