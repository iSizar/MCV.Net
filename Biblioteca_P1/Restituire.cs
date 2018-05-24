using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_P1
{
    public class Restituire
    {
        // a 3-a metoda
        API a = new API();
        public bool existaCarte(CARTE carte)=>a.existCarte(carte);

        public bool existaCititor(CITITOR cit) => a.existCititor(cit);

        public bool existaImprumut(IMPRUMUT imp) => a.existImprumut(imp);

        public bool restituieCartea(int impId, string rewiew) => a.restituieCartea(impId, rewiew);
    }
}
