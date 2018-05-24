using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteca_P1;
namespace WcfServiceBiblio
{
    internal class Convertor
    {
        internal AUTOR getAutor(AutorDTO a)
        {
            return new AUTOR()
            {
                Nume = a.Nume,
                Prenume = a.Prenume
            };
        }
        internal AutorDTO getAutorDTO(AUTOR a)
        {
            return new AutorDTO()
            {
                AutorId = a.AutorId,
                Nume = a.Nume,
                Prenume = a.Prenume
            };
        }

        internal GEN getGen(GenDTO gen)
        {
            return new GEN()
            {
                Descriere = gen.Descriere,
            };
        }
        internal GenDTO getGenDTO(GEN gen)
        {
            return new GenDTO()
            {
                Descriere = gen.Descriere,
                GenId = gen.GenId
            };
        }

        internal CARTE getCARTE(CarteDTO c)
        {
            return new CARTE()
            {
                Titlu = c.Titlu,
                AUTOR = getAutor(c.autor),
                GEN = getGen(c.gen)
            };
        }
        internal CarteDTO getCARTEDTO(CARTE c)
        {
            return new CarteDTO()
            {
                AutorId = c.AutorId,
                GenId = c.GenId,
                CarteId = c.CarteId,
                Titlu = c.Titlu,
                autor = getAutorDTO(c.AUTOR),
                gen = getGenDTO(c.GEN)
            };
        }

        internal IMPRUMUT getImprumut(ImprumutDTO imp)
        {
            return new IMPRUMUT()
            {
                ImprumutId = imp.ImprumutId,
                CARTE = getCARTE(imp.carte),
                CITITOR = getCititor(imp.cititor),
                DataImprumut = imp.DataImprumut,
                DataRestituire = imp.DataRestituire,
                DataScadenta = imp.DataScadenta
            };
        }
        internal ImprumutDTO getImprumutDTO(IMPRUMUT imp)
        {
            return new ImprumutDTO()
            {
                ImprumutId = imp.ImprumutId,
                carte = getCARTEDTO(imp.CARTE),
                cititor = getCititorDTO(imp.CITITOR),
                DataImprumut = imp.DataImprumut,
                DataRestituire = imp.DataRestituire,
                DataScadenta = imp.DataScadenta
            };
        }

        internal REVIEW getReview(ReviewDTO review)
        {
            return new REVIEW()
            {
                Text = review.Text
            };
        }
        internal ReviewDTO getReviewDTO(REVIEW review)
        {
            return new ReviewDTO()
            {
                ReviewId = review.ReviewId,
                Text = review.Text
            };
        }

        internal CititorDTO getCititorDTO(CITITOR cititor)
        {
            return new CititorDTO()
            {
                Adresa = cititor.Adresa,
                Email = cititor.Adresa,
                Nume = cititor.Nume,
                Prenume = cititor.Prenume
            };
        }
        internal CITITOR getCititor(CititorDTO cititor)
        {
            return new CITITOR()
            {
                Adresa = cititor.Adresa,
                Email = cititor.Adresa,
                Nume = cititor.Nume,
                Prenume = cititor.Prenume
            };
        }

        internal List<CititorDTO> getListCititori(List<CITITOR> list)
        {
            List<CititorDTO> list_c = new List<CititorDTO>();
            foreach (var cit in list)
            {
                CititorDTO c = getCititorDTO(cit);
                list_c.Add(c);
            }
            return list_c;
        }

        internal List<GenDTO> getListGens(List<GEN> list)
        {
            List<GenDTO> list_g = new List<GenDTO>();
            foreach (var gen in list)
            {
                GenDTO g = getGenDTO(gen);
                list_g.Add(g);
            }
            return list_g;
        }

        internal List<ReviewDTO> getListReview(List<REVIEW> list)
        {
            List<ReviewDTO> list_r = new List<ReviewDTO>();
            foreach (var rev in list)
            {
                ReviewDTO r = getReviewDTO(rev);
                list_r.Add(r);
            }
            return list_r;
        }

        internal List<AutorDTO> getListAutori(List<AUTOR> list)
        {
            List<AutorDTO> list_r = new List<AutorDTO>();
            foreach (var a in list)
            {
                AutorDTO r = getAutorDTO(a);
                list_r.Add(r);
            }
            return list_r;
        }

        internal List<CarteDTO> getListCarti(ICollection<CARTE> collection)
        {
            List<CarteDTO> list_c = new List<CarteDTO>();
            
            foreach (var book in collection)
            {
                CARTE c1 = new CARTE() {
                    AUTOR = book.AUTOR,
                    AutorId = book.AutorId,
                    CarteId = book.CarteId,
                    GEN = book.GEN,
                    GenId = book.GenId,
                    IMPRUMUT = book.IMPRUMUT,
                    Titlu = book.Titlu
                };
                CarteDTO c = getCARTEDTO(c1);
                list_c.Add(c);
            }
            return list_c;
        }

        internal List<ImprumutDTO> getListImprumuturi(ICollection<IMPRUMUT> collection)
        {
            List<ImprumutDTO> list_c = new List<ImprumutDTO>();
            
            foreach (var imprumut_colection in collection)
            {
                if (!(imprumut_colection is null))
                {
                    ImprumutDTO imp = getImprumutDTO(imprumut_colection);
                    list_c.Add(imp);
                }
            }
            return list_c;
        }
    }
}
