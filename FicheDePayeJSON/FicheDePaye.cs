using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

namespace FicheDePayeJSON
{
    [DataContract]
    class FicheDePaye : Contrat
    {
       
        private double _nbHeure;
        [DataMember]
        public double NbHeure
        {
            get { return _nbHeure; }
            set { _nbHeure = value; }
        }

        private double _vacancesJours;
        [DataMember]
        public double VacancesJours
        {
            get { return _vacancesJours; }
            set { _vacancesJours = value; }
        }

        private double _vacancesHeures;
        [DataMember]
        public double VacancesHeures
        {
            get { return _vacancesHeures; }
            set { _vacancesHeures = value; }
        }


        private string _debutPeriode;
        [DataMember]
        public string DebutPeriode
        {
            get { return _debutPeriode; }
            set { _debutPeriode = value; }
        }

        private string _finPeriode;
        [DataMember]
        public string FinPeriode
        {
            get { return _finPeriode; }
            set { _finPeriode = value; }
        }

        public FicheDePaye(Salarié s, Contrat c, double heuretot, double vac, string debutPeriode, string finPeriode) : base(s, c.TauxHoraire)
        {
            //_nbHeure = Math.Round(heuretot);
            _nbHeure = (Math.Round(heuretot / 4.6));
            _debutPeriode = debutPeriode;
            _finPeriode = finPeriode;
            _vacancesJours = vac;
            _vacancesHeures = vac * 7;
        }

        public override string ToString()
        {                                                                                                                                                 // divisé par 4.6 pour s'approché de la réalité sur 1 mois
            return "L'employé(e) s'appelle " + base.Nom + " " + base.Prenom + "." + " Du " + DebutPeriode + " au " + FinPeriode + ", l'employé a travaillé " + /*(Math.Round(NbHeure / 4.6)-VacancesHeures)*/ (NbHeure - VacancesHeures) + " heures, pour un salaire de " + /*Math.Round((NbHeure / 4.6)*/ (NbHeure * TauxHoraire) + "euros. Le salarié à pris " + VacancesJours + " jours de vacances"; ;
            
        }
    }
}
