using System;
using System.Collections.Generic;
using System.Text;

namespace FicheDePayeJSON
{
    class Salarié
    {
        private string _nom;
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        private string _prenom;
        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        private DateTime _dateDeNaissance;
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
