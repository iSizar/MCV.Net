using Biblioteca_P1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceBiblio
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode =InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        private AchizitionareCarti cumparator = new AchizitionareCarti();
        private ImprumutaCarte imprumut = new ImprumutaCarte();
        private Restituire restituire = new Restituire();
        private Statistica statistica = new Statistica();
        private Convertor convertor = new Convertor();

        public bool achizitioneazaCarte(CarteDTO carte, int nr_carti) =>cumparator.achizitioneazaCarte(convertor.getCARTE(carte),nr_carti);
        

        public bool adaugaCititor(CititorDTO cITITOR) => imprumut.adaugaCititor(convertor.getCititor(cITITOR));

        public void agaugaCarti(CarteDTO carte, int nr_carti) => cumparator.achizitioneazaCarte(convertor.getCARTE(carte), nr_carti);

        public bool existaCarte(CarteDTO c1) => imprumut.existaCarte(convertor.getCARTE(c1));

        public bool existaCititor(CititorDTO cit) => restituire.existaCititor(convertor.getCititor(cit));

        public bool existaImprumut(ImprumutDTO imp) => restituire.existaImprumut(convertor.getImprumut(imp));

        public ICollection<CarteDTO> getAcelasiGen(GenDTO g) {
            ICollection<CARTE> list = imprumut.getAcelasiGen(convertor.getGen(g));
            ICollection<CarteDTO> list_c = new List<CarteDTO>();
            foreach (var book in list)
            {
                CarteDTO c_c = convertor.getCARTEDTO(book);
                list_c.Add(c_c);
            }
            return list_c;
        }

        public int getBookId1(GenDTO g, string Titlu) => imprumut.getBookId(convertor.getGen(g), Titlu);

        public int getBookId2(GenDTO g, AutorDTO a1) => imprumut.getBookId(convertor.getGen(g), convertor.getAutor(a1));

        public int getBookId3(GenDTO g, AutorDTO a1, string Titlu) => imprumut.getBookId(convertor.getGen(g), convertor.getAutor(a1), Titlu);

        public bool imprumutaCarte(int c1, CititorDTO cit) => imprumut.imprumutaCarte(c1, convertor.getCititor(cit));

        public bool restituieCartea(int impId, string rewiew) => restituire.restituieCartea(impId, rewiew);

        public bool searchBook1(GenDTO g, string Titlu) => imprumut.searchBook(convertor.getGen(g) , Titlu);

        public bool searchBook2(GenDTO g, AutorDTO a1) => imprumut.searchBook(convertor.getGen(g), convertor.getAutor(a1));

        public bool searchBook3(GenDTO g, AutorDTO a1, string Titlu) => imprumut.searchBook(convertor.getGen(g), convertor.getAutor(a1), Titlu);

        public DateTime searchDataScadenta(CarteDTO carte) => imprumut.searchDataScadenta(convertor.getCARTE(carte));

        public bool verfStare(CititorDTO cit) => imprumut.verfStare(convertor.getCititor(cit));

        public bool verificaDisponibila(int carte) => imprumut.verificaDisponibila(carte);


        public List<CititorDTO> ReaderBetweenDates(DateTime d1, DateTime d2) => convertor.getListCititori(statistica.ReaderBetweenDates(d1, d2));

        public List<CarteDTO> MostWantedBooks() => convertor.getListCarti(statistica.MostWantedBooks());

        public List<AutorDTO> getMostFaimousAutors() => convertor.getListAutori(statistica.getMostFaimousAutors());

        public List<GenDTO> getMostComuneGens() => convertor.getListGens(statistica.getMostComuneGens());

        public List<ReviewDTO> getReviews(CarteDTO Carte) => convertor.getListReview(statistica.getReviews(convertor.getCARTE(Carte)));

        public GenDTO getGENDTO(string descriere)
        {
            return new GenDTO()
            {
                Descriere = descriere
            };
        }

        public AutorDTO getAutorDTO(string nume, string prenume)
        {
            return new AutorDTO()
            {
                Nume = nume,
                Prenume = prenume
            };
        }

        public CarteDTO getCarteDTO(string titlu, AutorDTO autor, GenDTO gen)
        {
            return new CarteDTO()
            {
                Titlu = titlu,
                autor = autor,
                gen = gen
            };
        }

        public CititorDTO getCititorDTO(string nume, string prenume, string email, string adresa)
        {
            return new CititorDTO()
            {
                Nume = nume,
                Prenume = prenume,
                Email = email,
                Adresa = adresa
            };
        }

        public ImprumutDTO getImprumutDTO(DateTime dataRes, DateTime dataImp, CarteDTO c, CititorDTO cit)
        {
            return new ImprumutDTO() {
                carte = c,
                cititor = cit,
                DataImprumut = dataImp,
                DataRestituire = dataRes
            };
        }

        public ReviewDTO getReviewDTO(string test)
        {
            return new ReviewDTO()
            {
                Text = test
            };
        }



        public List<CarteDTO> getAllBooks() => convertor.getListCarti(statistica.getAllBooks());

        public List<GenDTO> getAllGens() => convertor.getListGens(statistica.getAllGens());

        public List<AutorDTO> getAllAutors() => convertor.getListAutori(statistica.getAllAutors());

        public List<ImprumutDTO> getAllImprumutsNotAlive() => convertor.getListImprumuturi(statistica.getAllImprumuts());

        public List<ReviewDTO> getAllReviews() => convertor.getListReview(statistica.getAllReviews());

        public List<CititorDTO> getAllCititors() => convertor.getListCititori(statistica.getAllCititors());

        public List<ReviewDTO> getReviewByBookID(int id) => convertor.getListReview(statistica.getReviewByBookID(id));

        public bool existReview(int id) => statistica.existReview(id);

        public CarteDTO searchForCArte(int id) => convertor.getCARTEDTO(imprumut.GetCARTE(id));
        
    }
}
