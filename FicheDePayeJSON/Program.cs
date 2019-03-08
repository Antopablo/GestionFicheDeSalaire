using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;

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
            
            string datePeriod;
            string UserChoice;
            List<Salarié> ListeSalarie = new List<Salarié>();
            List<Contrat> ListeContrat = new List<Contrat>();
            List<FicheDePaye> ListeFDP = new List<FicheDePaye>();


           

            do
            {
                Console.WriteLine("Voulez-vous recruter un salarié ?");
                UserChoice = Console.ReadLine();
                if (UserChoice == "oui")
                {
                    CreationSalarié(ListeSalarie); //création + ajout dans la liste d'un salarié
                    EcrireDansJson(ListeSalarie); // sauvegarde dans le fichier Json
                    CreationContrat(ListeSalarie, ListeContrat);
                    EcrireDansJson(ListeContrat);
                }
            } while (UserChoice == "oui");

            Console.WriteLine("######## UN MOIS PLUS TARD ########");
            Console.WriteLine("######## Création automatique des fiches de salaire ########");

            CreationFicheDePaie(ListeSalarie, ListeContrat, ListeFDP);
            EcrireDansJson(ListeFDP);

            Console.WriteLine("####### Fiches de salaire disponibles #######");




            /* calcul des heures dans le mois*/

            /*            Salarié Antony = new Salarié("LEFEVRE", "Antony", new DateTime(1993, 10, 07));
                       Contrat ContratAntony = new Contrat(Antony, 10);
                       Console.WriteLine("date de début de période");
                       datePeriod = Console.ReadLine();
                       DateTime DebutPeriode =  DateTime.Parse(datePeriod);
                       Console.WriteLine("date de fin de période");
                       datePeriod = Console.ReadLine();
                       DateTime FinPeriode =  DateTime.Parse(datePeriod);
                       TimeSpan HeuresTotal = FinPeriode.Subtract(DebutPeriode);

                       /* fonctionne pour les 2 */
            /*           ListeFDP.Add(new FicheDePaye(Antony, ContratAntony, HeuresTotal.TotalHours, vacances(Antony), DebutPeriode.ToLongDateString(), FinPeriode.ToLongDateString()));
                       ListeFDP.Add(new FicheDePaye(new Salarié("GEORGES", "Alexis", new DateTime(1990, 07, 01)), new Contrat(new Salarié("GEORGES", "Alexis", new DateTime(1990, 07, 01)), 15), HeuresTotal.TotalHours, vacances(new Salarié("GEORGES", "Alexis", new DateTime(1990, 07, 01))), DebutPeriode.ToLongDateString(), FinPeriode.ToLongDateString()));

                        EcrireDansJson(ListeFDP);

             */
        }

        static public double vacances(Salarié s)
        {
            string userChoice;
            double JourDeVacances = 0.0;
            Console.WriteLine("Y a-t-il eu des vacances pour " + s.Prenom + " " + s.Nom + " ce mois ?");
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


        static public void EcrireDansJson(List<Salarié> salarie)
        {
            /* écriture dans le fichier JSON */
            Stream memstream = new StreamWriter("listeDesSalaries.json").BaseStream;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Salarié>));
            ser.WriteObject(memstream, salarie);
            memstream.Close();
            Console.WriteLine("Fin d'écriture");
        }

        static public void EcrireDansJson(List<Contrat> Contrat)
        {
            /* écriture dans le fichier JSON */
            Stream memstream = new StreamWriter("listeDesContrats.json").BaseStream;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Contrat>));
            ser.WriteObject(memstream, Contrat);
            memstream.Close();
            Console.WriteLine("Fin d'écriture");
        }

        static public void EcrireDansJson(List<FicheDePaye> fdp)
        {
            /* écriture dans le fichier JSON */
            Stream memstream = new StreamWriter("ListeDesFichesDePaies.json").BaseStream;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<FicheDePaye>));
            ser.WriteObject(memstream, fdp);
            memstream.Close();
            Console.WriteLine("Fin d'écriture");
        }

        static public void CreationSalarié(List<Salarié> ls)
        {
            Console.WriteLine("Quel est son nom");
            string nomS = Console.ReadLine();
            Console.WriteLine("Quel est son prénom");
            string prenomS = Console.ReadLine();
            Console.WriteLine("Quel est son jour de naissance");
            int JJ = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Quel est son mois de naissance");
            int MM = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Quel est son année de naissance");
            int YY = Int32.Parse(Console.ReadLine());
            Salarié sala = new Salarié(nomS, prenomS, new DateTime(YY, MM, JJ));
            ls.Add(sala);
        }

        static public void CreationContrat(List<Salarié> s, List<Contrat> c)
        {
            int UserChoiceINT;
            Console.WriteLine("Quel taux horaire");
            UserChoiceINT = Int32.Parse(Console.ReadLine());
            Contrat Cont = new Contrat(s[s.Count-1], UserChoiceINT);
            c.Add(Cont);
        }

        static public void CreationFicheDePaie(List<Salarié> s,List<Contrat> c, List<FicheDePaye> fdp)
        {
            string datePeriod;
            Console.WriteLine("date de début de période");
            datePeriod = Console.ReadLine();
            DateTime DebutPeriode = DateTime.Parse(datePeriod);
            Console.WriteLine("date de fin de période");
            datePeriod = Console.ReadLine();
            DateTime FinPeriode = DateTime.Parse(datePeriod);
            TimeSpan HeuresTotal = FinPeriode.Subtract(DebutPeriode);
            for (int i = 0; i < c.Count; i++)
            {
                FicheDePaye FichePaye = new FicheDePaye(s[i], c[i], HeuresTotal.TotalHours, vacances(s[i]), DebutPeriode.ToLongDateString(), FinPeriode.ToLongDateString());
                fdp.Add(FichePaye);
            }
            
        }

    }
}
