using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_P1
{
    public class menager
    {
        public GEN getGEN(string descriere)
        {
            GEN g1 = new GEN()
            {
                Descriere = descriere
            };
            return g1;
        }

        public AUTOR getAutor(string nume, string prenume)
        {
            AUTOR a1 = new AUTOR()
            {
                Nume = nume,
                Prenume = prenume
            };
            return a1;
        }

        public CARTE getCarte(string titlu, AUTOR autor, GEN gen)
        {
            CARTE c1 = new CARTE()
            {
                Titlu = titlu,
                AUTOR = autor,
                GEN = gen
            };
            return c1;
        }

        public CITITOR getCititor(string nume, string prenume, string email, string adresa)
        {
            var stare = new byte[16];
            for (int i = 0; i < 15; ++i)
            {
                stare[i] = 0;
            }

            return new CITITOR()
            {
                Nume = nume,
                Prenume = prenume,
                Email = email,
                Adresa = adresa,
                Stare = stare
            };

        }

        public bool searchForReader(string nume,string prenume)
        {
            using(var context = new ModelGeneral())
            {
                foreach(var cititor in context.CITITORs)
                {
                    if(cititor.Nume.Trim().Equals(nume.Trim()) && cititor.Prenume.Trim().Equals(prenume.Trim()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public CITITOR findCititor(string nume, string prenume)
        {
            CITITOR c = new CITITOR();
            using (var context = new ModelGeneral())
            {
                foreach (var cititor in context.CITITORs)
                {
                    if (cititor.Nume.Trim().Equals(nume.Trim()) && cititor.Prenume.Trim().Equals(prenume.Trim()))
                    {
                        return cititor;
                    }
                }
            }
            return c;
        }

        public bool searchForBook(string titlu, string numeA, string prenumeA)
        {
            using (var context = new ModelGeneral())
            {
                foreach (var carte in context.CARTEs)
                {
                    if (carte.Titlu.Trim().Equals(titlu) && (carte.AUTOR.Nume.Trim().Equals(numeA.Trim()) && carte.AUTOR.Prenume.Trim().Equals(prenumeA.Trim())))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public CARTE findBook(string titlu,string numeA,string prenumeA)
        {
            CARTE c = new CARTE();
            using(var context = new ModelGeneral())
            {
                foreach(var carte in context.CARTEs)
                {
                    if(carte.Titlu.Trim().Equals(titlu) &&( carte.AUTOR.Nume.Trim().Equals(numeA.Trim() ) && carte.AUTOR.Prenume.Trim().Equals(prenumeA.Trim())))
                    {
                         c = carte;
                    }
                }
            }
            return c;
        }
    }
}
