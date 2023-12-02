namespace TrainApp.Modals
{
    public class Response
    {
        public Response()
        {
            YerlesimAyrinti = new List<YerlesimAyrinti>();
        }
        public bool RezervasyonYapilabilir { get; set; }
        public List<YerlesimAyrinti> YerlesimAyrinti { get; set; }
    }
}
