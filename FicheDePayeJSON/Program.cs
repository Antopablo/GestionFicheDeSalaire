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
            
            string UserChoice;
            int UserChoiceINT;
            List<Salarié> ListeSalarie = new List<Salarié>();
            List<Contrat> ListeContrat = new List<Contrat>();
            List<FicheDePaye> ListeFDP = new List<FicheDePaye>();

            
            do
            {
                //Console.WriteLine("Voulez-vous recruter un salarié ? oui/non");
                Console.WriteLine("Que faire ? 1)Recruter un salarié  2) Licencié un salarié 3) Générer fiche de salaire 4)FinDuProg");
                UserChoiceINT = Int32.Parse(Console.ReadLine());
                if (UserChoiceINT == 1)
                {
                    CreationSalarié(ListeSalarie); //création + ajout dans la liste d'un salarié
                    EcrireDansJson(ListeSalarie); // sauvegarde dans le fichier Json
                    CreationContrat(ListeSalarie, ListeContrat);
                    EcrireDansJson(ListeContrat);
                }
                else if (UserChoiceINT == 2)
                {
                    Console.WriteLine("nom de la personne à virer");
                    string vireNom = Console.ReadLine();
                    Console.WriteLine("prenom de la personne à virer");
                    string virePrenom = Console.ReadLine();
                    LicencierSalarie(ListeContrat, vireNom, virePrenom);
                }
                else if (UserChoiceINT == 3)
                {
                    Console.WriteLine("######## Création automatique des fiches de salaire ########");
                    CreationFicheDePaie(ListeSalarie, ListeContrat, ListeFDP);
                    EcrireDansJson(ListeFDP);
                    Console.WriteLine("####### Fiches de salaire disponibles #######");
                }
                else if (UserChoiceINT == 4)
                {
                    Console.WriteLine("fin du prog");
                    Environment.Exit(0);
                }
            } while (UserChoiceINT == 1 || UserChoiceINT == 2);


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
            Console.WriteLine("date de début de période format jj/mm/yyyy");
            datePeriod = Console.ReadLine();
            DateTime DebutPeriode = DateTime.Parse(datePeriod);
            Console.WriteLine("date de fin de période format jj/mm/yyyy");
            datePeriod = Console.ReadLine();
            DateTime FinPeriode = DateTime.Parse(datePeriod);
            TimeSpan HeuresTotal = FinPeriode.Subtract(DebutPeriode);
            for (int i = 0; i < c.Count; i++)
            {
                FicheDePaye FichePaye = new FicheDePaye(s[i], c[i], HeuresTotal.TotalHours, vacances(s[i]), DebutPeriode.ToLongDateString(), FinPeriode.ToLongDateString());
                fdp.Add(FichePaye);
            }
            
        }

        static public void LicencierSalarie(List<Contrat> c, string nom, string prenom)
        {
            foreach (Contrat item in c)
            {
                if (item.Nom == nom && item.Prenom == prenom)
                {
                    Console.WriteLine(item.Nom + " " + item.Prenom + " a été viré comme une merdeeee");
                    c.Remove(item);
                    break;
                }
                
            }

            foreach (Contrat item in c)
            {
                Console.WriteLine(item.Prenom);
            }
            
        }

    }
}
