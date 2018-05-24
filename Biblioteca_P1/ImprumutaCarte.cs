using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_P1
{
    public class ImprumutaCarte
    {
        API a = new API();

        //aceasta metoda se asigura ca cititorul exista in baza de date
        public bool adaugaCititor(CITITOR cit)
        {
            bool ret = a.existCititor(cit);
            a.adaugaCititor(cit);
            return true;
        }

        //aceasta metoda returneaza starea cititorului
        public bool verfStare(CITITOR cit)
        {
            return a.verfStare(cit);
        }

        //aceasta metoda verifica daca o carte este disponibila
        public bool verificaDisponibila(int carte)
        {
            return a.verfDisponibilitateCarte(carte);

        }

        //aceasta metoda returneaza data cea mai apropiata de timp la care cartea va fi disponibila
        public DateTime searchDataScadenta(CARTE carte) => a.searchDataScadenta(carte);

        public bool existaCarte(CARTE c1)
        {
            return a.existCarte(c1);
        }
        public CARTE GetCARTE(int id) => a.GetCarte(id);
        public ICollection<CARTE> getAcelasiGen(GEN g)
        {
            return a.getCartiDeGen(g);
        }
        public bool imprumutaCarte(int c1,CITITOR cit)
        {
            return a.imprumutaCarte(c1, cit); //sigur va gasi o carte
        }

        public bool searchBook(GEN g, String Titlu) => a.searchBook(g, Titlu);
        public bool searchBook(GEN g, AUTOR a1) => a.searchBook(g, a1);
        public bool searchBook(GEN g, AUTOR a1, String Titlu) => a.searchBook(g, a1,Titlu);
    
        public int getBookId(GEN g, String Titlu) => a.getBookId(g, Titlu);
        public int getBookId(GEN g, AUTOR a1) => a.getBookId(g, a1);
        public int getBookId(GEN g, AUTOR a1, String Titlu) => a.getBookId(g, a1, Titlu);

    }
}
