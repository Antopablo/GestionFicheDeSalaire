using System;
using System.Globalization;

namespace FicheDePayeJSON
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Consigne
            /* -garder l'hsitorique sans multiplication de données
             * ecrire un prog qui permet de gérer les fiche de paie => ajout / suppression / modifier
             * faire janvier / février 
             * afficher l'historique des modif */
            #endregion

            
            Salarié Antony = new Salarié("LEFEVRE", "Antony", new DateTime(1993, 10, 07));
            Contrat ContratAntony = new Contrat(Antony, 10);


            Console.WriteLine("date de début de période");
            string coucou = Console.ReadLine();
            DateTime DebutPeriode =  DateTime.Parse(coucou);
            Console.WriteLine("date de fin de période");
            coucou = Console.ReadLine();
            DateTime FinPeriode =  DateTime.Parse(coucou);
            TimeSpan HeuresTotal = FinPeriode.Subtract(DebutPeriode);
            Console.WriteLine(DebutPeriode);
            Console.WriteLine(FinPeriode);

            
            FicheDePaye FDPAntony = new FicheDePaye(Antony, ContratAntony, HeuresTotal.TotalHours ,vacances(), DebutPeriode.ToLongDateString(), FinPeriode.ToLongDateString());
            Console.WriteLine(FDPAntony);

        }

        static public double vacances()
        {
            string userChoice;
            double JourDeVacances = 0.0;
            Console.WriteLine("Y a-t-il eu des vacances pour ce mois ?");
            userChoice = Console.ReadLine();
            if (userChoice == "oui")
            {
                Console.WriteLine("Date de début des vacances format dd/MM/YYYY");
                string DateDebutVac = Console.ReadLine();
                DateTime DateDebutVacances = DateTime.Parse(DateDebutVac);
                Console.WriteLine("Date de fin des vacances format dd/MM/YYYY");
                string DateFinVac = Console.ReadLine();
                DateTime DateFinVacances = DateTime.Parse(DateFinVac);
                JourDeVacances = (DateFinVacances.Subtract(DateDebutVacances).TotalDays+1);
            }
            return JourDeVacances;
        }
    }
}
