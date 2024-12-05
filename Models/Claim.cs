namespace AstreeWebApp.Models
{
    public class Claim
    {
        public Guid Id { get; set; }
        public Guid PolicyId { get; set; }
        public DateOnly DateSinistre { get; set; }
        public string TypeSinistre { get; set; }
        public string Description { get; set; }
        public float Montant { get; set; }
        public Statut ClaimStatut { get; set; }
        public enum Statut
        {
            Ouvert,
            EnCours,
            Réglé,
            Refusé
        }
        public Claim() { }
    }
}

