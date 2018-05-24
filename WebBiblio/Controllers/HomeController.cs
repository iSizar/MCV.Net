using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebBiblio.Models;
using ServiceReference1;
using System.Globalization;

namespace WebBiblio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Statistica()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult RestituieCarte()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RestituieCarte(int? id)
        {
            if (id is null)
            {
                return RedirectToAction("Error");
            }

            string review = Request.Form["Text"];

            Task<bool> r_restituire = RestituieCarteAsync(id.Value, review);
            if (r_restituire.Result == true)
            {
                ViewData["Message"] = "Carte returnata cu succes.";
            }
            else
            {
                ViewData["Message"] = "Carte nu a fost returnata cu succes.";
            }
            return View();
        }

        public IActionResult ListCartiDeRestituit()
        {
            Task<List<ImprumutDTO>> l = getListImprumutsAsync();
            if(l is null)
            {
                return RedirectToAction("Error");
            }
            return View(l.Result);
        }

        private async Task<List<ImprumutDTO>> getListImprumutsAsync()
        {
            Service1Client s = new Service1Client();
            ImprumutDTO[] list = await s.getAllImprumutsNotAliveAsync();
            if (list is null)
            {
                return null;
            }
            List<ImprumutDTO> list1 = new List<ImprumutDTO>(list);
            
            return list1;
        }

        public IActionResult MostWantedBooks()
        {
            Task<List<CarteDTO>> list_carti = MostWantedBook();
            if(list_carti is null)
            {
                return null;
            }

            return View(list_carti.Result);
        }

        public IActionResult MostComuneGens()
        {
            Task<List<GenDTO>> list_carti = MostComuneGensAsync();
            if (list_carti is null)
            {
                return null;
            }

            return View(list_carti.Result);
        }

        public IActionResult BestAutors()
        {
            Task<List<AutorDTO>> list_carti = BestAutorsAsync();
            if (list_carti is null)
            {
                return null;
            }

            return View(list_carti.Result);
        }

        public IActionResult CitInInterval()
        {
            return View();
        }

        public IActionResult CautaReview()
        {
            Task<List<CarteDTO>> cartes = getAllBooks();
            if (cartes == null)
                return View();

            return View(cartes.Result);
        }

        public IActionResult ReviewCarte(int? id)
        {
            if(id.HasValue == false)
            {
                return RedirectToAction("Error");
            }

            Task<bool> revExistsTask = checkExistsReviews(id.Value);
            if (!revExistsTask.Result)
            {
                ViewData["Message"] = "Cartea nu are review-uri.";
                return View();
            }
            else
            {
                ViewData["Message"] = "Cartea are review-uri.\nAcestea sunt:";

                Task<List<ReviewDTO>> list_rev = getReviewsAsync(id.Value);


                return View(list_rev.Result);
            }

        }

        public IActionResult VerfDispCarte(int? id)
        {
            if(id is null)
            {
                return RedirectToAction("Error");
            }
            Task<bool> verfDispTask = VerfDispCarteAsync(id.Value);
            if (verfDispTask.Result)
            {
                return RedirectToAction("Imprumuta", new { id});
            }

           
            Task<CarteDTO> carteTask = getCarteAsync(id.Value);

            if (carteTask is null)
                return RedirectToAction("Error");

            Task<DateTime> dataScadentaTask = getDataScadenta(carteTask.Result);

            ViewData["Message"] = "Cartea va fi disponibila la data:"+ dataScadentaTask.Result.ToString();

            return View(carteTask.Result);
        }

        private async Task<DateTime> getDataScadenta(CarteDTO result)
        {
            Service1Client s = new Service1Client();
            DateTime date = await s.searchDataScadentaAsync(result);
           
            return date;
        }

        private async Task<CarteDTO> getCarteAsync(int value)
        {
            Service1Client s = new Service1Client();
            CarteDTO carte = await s.searchForCArteAsync(value);
            if (carte is null)
                return null;
            return carte;
        }

        private async Task<bool> VerfDispCarteAsync(int id)
        {
            Service1Client s = new Service1Client();
            bool ok = await s.verificaDisponibilaAsync(id);
            return ok;
        }

        [HttpPost]
        public IActionResult CitInInterval(DateTime d1, DateTime d2)
        {
            string timeString1 = Request.Form["DataImprumut"];
            string timeString2 = Request.Form["DataRestituire"];
            timeString1=timeString1.Replace("-", "/") ;
            timeString2=timeString2.Replace("-", "/") ;
            DateTime date1= DateTime.ParseExact(timeString1, "yyyy/MM/dd", CultureInfo.InvariantCulture);
            DateTime date2 = DateTime.ParseExact(timeString2, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            Task<List<CititorDTO>> list = getReadersBetweenDatesAsync(date1, date2);
            
            return View(list.Result);
        }


        private async Task<List<ReviewDTO>> getReviewsAsync(int idCarte)
        {
            Service1Client s = new Service1Client();

            ReviewDTO[] rev = await s.getReviewByBookIDAsync(idCarte);

            // ViewData["Message"] = "Cititorul este de buna credinta";
            List<ReviewDTO> l_rev = new List<ReviewDTO>(rev);
            return l_rev;
        }

        private async Task<bool> checkExistsReviews(int idCarte)
        {
            Service1Client s = new Service1Client();

            bool rev = await s.existReviewAsync(idCarte);
  
           // ViewData["Message"] = "Cititorul este de buna credinta";
            return rev;
        }

        private async Task<bool> RestituieCarteAsync(int id, string review)
        {
            Service1Client s = new Service1Client();
            bool ok = await s.restituieCarteaAsync(id, review);
            return ok;
        }


        private async Task<List<CititorDTO>> getReadersBetweenDatesAsync(DateTime date1, DateTime date2)
        {
            Service1Client s = new Service1Client();
            CititorDTO[] list = await s.ReaderBetweenDatesAsync(date1,date2);
            if (list is null)
            {
                return null;
            }
            List<CititorDTO> l1 = new List<CititorDTO>(list);
            return l1;
        }

        private async Task<List<AutorDTO>> BestAutorsAsync()
        {
            Service1Client s = new Service1Client();
            AutorDTO[] list = await s.getMostFaimousAutorsAsync();
            if (list is null)
            {
                return null;
            }
            List<AutorDTO> l1 = new List<AutorDTO>(list);
            return l1;
        }

        private async Task<List<GenDTO>> MostComuneGensAsync()
        {
            Service1Client s = new Service1Client();
            GenDTO[] list =await s.getMostComuneGensAsync();
            if (list is null)
            {
                return null;
            }
            List<GenDTO> l1 = new List<GenDTO>(list);
            return l1;
         }

        private async Task<List<CarteDTO>> getAllBooks()
        {
            Service1Client pc = new Service1Client();
            CarteDTO[] cartes = await pc.getAllBooksAsync();
            if (cartes == null)
                return null;
            List<CarteDTO> lcartes = new List<CarteDTO>(cartes);
            return lcartes;
        }

        private async Task<List<AutorDTO>> getAllAutors()
        {
            Service1Client pc = new Service1Client();
            AutorDTO[] autors = await pc.getAllAutorsAsync();
            if (autors == null)
                return null;
            List<AutorDTO> lautors = new List<AutorDTO>(autors);
            return lautors;
        }

        private async Task<List<CarteDTO>> MostWantedBook()
        {
            Service1Client pc = new Service1Client();
            CarteDTO[] cartes = await pc.MostWantedBooksAsync();
            if (cartes == null)
                return null;
            List<CarteDTO> lcartes = new List<CarteDTO>(cartes);
            return lcartes;
        }

        private async Task<GenDTO> getGenDTO(string s)
        {
            Service1Client pc = new Service1Client();
            GenDTO gen = await pc.getGENDTOAsync(s);
            if (gen is null)
                return null;
            return gen;
        }

        private async Task<AutorDTO> getAutorDTO(string numeA,string prenumeA)
        {
            Service1Client pc = new Service1Client();
            AutorDTO autor = await pc.getAutorDTOAsync(numeA,prenumeA);
            if (autor == null)
                return null;
            return autor;
        }
    
        private async Task<CarteDTO> getCarteDTOAsync(string titlu, AutorDTO autor , GenDTO gen)
        {
            Service1Client pc = new Service1Client();
            CarteDTO carte = await pc.getCarteDTOAsync(titlu, autor, gen);
            if (autor is null)
                return null;
            return carte;
        }

        private async Task<bool> achizitioneazaCarte(CarteDTO carte, int nrExemplare)
        {
            Service1Client pc = new Service1Client();
            bool a = await pc.achizitioneazaCarteAsync(carte, nrExemplare);
            return a;

        }

        private async Task<CititorDTO> getCititorDTOAsync(string nume, string prenume, string adresa, string email)
        {
            Service1Client pc = new Service1Client();
            CititorDTO cit = await pc.getCititorDTOAsync(nume, prenume, adresa,email);
            if (cit is null)
                return null;
            return cit;
        }

        private async Task<bool> adaugaCititorAsync(CititorDTO cititor)
        {
            Service1Client pc = new Service1Client();
            bool ok = await pc.adaugaCititorAsync(cititor);
            return ok;
        }

        private async Task<bool> verifStareCititorAsync(CititorDTO cititor)
        {
            Service1Client pc = new Service1Client();
            bool ok = await pc.verfStareAsync(cititor);
            return ok;
        }

        private async Task<List<CarteDTO>> getAcelasiGenAsync(GenDTO gen)
        {
            Service1Client pc = new Service1Client();
            CarteDTO[] array_books = await pc.getAcelasiGenAsync(gen);
            if (array_books is null)
            {
                return null;
            }
            List<CarteDTO> list_books = new List<CarteDTO>(array_books);
            return list_books;
        }

        private async Task<ImprumutDTO> getImprumutAsync(CarteDTO carte, CititorDTO cititor)
        {
            DateTime d1 = DateTime.Now;
            DateTime d2 = DateTime.Now;
            d2.AddDays(14);
            Service1Client s = new Service1Client();
            ImprumutDTO imp = await s.getImprumutDTOAsync(d1, d2, carte, cititor);
            if (imp is null)
            {
                return null;
            }
            return imp;
        }

        private async Task<bool> adaugaImprumutAsync(int carteId, CititorDTO cit)
        {
            Service1Client s = new Service1Client();
            bool ok = await s.imprumutaCarteAsync(carteId, cit);
            return ok;
        }

        public IActionResult CumparaCarte()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CumparaCarte(CarteDTO c)
        {
            Service1Client s = new Service1Client();
            string titlu = Request.Form["Titlu"];
            int nrExemplare = Convert.ToInt32(Request.Form["nrExemplare"]);
            
            string descriereGen = Request.Form["gen.Descriere"];
            string numeAutor = Request.Form["autor.Nume"];
            string prenumeAutor = Request.Form["autor.Prenume"];

            Task<GenDTO> g1 = getGenDTO(descriereGen);
            if (g1 is null)
                return null;


            Task<AutorDTO> a1 = getAutorDTO( numeAutor,prenumeAutor);
            if(a1 is null)
                return null;
  
                    
            Task<CarteDTO> c1 = getCarteDTOAsync(titlu, a1.Result , g1.Result);

            Task<bool>a = achizitioneazaCarte(c1.Result, nrExemplare);
            if (a.Result)
                return View();
            else
                return View();
        }
        

        public IActionResult ImprumutaCarte()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ImprumutaCarteSchGen(ImprumutDTO imp)
        {
            string gen = Request.Form["carte.gen.Descriere"];
            
            Task<GenDTO> g1 = getGenDTO(gen);

            Task<List<CarteDTO>> list = getAcelasiGenAsync(g1.Result);
            
            return View(list.Result);
        }

        public IActionResult Imprumuta()
        {
          
            return View();
        }

        [HttpPost]
        public IActionResult Imprumuta(int? id)
        {
            if(id is null)
                return RedirectToAction("Error");
            
            string numeCit = Request.Form["Nume"];
            string prenumeCit = Request.Form["Prenume"];
            string adresa = Request.Form["Adresa"];
            string email = Request.Form["Email"];

            Task<CititorDTO> cititor = getCititorDTOAsync(numeCit, prenumeCit, adresa, email);

            Task<bool> adaugaCititor = adaugaCititorAsync(cititor.Result);


            if (!adaugaCititor.Result)
            {
                ViewData["Message"] = "Cititorul este a fost adaugat in baza de date";
            }
            else
            {
                Task<bool> stare = verifStareCititorAsync(cititor.Result);

                if (stare.Result)
                {
                    ViewData["Message"] = "Cititorul este de buna credinta";
                }
                else
                {
                    ViewData["Message"] = "Cititorul nu este de buna credinta";
                }
            }
            //Task<ImprumutDTO> impTask = getImprumutAsync(carte, cititor.Result);
            Task<bool> stareImprumut = adaugaImprumutAsync(id.Value, cititor.Result);

            if (stareImprumut.Result)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Error");
            }

        }

        
    }
}
