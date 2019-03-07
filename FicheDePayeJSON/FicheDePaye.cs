using System;
using System.Collections.Generic;
using System.Text;

namespace FicheDePayeJSON
{
    class FicheDePaye : Contrat
    {
       
        private double _nbHeure;
        public double NbHeure
        {
            get { return _nbHeure; }
            set { _nbHeure = value; }
        }

        private double _vacancesJours;
        public double VacancesJours
        {
            get { return _vacancesJours; }
            set { _vacancesJours = value; }
        }

        private string _debutPeriode;
        public string DebutPeriode
        {
            get { return _debutPeriode; }
            set { _debutPeriode = value; }
        }

        private string _finPeriode;
        public string FinPeriode
        {
            get { return _finPeriode; }
            set { _finPeriode = value; }
        }

        public FicheDePaye(Salarié s, Contrat c, double heuretot, double vac, string debutPeriode, string finPeriode) : base(s, c.TauxHoraire)
        {
            _nbHeure = Math.Round(heuretot);
            _debutPeriode = debutPeriode;
            _finPeriode = finPeriode;
            _vacancesJours = vac;
        }

        public override string ToString()
        {                                                                                                                                                 // divisé par 4.6 pour s'approché de la réalité sur 1 mois
            return "L'employé(e) s'appelle " + base.Nom + " " + base.Prenom + "." + " Du " + DebutPeriode + " au " + FinPeriode + ", l'employé a travaillé " + Math.Round(NbHeure / 4.6) + " heures, pour un salaire de " + Math.Round((NbHeure / 4.6) * TauxHoraire) + "euros. Le salarié à pris " + VacancesJours + " jours de vacances"; ;
            
        }
    }
}
