using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_P1
{
    public class AchizitionareCarti
    {
        API a = new API();
        public bool achizitioneazaCarte(CARTE carte,int nr_carti)
        {
            a.adaugaAutor(carte.AUTOR);
            a.adaugaGen(carte.GEN);
            if (nr_carti <= 0)
            {
                CARTE def = new CARTE()
                {
                    Titlu="defoult",
                    AUTOR = carte.AUTOR,
                    GEN = carte.GEN
                };
                a.agaugaCarte(def);
            }
            for (int i = 0; i < nr_carti; ++i)
            {
                a.agaugaCarte(carte);
            }

            return true;
        }
        
    }
}
