using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca_P1
{
    public class Statistica
    {
        API a = new API();
        public List<CITITOR> ReaderBetweenDates(DateTime d1, DateTime d2) => a.ReaderBetweenDates(d1, d2);

        public List<CARTE> MostWantedBooks() => a.MostWantedBooks();

        public List<AUTOR> getMostFaimousAutors() => a.getMostFaimousAutors();

        public List<GEN> getMostComuneGens() => a.getMostComuneGens();

        public List<REVIEW> getReviews(CARTE Carte) => a.getReviews(Carte);

        public List<CARTE> getAllBooks() => a.getAllBooks();

        public List<GEN> getAllGens() => a.getAllGens();

        public List<AUTOR> getAllAutors() => a.getAllAutors();

        public List<IMPRUMUT> getAllImprumuts() => a.getAllImprumutsAlive();

        public List<REVIEW> getAllReviews() => a.getAllReviews();

        public List<CITITOR> getAllCititors() => a.getAllCititors();

        public List<REVIEW> getReviewByBookID(int id) => a.getReviewByBookID(id);

        public bool existReview(int id) => a.existReview(id);

    }
}
