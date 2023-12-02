namespace TrainApp.Modals
{
    public class Request
    {
        public Tren Tren { get; set; }
        public int RezervasyonYapilacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}
