using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Biblioteca_P1
{
    public class Program
    {
        public static void restituie()
        {
            menager m = new menager();
            GEN g1 = m.getGEN("SF");

            AUTOR a1 = m.getAutor(nume: "Aston", prenume: "Martin");

            CARTE c1 = m.getCarte(titlu: "Intredeschise ganduri.", autor: a1, gen: g1);

            Restituire r = new Restituire();

            CITITOR cititor1 = m.getCititor(nume: "Manolache123", prenume: "Irinel", adresa: "Iasi,Iasi,Str. Codrescu nr.13,cammin C12", email: "irinel223.aka.manolache@info.uaic.ro");

            IMPRUMUT imp = new IMPRUMUT()
            {
                CARTE = c1,
                CITITOR = cititor1
            };
            if (r.existaCarte(c1))
            {
                Console.WriteLine("Cartea exista in biblioteca");
                if (r.existaCititor(cititor1))
                {
                    Console.WriteLine("Cititorul este in baza noastra de date");
                    if (r.existaImprumut(imp))
                    {
                        Console.WriteLine("Imprumutul este in baza de date");
                        r.restituieCartea(1, "O carte foarte buna");
                    }
                    else
                    {
                        Console.WriteLine("Imprumutul nu este in baza de date");
                    }
                }
                else
                {
                    Console.WriteLine("Cititorul nu este in baza noastra de date");
                }
            }
            else
            {
                Console.WriteLine("Cartea nu exista in biblioteca");
            }

        }

        public static void cumpara()
        {
            menager m = new menager();

            GEN g1 = m.getGEN("SF");

            AUTOR a1 = m.getAutor(nume: "Aston", prenume: "Martin");

            CARTE c1 = m.getCarte(titlu: "Intredeschise ganduri.", autor: a1, gen: g1);

            int nr_carti = 1;
            /*---------------------------------------------------------*/
            //metoda 1 exemplu de folosire
            AchizitionareCarti ac = new AchizitionareCarti();
            ac.achizitioneazaCarte(c1, nr_carti);
        }

        public static void  imprumuta()
        {
            menager m = new menager();

            GEN g1 = m.getGEN("SF");

            AUTOR a1 = m.getAutor(nume: "Aston", prenume: "Martin");

            CARTE c1 = m.getCarte(titlu: "Intredeschise ganduri.", autor: a1, gen: g1);

            //metoda 2 expemul de folosire
            ImprumutaCarte ic = new ImprumutaCarte();
            CITITOR cititor1 = m.getCititor(nume: "Manolache123", prenume: "Irinel", adresa: "Iasi,Iasi,Str. Codrescu nr.13,cammin C12", email: "irinel223.aka.manolache@info.uaic.ro");
            //ic.imprumutaCarte(cititor1, gen: "SF", carte_titlu:c1.Titlu, nume_a:a1.Nume,prenume_a:a1.Prenume);
            if (ic.adaugaCititor(cititor1))
            {
                Console.WriteLine("Cititor existent!");
            }
            else
            {
                Console.WriteLine("Cititor inexistent!");
            }
            Console.WriteLine(ic.verfStare(cititor1));
            if (ic.verfStare(cititor1))
            {
                Console.WriteLine("Cititorul este de buna credinta");
            }
            else
            {
                Console.WriteLine("Cititorul nu este de buna credinta");
            }
            Console.WriteLine(ic.existaCarte(c1));
            if (ic.existaCarte(c1))
            {
                Console.WriteLine("O lista cu cartile care au acelasi gen cu cartea cautata este:");
                ICollection<CARTE> list = ic.getAcelasiGen(c1.GEN);
                foreach (var carte in list)
                {
                    Console.WriteLine("Titlu:{0}", carte.Titlu.Trim());
                }

                String titlu = c1.Titlu;
                if (ic.searchBook(g1, titlu))
                {
                    int carteID = ic.getBookId(g1, titlu);
                    Console.WriteLine("Cartea a fost gasita dupa titlu");
                    if (ic.imprumutaCarte(c1.CarteId, cititor1))
                    {
                        Console.WriteLine("Cartea a fost imprumutata cu succes");
                    }
                    else
                    {
                        Console.WriteLine("Cartea va fi disponibila la data: {0}", ic.searchDataScadenta(c1));
                    }
                }
                else if (ic.searchBook(g1, a1))
                {
                    int carteID = ic.getBookId(g1, a1);
                    Console.WriteLine("Cartea a fost gasita dupa autor");
                }
                else if (ic.searchBook(g1, a1,titlu))
                {
                    int carteID = ic.getBookId(g1, a1, titlu);
                    Console.WriteLine("Cartea a fost gasita dupa titlu si autor");
                }
            }
            else
            {
                Console.WriteLine("Cartea nu exista in bilioteca.");
            }
        }

        public static void statistica()
        {
            Statistica s = new Statistica();
            menager m = new menager();

            GEN g1 = m.getGEN("SF");

            AUTOR a1 = m.getAutor(nume: "Andrei", prenume: "Plesu");

            CARTE c1 = m.getCarte(titlu: "Caderea mortii", autor: a1, gen: g1);

            // prima metoda implementata
            DateTime d2 = DateTime.Now;
            DateTime d1 = DateTime.Now.AddDays(-10);
            ICollection < CITITOR > list= s.ReaderBetweenDates(d1, d2);

            Console.WriteLine(list.Count);
            foreach (var cititor in list)
            {
                Console.WriteLine("Nume: {0}, Prenume: {1}", cititor.Nume, cititor.Prenume);
            }

            //a doua metoda implementata 
           /* List<CARTE> lista_carti = s.MostWantedBooks();
            Console.WriteLine(lista_carti.Count);
            foreach (var carte in lista_carti)
            {
                Console.WriteLine("Nume: {0}, Autor: {1} {2}", carte.Titlu.Trim(), carte.AUTOR.Nume.Trim(), carte.AUTOR.Prenume.Trim());
            }
            */
            //a 3-a metoda implementata 
            List<AUTOR> lista_autori = s.getMostFaimousAutors();
            Console.WriteLine(lista_autori.Count);
            foreach (var autor in lista_autori)
            {
                Console.WriteLine("Nume: {0}, Prenume:{1}", autor.Nume.Trim(), autor.Prenume.Trim());
            }

            //a 4-a metoda implementata 
            List<GEN> lista_genuri = s.getMostComuneGens();
            Console.WriteLine(lista_genuri.Count);
            foreach (var g in lista_genuri)
            {
                Console.WriteLine("Descriere: {0}", g.Descriere.Trim());
            }

            //a 5-a metoda implementata 
            List<REVIEW> lista_rev = s.getReviews(c1);
            Console.WriteLine(lista_rev.Count);
            foreach (var rev in lista_rev)
            {
                Console.WriteLine("Cartea: {0} a primit urm. review: {1}",c1.Titlu.Trim(),rev.Text.Trim());
            }
            
        }

        static void Main(string[] args)
        {
            //cumpara();
            //imprumuta();
            /*---------------------------------------------------------*/
            //metoda 3 ex de folosire
            //Restituie(c1, cititor1,"O carte foarte relaxanta.");
            //Restituire r = new Restituire();
            // r.RestituieCartea(c1, cititor1, "O carte foarte relaxanta");


            //DateTime d1 = DateTime.Now.AddDays(-10);
            //DateTime d2 = DateTime.Now;
            //Statistica s = new Statistica();
            //s.ReaderBetweenDates(d1,d2);
            //s.getMostFaimousAutors();
            //s.getMostComuneGens();
            //s.MostWantedBooks();
            //
            //restituie();
            //statistica();
            ImprumutaCarte r = new ImprumutaCarte();
            CARTE c = r.GetCARTE(1);
            Console.WriteLine("{0},{1},{2},{3},{4}",c.AutorId,c.Titlu,c.AUTOR.Nume,c.GenId,c.GEN.Descriere);
            Console.ReadKey();
        }



    }
    
}
