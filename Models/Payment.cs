namespace AstreeWebApp.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid PolicyId { get; set; }
        public float Montant { get; set; }
        public DateOnly DatePaiment { get; set; }
        public ModePayment ModeOfPayment { get; set; }
        public Statut PayStatut { get; set; }
        public Payment() { }
        public enum ModePayment
        {
            Carte,
            Virement,
            Cheque,
            Espece
        }
        public enum Statut
        {
            Paye,
            EnAttente,
            EnEchec
        }

    }
}
