using Biblioteca_P1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceBiblio
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
 {      /// <summary>
        ///         PRIMA METODA
        /// </summary>
        /// <param name="carte"></param>
        /// <param name="nr_carti"></param>
        [OperationContract]
        bool achizitioneazaCarte(CarteDTO carte, int nr_carti);


        /// <summary>
        ///      A DOUA METODA
        /// </summary>
        /// <param name="cit"></param>
        /// <returns></returns>
        [OperationContract]
        GenDTO getGENDTO(string descriere);

        [OperationContract]
        AutorDTO getAutorDTO(string nume, string prenume);

        [OperationContract]
        CarteDTO getCarteDTO(string titlu, AutorDTO autor, GenDTO gen);

        [OperationContract]
        CititorDTO getCititorDTO(string nume, string prenume, string email, string adresa);

        [OperationContract]
        ImprumutDTO getImprumutDTO(DateTime dataRes,DateTime dataImp,CarteDTO c,CititorDTO cit);

        [OperationContract]
        ReviewDTO getReviewDTO(string test);

        [OperationContract]
        bool adaugaCititor(CititorDTO cit);


        //aceasta metoda returneaza starea cititorului
        [OperationContract]
        bool verfStare(CititorDTO cit);

        //aceasta metoda verifica daca o carte este disponibila
        [OperationContract]
        bool verificaDisponibila(int carte);

        //aceasta metoda returneaza data cea mai apropiata de timp la care cartea va fi disponibila
        [OperationContract]
        DateTime searchDataScadenta(CarteDTO carte);

        [OperationContract]
        bool existaCarte(CarteDTO c1);

        [OperationContract]
        ICollection<CarteDTO> getAcelasiGen(GenDTO g);

        [OperationContract]
        bool imprumutaCarte(int c1, CititorDTO cit);

        [OperationContract]
        bool searchBook1(GenDTO g, String Titlu);
        [OperationContract]
        bool searchBook2(GenDTO g, AutorDTO a1);
        [OperationContract]
        bool searchBook3(GenDTO g, AutorDTO a1, String Titlu);

        [OperationContract]
        int getBookId1(GenDTO g, String Titlu);
        [OperationContract]
        int getBookId2(GenDTO g, AutorDTO a1);
        [OperationContract]
        int getBookId3(GenDTO g, AutorDTO a1, String Titlu);

        // a 3-a metoda
        [OperationContract]
        bool existaCititor(CititorDTO cit);

        [OperationContract]
        bool existaImprumut(ImprumutDTO imp);

        [OperationContract]
        bool restituieCartea(int impId, string rewiew);

        [OperationContract]
        CarteDTO searchForCArte(int id);

        [OperationContract]
        List<CititorDTO> ReaderBetweenDates(DateTime d1, DateTime d2);

        [OperationContract]
        List<CarteDTO> MostWantedBooks();

        [OperationContract]
        List<AutorDTO> getMostFaimousAutors();

        [OperationContract]
        List<GenDTO> getMostComuneGens();

        [OperationContract]
        List<ReviewDTO> getReviews(CarteDTO Carte);

        [OperationContract]
        List<CarteDTO> getAllBooks();

        [OperationContract]
        List<GenDTO> getAllGens();

        [OperationContract]
        List<AutorDTO> getAllAutors();

        [OperationContract]
        List<ImprumutDTO> getAllImprumutsNotAlive();

        [OperationContract]
        List<ReviewDTO> getAllReviews();

        [OperationContract]
        List<CititorDTO> getAllCititors();

        [OperationContract]
        List<ReviewDTO> getReviewByBookID(int id);

        [OperationContract]
        bool existReview(int id);
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfServiceBiblio.ContractType".
    
}
