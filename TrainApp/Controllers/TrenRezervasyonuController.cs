using Microsoft.AspNetCore.Mvc;
using TrainApp.Modals;

namespace TrainApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrenRezervasyonuController : ControllerBase
    {
        [HttpPost(Name = "TrenRezervasyonuPost")]
        public Response Post(Request request)
        {
            int rezervasyonYapilacakKisiSayisi = request.RezervasyonYapilacakKisiSayisi;

            Response response = new Response();
            YerlesimAyrinti yerlesimAyrinti = new YerlesimAyrinti();
            List<YerlesimAyrinti> yerlesimAyrintiList = new List<YerlesimAyrinti>();

            foreach (Vagon vagon in request.Tren.Vagonlar)
            {
                if (!request.KisilerFarkliVagonlaraYerlestirilebilir)
                {
                    if ((Convert.ToDouble(vagon.DoluKoltukAdet) + Convert.ToDouble(request.RezervasyonYapilacakKisiSayisi)) / Convert.ToDouble(vagon.Kapasite) * 100 <= 70)
                    {
                        yerlesimAyrinti.KisiSayisi = request.RezervasyonYapilacakKisiSayisi;
                        yerlesimAyrinti.VagonAdi = vagon.Ad;

                        response.RezervasyonYapilabilir = true;
                        response.YerlesimAyrinti.Add(yerlesimAyrinti);
                        return response;
                    }
                }

                else if (request.KisilerFarkliVagonlaraYerlestirilebilir)
                {
                    int yerlestirilenYolcuSayisi = 0;

                    while (((Convert.ToDouble(vagon.DoluKoltukAdet) + yerlestirilenYolcuSayisi+1) / Convert.ToDouble(vagon.Kapasite) * 100 <= 70) 
                        && (rezervasyonYapilacakKisiSayisi > yerlestirilenYolcuSayisi) 
                        && (rezervasyonYapilacakKisiSayisi > 0))
                    {
                        yerlestirilenYolcuSayisi = yerlestirilenYolcuSayisi + 1;
                    }

                    if(yerlestirilenYolcuSayisi > 0)
                    {
                        yerlesimAyrinti = new YerlesimAyrinti();
                        yerlesimAyrinti.KisiSayisi = yerlestirilenYolcuSayisi;
                        yerlesimAyrinti.VagonAdi = vagon.Ad;
                        yerlesimAyrintiList.Add(yerlesimAyrinti);
                        rezervasyonYapilacakKisiSayisi -= yerlestirilenYolcuSayisi;
                    }
                }
            }
            if (yerlesimAyrintiList.Count > 0 && rezervasyonYapilacakKisiSayisi == 0)
            {
                response.YerlesimAyrinti = yerlesimAyrintiList;
                response.RezervasyonYapilabilir = true;
                return response;
            }
            response.RezervasyonYapilabilir = false;
            return response;
        }
    }
}
