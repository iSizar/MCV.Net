using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Biblioteca_P1
{
    internal class API
    {
        internal bool agaugaCarte(CARTE carte)
        { // aceasta metoda introduce cartea carte de nr_carti_ori in baza de date
            using (var context = new ModelGeneral())
            {
                if (existAutor(carte.AUTOR))
                    carte.AUTOR = getAutor(carte.AUTOR);
                if (existGen(carte.GEN))
                    carte.GEN = getGen(carte.GEN);
                //Console.WriteLine(carte.AUTOR.AutorId);
                CARTE c = new CARTE()
                {
                    AutorId = carte.AUTOR.AutorId,
                    GenId = carte.GEN.GenId,
                    Titlu = carte.Titlu

                };
                context.CARTEs.Add(c);

                context.SaveChanges();
            }
            return true;
        }

        private AUTOR getAutor(AUTOR aUTOR)
        {
            AUTOR a = new AUTOR();
            using (var context = new ModelGeneral())
            {
                foreach (var aut in context.AUTORs)
                {
                    if (comparAutori(aut, aUTOR))
                    {
                        a.AutorId = aut.AutorId;
                        a.CARTE = aut.CARTE;
                        a.Nume = aut.Nume;
                        a.Prenume = aut.Prenume;

                        break;
                    }
                }
            }

            return a;
        }

        internal CARTE GetCarte(int id)
        {
            CARTE c = new CARTE();
            using (var context = new ModelGeneral())
            {
                foreach (var carte in context.CARTEs)
                {
                    if (carte.CarteId == id)
                    {
                        GEN g = new GEN()
                        {
                            Descriere = carte.GEN.Descriere,
                            GenId = carte.GEN.GenId
                        };

                        AUTOR a = new AUTOR()
                        {
                            AutorId = carte.AutorId.Value,
                            Nume = carte.AUTOR.Nume,
                            Prenume = carte.AUTOR.Prenume

                        };

                        foreach (var cart in carte.AUTOR.CARTE)
                        {
                            CARTE crt = new CARTE()
                            {
                                Titlu = carte.Titlu,
                                GenId = carte.GenId,
                                AutorId = a.AutorId,
                                AUTOR = a
                            };
                            a.CARTE.Add(cart);
                        }


                        c.Titlu = carte.Titlu;
                        c.GenId = carte.GenId;
                        c.AutorId = a.AutorId;
                        c.GEN = g;
                        c.AUTOR = a;
                        c.CarteId = carte.CarteId;
                    }
                }
            }
            return c;
        }

        private GEN getGen(GEN gen)
        {
            GEN g1 = new GEN();
            using (var context = new ModelGeneral())
            {
                foreach (var g in context.GENs)
                {
                    if (comparGen(g, gen))
                    {
                        g1.CARTE = g.CARTE;
                        g1.Descriere = g.Descriere;
                        g1.GenId = g.GenId;
                        break;
                    }
                }
            }

            return g1;
        }

        internal bool restituieCartea(int impId, string rewiew)
        {
            bool ok = false;
            using (var context = new ModelGeneral())
            {
                foreach (var imp in context.IMPRUMUTs)
                {
                    if (impId == imp.ImprumutId && imp.DataScadenta.HasValue == false)
                    {
                        imp.DataScadenta = DateTime.Now;
                        REVIEW rev = new REVIEW()
                        {
                            Text = rewiew
                        };
                        imp.REVIEW.Add(rev);
                        ok = true;
                    }
                }
                /*if (countCititorBehavior(cititor) >= 2)
                    changeStateCititor(cititor);
                */
                context.SaveChanges();
            }
            return ok;
        }

        internal void changeStateCititor(CITITOR cititor)
        {
            using (var context = new ModelGeneral())
            {
                context.CITITORs.Add(cititor);
                context.Entry(cititor).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        internal int countCititorBehavior(CITITOR cititor)
        {
            int nr_abateri = 0;
            using (var context = new ModelGeneral())
            {

                foreach (var imp in context.IMPRUMUTs)
                {


                    if (imp.CITITOR != null && comparCititori(imp.CITITOR, cititor))
                    {//afisez starea cititorului
                        if (!imp.DataRestituire.HasValue && imp.DataScadenta < DateTime.Now)
                            nr_abateri++;
                        else
                        {
                            if (imp.DataScadenta < imp.DataRestituire)
                                nr_abateri++;
                        }
                    }

                }
            }
            return nr_abateri;
        }

        internal bool existImprumut(IMPRUMUT imp)
        {
            bool foud = false;
            using (var context = new ModelGeneral())
            {

                foreach (var imprumut in context.IMPRUMUTs)
                {
                    if (comparImprumuts(imp, imprumut) && !imprumut.DataScadenta.HasValue)
                    {
                        Console.WriteLine(imp.CARTE.Titlu.Trim());
                        Console.WriteLine(imprumut.DataScadenta.HasValue);
                        Console.WriteLine(imprumut.ImprumutId);
                        foud = true;
                        break;
                    }
                }
            }
            return foud;
        }

        internal bool comparImprumuts(IMPRUMUT imp1, IMPRUMUT imp2)
        {
            if (imp1 is null || imp2 is null)
                return false;
            if (comparCarti(imp1.CARTE, imp2.CARTE) && comparCititori(imp2.CITITOR, imp1.CITITOR))
                return true;
            return false;
        }

        //aceasta metoda verifica daca cartea primita ca parametru exista in baza de date
        internal bool existCarte(CARTE carte)
        {
            bool foud = false;
            using (var context = new ModelGeneral())
            {

                foreach (CARTE book in context.CARTEs)
                {
                    if (comparCarti(book, carte))
                    {
                        foud = true;
                    }

                }
            }
            return foud;
        }
        //aceasta metoda verifica daca 2 carti sunt identice
        internal bool comparCarti(CARTE c1, CARTE c2)
        {
            if (c1 is null || c2 is null)
                return false;
            if (c1.Titlu.Trim().Equals(c2.Titlu.Trim()) && (comparGen(c1.GEN, c2.GEN) && comparAutori(c1.AUTOR, c2.AUTOR)))
                return true;
            return false;
        }

        //aceasta metoda verifica da 2 genuri sunt identice
        internal bool comparGen(GEN g1, GEN g2)
        {
            if (g1 is null)
                return false;
            if (g1.Descriere.Trim().Equals(g2.Descriere.Trim()))
            {
                return true;
            }
            return false;
        }

        //aceasta metoda verifica daca autorul primit ca parametru exista in baza de date
        internal bool existAutor(AUTOR autor)
        {
            bool foud = false;
            using (var context = new ModelGeneral())
            {
                foreach (var aut in context.AUTORs)
                {
                    Console.WriteLine("{0} as=asdasdasd",(autor.Nume.Trim().ToLower().Equals(aut.Nume.Trim().ToLower()) && autor.Prenume.Trim().ToLower().Equals(aut.Prenume.Trim().ToLower())));
                    if (autor.Nume.Trim().ToLower().Equals(aut.Nume.Trim().ToLower()) && autor.Prenume.Trim().ToLower().Equals(aut.Prenume.Trim().ToLower()))
                    {
                        foud = true;
                        break;
                    }
                }
            }
            return foud;
        }

        //aceasta metoda adauga autorul primit ca argument in baza de date
        internal void adaugaAutor(AUTOR a)
        {
            if (!existAutor(a))
            {
                using (var context = new ModelGeneral())
                {

                    AUTOR autor = new AUTOR()
                    {
                        Nume = a.Nume.Trim(),
                        Prenume = a.Prenume.Trim()

                    };
                    context.AUTORs.Add(autor);
                    context.SaveChanges();
                }
            }
        }

        //aceasta metoda verifica daca genul primit ca parametru exista in baza de date
        internal bool existGen(GEN gen)
        {
            bool foud = false;
            using (var context = new ModelGeneral())
            {

                var items = context.GENs;
                foreach (var g in items)
                {
                    if (g.Descriere.Trim().Equals(gen.Descriere.Trim()))
                    {
                        foud = true;
                    }
                }
            }
            return foud;
        }

        //aceasta metoda adauga un gen in bd daca este cazul
        internal void adaugaGen(GEN g)
        {
            if (!existGen(g))
            {
                using (var context = new ModelGeneral())
                {

                    GEN gen = new GEN()
                    {
                        Descriere = g.Descriere.Trim()

                    };
                    context.GENs.Add(gen);
                    context.SaveChanges();
                }
            }
        }


        //aceasta metoda returneaza un ArrayList cu cartile care au genul primit ca argument
        internal ICollection<CARTE> getBooks(GEN gen)
        {
            ICollection<CARTE> books = new List<CARTE>();
            using (var context = new ModelGeneral())
            {
                var CARTI = context.CARTEs;
                foreach (var carte in CARTI)
                {
                   
                        if (comparGen(carte.GEN, gen))
                        {
                            CARTE c = new CARTE()
                            {
                                AUTOR = carte.AUTOR,
                                GEN = carte.GEN,
                                Titlu = carte.Titlu,
                                CarteId = carte.CarteId,
                                AutorId = carte.CarteId,
                                GenId = carte.GenId,
                                IMPRUMUT = carte.IMPRUMUT
                            };
                            books.Add(c);
                        }
                    
                }
            }
            return books;
        }

        //aceasta metoda verifica daca un cititor exista in bd
        internal bool existCititor(CITITOR cititor)
        {
            bool foud = false;
            using (var context = new ModelGeneral())
            {
                var cititori = context.CITITORs;
                foreach (var reader in cititori)
                {
                    if (reader.Email.Trim().Equals(cititor.Email.Trim()))
                    {//afisez starea cititorului
                        foud = true;
                    }

                }
            }
            return foud;
        }

        //acesta metoda adauga un cititor in baza de date
        internal void adaugaCititor(CITITOR cit)
        {
            if (!existCititor(cit))
            {
                using (var context = new ModelGeneral())
                {
                    CITITOR c = new CITITOR()
                    {
                        Adresa = cit.Adresa.Trim(),
                        Email = cit.Email.Trim(),
                        Nume = cit.Nume.Trim(),
                        Prenume = cit.Prenume.Trim()
                    };
                    context.CITITORs.Add(c);
                    context.SaveChanges();
                }
            }
        }

        //aceasta metoda verifica daca 2 cititori sunt identici
        internal bool comparCititori(CITITOR c1, CITITOR c2)
        {
            if (c1 is null || c2 is null)
                return false;
            if (c1.Email.Trim().Equals(c2.Email.Trim()))
                return  true;
            return false;
        }

        //aceasta metoda verifica starea cititorului primit ca argument
        internal bool verfStare(CITITOR cititor)
        {
            int nr_abateri = 0;
            using (var context = new ModelGeneral())
            {
               
                foreach (var imp in context.IMPRUMUTs)
                {

                   
                    if (imp.CITITOR != null && comparCititori(imp.CITITOR,cititor))
                    {//afisez starea cititorului
                        if (!imp.DataRestituire.HasValue && imp.DataScadenta < DateTime.Now)
                            nr_abateri++;
                        else
                        {
                            if (imp.DataScadenta < imp.DataRestituire)
                                nr_abateri++;
                        }
                    }

                }
            }
            if (nr_abateri > 1)
            {
                return false;
            }
            return true;
        }

        //aceasta metoda verifica daca 2 autori sunt identici
        internal bool comparAutori(AUTOR a1, AUTOR a2)
        {
            if (a1 is null)
                return false;
            if (a1.Nume.Trim().Equals(a2.Nume.Trim()) && a1.Prenume.Trim().Equals(a2.Prenume.Trim()))
                return true;
            return false;
        }

        
        //aceasta metoda verifica daca cartea primita ca argument este disponibila
        internal bool verfDisponibilitateCarte(int carteId)
        {
           
            bool found = true;
            using (var context = new ModelGeneral())
            {
                var items = context.IMPRUMUTs;
                foreach (var imp in items)
                {
                    if (imp.CARTE.CarteId == carteId)
                    {
                
                        if (! imp.DataScadenta.HasValue)
                        {
                            found = false;
                            break;
                        }
                    }
                }
            }
            return found;
        }

        //aceasta metoda returneaza data(cea mai mica) la care cartea primita ca argument va fi disponibila
        internal DateTime searchDataScadenta(CARTE carte)
        {
            DateTime d1= DateTime.Now;
            using (var context = new ModelGeneral())
            {
                foreach (var imp in context.IMPRUMUTs)
                {
                   
                    if (comparCarti(imp.CARTE, carte) && imp.DataScadenta.HasValue)
                    {
                        if (d1 > (DateTime)imp.DataScadenta)
                            d1 = (DateTime)imp.DataScadenta;
                    }
                }
          
            }
            return d1;
        }

        //aceasta metoda primeste un gen si returneaza o colectie de carti de genul primit ca argument
        internal ICollection<CARTE> getCartiDeGen(GEN g)
        {
            ICollection<CARTE> list = new List<CARTE>();
            using (var context = new ModelGeneral())
            {
                foreach (var carte in context.CARTEs)
                {
                    if (comparGen(carte.GEN, g))
                    {
                        CARTE c = new CARTE()
                        {
                            AUTOR = carte.AUTOR,
                            GEN = carte.GEN,
                            Titlu = carte.Titlu,
                            CarteId = carte.CarteId,
                            AutorId = carte.CarteId,
                            GenId = carte.GenId,
                            IMPRUMUT = carte.IMPRUMUT
                        };
                        list.Add(c);
                    }
                }
            }
            return list;
        }

        //aceasta metoda returneaza tru daca s-a putut face imprumutul si il realizeaza , false c.c.
        internal bool imprumutaCarte(int carteID, CITITOR cit)
        {
            if (verfDisponibilitateCarte(carteID))
            {
                CARTE c = new CARTE();
                using (var context = new ModelGeneral())
                {
                    foreach (var carte in context.CARTEs)
                    {
                        if (carte.CarteId == carteID)
                        {
                            c = carte;
                            break;
                        }
                    }

                    cit.CititorId = getCitirorId(cit);

                    IMPRUMUT imp = new IMPRUMUT()
                    {
                        CARTE = c,
                        DataImprumut = DateTime.Now,
                        DataRestituire = DateTime.Now.AddDays(14),
                        CITITOR = cit
                    };
                    Console.WriteLine(cit.Email);
                    context.IMPRUMUTs.Add(imp);
                    context.SaveChanges();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        //aceste 3 metode cauta o carte in functie de titlu si/sau autor
        internal bool searchBook(GEN g, String Titlu)
        {
            bool foud = false;
            using (var context = new ModelGeneral())
            {
                foreach(var carte in context.CARTEs)
                {
                    if (carte.Titlu.Trim().Equals(Titlu.Trim()) && comparGen(g,carte.GEN))
                    {
                        foud = true;
                        break;
                    }
                }
            }
            return foud;
        }
        internal bool searchBook(GEN g, AUTOR a)
        {
            bool foud = false;
            using (var context = new ModelGeneral())
            {
                foreach (var carte in context.CARTEs)
                {
                    if (comparAutori(carte.AUTOR,a)&& comparGen(g, carte.GEN))
                    {
                        foud = true;
                        break;
                    }
                }
            }
            return foud;
        }
        internal bool searchBook(GEN g, AUTOR a, String Titlu)
        {
            bool foud = false;
            using (var context = new ModelGeneral())
            {
                foreach (var carte in context.CARTEs)
                {
                    if ((comparAutori(carte.AUTOR, a) && comparGen(g, carte.GEN)) && carte.Titlu.Trim().Equals(Titlu.Trim()))
                    {
                        foud = true;
                        break;
                    }
                }
            }
            return foud;
        }

        internal int getCitirorId(CITITOR cit)
        {
            if (existCititor(cit))
            {
                int citID = -1;
                using (var context = new ModelGeneral())
                {
                    foreach (var cititor in context.CITITORs)
                    {
                        if (comparCititori(cit,cititor))
                        {
                            citID = cititor.CititorId;
                            break;
                        }
                    }
                }
                return citID;
            }
            return -1;
        }
        internal int getBookId(GEN g, String Titlu)
        {
            int id=-1;
            using (var context = new ModelGeneral())
            {
                foreach (var carte in context.CARTEs)
                {
                    if (carte.Titlu.Trim().Equals(Titlu.Trim()) && comparGen(g, carte.GEN))
                    {
                        id = carte.CarteId;
                        break;
                    }
                }
            }
            return id;
        }
        internal int getBookId(GEN g, AUTOR a)
        {
            int id = -1;
            using (var context = new ModelGeneral())
            {
                foreach (var carte in context.CARTEs)
                {
                    if (comparAutori(carte.AUTOR, a) && comparGen(g, carte.GEN))
                    {
                        id = carte.CarteId;
                        break;
                    }
                }
            }

            return id;
        }
        internal int getBookId(GEN g, AUTOR a, String Titlu)
        {
            int id = -1;
            using (var context = new ModelGeneral())
            {
                foreach (var carte in context.CARTEs)
                {
                    if ((comparAutori(carte.AUTOR, a) && comparGen(g, carte.GEN)) && carte.Titlu.Trim().Equals(Titlu.Trim()))
                    {
                        id = carte.CarteId;
                        break;
                    }
                }
            }
            return id;
        }

        internal List<CITITOR> ReaderBetweenDates(DateTime d1, DateTime d2)
        {
            List<CITITOR> list = new List<CITITOR>();
            using (var context = new ModelGeneral())
            {
                var citiori = context.CITITORs.Where(x => x.IMPRUMUT.Where(y => (y.DataImprumut >= d1 && y.DataImprumut <= d2) || (y.DataRestituire >= d1 && y.DataRestituire <= d2)).Count() != 0);
                
                foreach (var cititor in citiori)
                {
                   list.Add(cititor);
                }
            }
            return list;

        }

        internal List<CARTE> MostWantedBooks()
        {//Cele mai solicitate carti.
            List<CARTE> list = new List<CARTE>();
            using (var context = new ModelGeneral())
            {
                int nr_taken = 0;
                foreach (var carte in context.CARTEs)
                {
                    int nr_exemp_date = context.IMPRUMUTs.Where(x => x.CARTE.Titlu.Trim().Equals(carte.Titlu.Trim()) &&
                    (x.CARTE.AUTOR.Nume.Trim().Equals(carte.AUTOR.Nume) && x.CARTE.AUTOR.Prenume.Trim().Equals(carte.AUTOR.Prenume))).Count();

                    if (nr_exemp_date > nr_taken)
                        nr_taken = nr_exemp_date;

                }

                foreach (var carte in context.CARTEs)
                {
                    int nr_exemp_date = context.IMPRUMUTs.Where(x => x.CARTE.Titlu.Trim().Equals(carte.Titlu.Trim()) &&
                    (x.CARTE.AUTOR.Nume.Trim().Equals(carte.AUTOR.Nume) && x.CARTE.AUTOR.Prenume.Trim().Equals(carte.AUTOR.Prenume))).Count();

                    if (nr_exemp_date == nr_taken )
                    {
                        bool foud = false;
                        foreach (var crt in list)
                        {
                            if (comparCarti(crt, carte))
                            {
                                foud = true;
                                break;
                            }
                        }
                        if (!foud)
                        {
                            CARTE careAux = new CARTE()
                            {
                                AUTOR = carte.AUTOR,
                                AutorId = carte.AutorId,
                                CarteId = carte.CarteId,
                                GEN = carte.GEN,
                                GenId = carte.GenId,
                                IMPRUMUT = carte.IMPRUMUT,
                                Titlu = carte.Titlu
                            };
                            list.Add(careAux);
                        }
                    }
                }
            }
            return list;
        }

        internal List<AUTOR> getMostFaimousAutors()
        {
            List<AUTOR> list = new List<AUTOR>();
            using (var context = new ModelGeneral())
            {
                int nr_taken = 0;
                foreach (var autor in context.AUTORs)
                {
                    int nr_exemp_date = context.IMPRUMUTs.Where(x =>
                    x.CARTE.AUTOR.Nume.Trim().Equals(autor.Nume) && x.CARTE.AUTOR.Prenume.Trim().Equals(autor.Prenume)).Count();

                    if (nr_exemp_date > nr_taken)
                        nr_taken = nr_exemp_date;

                }

                foreach (var autor in context.AUTORs)
                {
                    int nr_exemp_date = context.IMPRUMUTs.Where(x =>
                    x.CARTE.AUTOR.Nume.Trim().Equals(autor.Nume) && x.CARTE.AUTOR.Prenume.Trim().Equals(autor.Prenume)).Count();

                    if (nr_exemp_date == nr_taken)
                    {
                        bool found = false;
                        foreach(var aut in list)
                        {
                            if (comparAutori(aut, autor))
                            {
                                found = true;
                                break;
                            }
                        }
                        if(!found)
                            list.Add(autor);
                    }
                }
            }
            return list;
        }

        internal List<GEN> getMostComuneGens()
        {//Genurile cele mai solicitate
            List<GEN> lista = new List<GEN>();
            using (var context = new ModelGeneral())
            {
                int nr_taken = 0;
                foreach (var gen in context.GENs)
                {
                    int nr_exemp_date = context.IMPRUMUTs.Where(x =>
                        x.CARTE.GEN.Descriere.Trim().Equals(gen.Descriere)).Count();

                    if (nr_exemp_date > nr_taken)
                        nr_taken = nr_exemp_date;

                }

                foreach (var gen in context.GENs)
                {
                    int nr_exemp_date = context.IMPRUMUTs.Where(x =>
                        x.CARTE.GEN.Descriere.Trim().Equals(gen.Descriere)).Count();

                    if (nr_exemp_date == nr_taken)
                    {
                        bool foud = false;
                        foreach(var g in lista)
                        {
                            if (comparGen(g, gen))
                            {
                                foud = true;
                                break;
                            }
                        }
                        if(!foud)
                            lista.Add(gen);
                    }
                }
            }
            return lista;
        }

        internal List<REVIEW> getReviews(CARTE Carte)
        {
            List<REVIEW> list = new List<REVIEW>();
            using (var context = new ModelGeneral())
            {
                foreach (var rev in context.REVIEWs)
                {
                    if ((rev.IMPRUMUT.CARTE.Titlu.Trim().Equals(Carte.Titlu.Trim()) && rev.IMPRUMUT.CARTE.AUTOR.Nume.Trim().Equals(Carte.AUTOR.Nume.Trim())
                        && rev.IMPRUMUT.CARTE.AUTOR.Prenume.Trim().Equals(Carte.AUTOR.Prenume.Trim())))

                        list.Add(rev);
                }
            }
            return list;
        }

        internal List<CARTE> getAllBooks()
        {
            List<CARTE> list = new List<CARTE>();
            
            using (var context = new ModelGeneral())
            {
                foreach(CARTE carte in context.CARTEs){
                    if (!(carte is null) && carte.Titlu.Trim().Length > 0)
                    {
                        CARTE c = new CARTE()
                        {
                            Titlu = carte.Titlu,
                            AUTOR = carte.AUTOR,
                            CarteId = carte.CarteId,
                            AutorId = carte.AutorId,
                            GEN = carte.GEN,
                            GenId = carte.GenId,
                            IMPRUMUT = carte.IMPRUMUT
                        };
                        list.Add(c);
                    }
                }
            }
            return list;
        }

        internal List<GEN> getAllGens()
        {
            List<GEN> list = new List<GEN>();
            using (var context = new ModelGeneral())
            {
                foreach (GEN gen in context.GENs)
                {
                    list.Add(gen);
                }
            }
            return list;
        }

        internal List<AUTOR> getAllAutors()
        {
            List<AUTOR> list = new List<AUTOR>();
            using (var context = new ModelGeneral())
            {
                foreach (AUTOR a in context.AUTORs)
                {
                    list.Add(a);
                }
            }
            return list;
        }

        internal List<IMPRUMUT> getAllImprumutsAlive()
        {
            List<IMPRUMUT> list = new List<IMPRUMUT>();
            using (var context = new ModelGeneral())
            {
                foreach (IMPRUMUT impt in context.IMPRUMUTs)
                {
                    if (impt.DataScadenta.HasValue == false)
                    {
                        GEN g = new GEN()
                        {
                            Descriere = impt.CARTE.GEN.Descriere,
                            GenId = impt.CARTE.GEN.GenId
                        };

                        AUTOR a = new AUTOR()
                        {
                            AutorId = impt.CARTE.AutorId.Value,
                            Nume = impt.CARTE.AUTOR.Nume,
                            Prenume = impt.CARTE.AUTOR.Prenume
                            
                        };

                        foreach (var carte in impt.CARTE.AUTOR.CARTE)
                        {
                            CARTE crt = new CARTE()
                            {
                                Titlu = impt.CARTE.Titlu,
                                GenId = impt.CARTE.GenId,
                                AutorId = a.AutorId,
                                AUTOR = a
                            };
                            a.CARTE.Add(carte);
                        }

                        CARTE c = new CARTE()
                        {
                            Titlu = impt.CARTE.Titlu,
                            GenId = impt.CARTE.GenId,
                            AutorId = a.AutorId,
                            GEN = g,
                            AUTOR = a,
                            CarteId = impt.CARTE.CarteId
                        };
                        c.CarteId = c.CarteId;

                        CITITOR cit = new CITITOR()
                        {
                            Nume = impt.CITITOR.Nume,
                            Prenume = impt.CITITOR.Prenume,
                            Adresa = impt.CITITOR.Adresa,
                            CititorId = impt.CITITOR.CititorId,
                            Email = impt.CITITOR.Email,
                            Stare = impt.CITITOR.Stare
                        };

                        ICollection<REVIEW> reviews = new List<REVIEW>();
                        foreach (var r in impt.REVIEW)
                        {
                            REVIEW rev = new REVIEW()
                            {
                                Text = r.Text,
                                ImprumutId = r.ImprumutId,
                                ReviewId = r.ReviewId
                            };
                            reviews.Add(r);
                        }
                        

                        IMPRUMUT i = new IMPRUMUT()
                        {
                            CARTE = c,
                            CarteId = impt.CarteId,
                            CITITOR = cit,
                            CititorId = impt.CititorId,
                            DataImprumut = impt.DataImprumut,
                            DataRestituire = impt.DataRestituire,
                            DataScadenta = impt.DataScadenta,
                            ImprumutId = impt.ImprumutId,
                            REVIEW = reviews
                        };
                        
                        list.Add(i);
                    }
                }
            }
            return list;
        }

        internal List<REVIEW> getAllReviews()
        {
            List<REVIEW> list = new List<REVIEW>();
            using (var context = new ModelGeneral())
            {
                foreach (REVIEW rev in context.REVIEWs)
                {
                    list.Add(rev);
                }
            }
            return list;
        }

        internal List<CITITOR> getAllCititors()
        {
            List<CITITOR> list = new List<CITITOR>();
            using (var context = new ModelGeneral())
            {
                foreach (CITITOR cit in context.CITITORs)
                {
                    list.Add(cit);
                }
            }
            return list;
        }

        public bool existReview(int idCarte)
        {
            bool found = false;
            using (var context = new ModelGeneral())
            {
                foreach (var imp in context.IMPRUMUTs)
                {
                    if (imp.CARTE.CarteId == idCarte)
                    {
                        foreach (var rev_imp in imp.REVIEW)
                        {
                            found = true;
                        }
                    }
                }
            }
            return found;
        }

        public List<REVIEW> getReviewByBookID(int idCarte)
        {
            List<REVIEW> l_rev = new List<REVIEW>();
            using (var context = new ModelGeneral())
            {
                foreach (var imp in context.IMPRUMUTs)
                {
                    if(imp.CARTE.CarteId == idCarte)
                    {
                        foreach (var rev_imp in imp.REVIEW)
                        {
                            REVIEW rev = new REVIEW()
                            {
                                ReviewId = rev_imp.ReviewId,
                                IMPRUMUT = rev_imp.IMPRUMUT,
                                ImprumutId = rev_imp.ImprumutId,
                                Text = rev_imp.Text
                            };
                            l_rev.Add(rev);
                        }
                    }
                }
            }
            return l_rev;
        }
    }
}
