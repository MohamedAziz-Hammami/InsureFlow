namespace AstreeWebApp.Models
{
    public class Policy
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public TypePolice Type { get; set; }
        public DateTime DateDeb { get; set; }
        public DateTime DateFin { get; set; }
        public int Prime { get; set; }
        public Statut PolicStatut { get; set; }
        public enum TypePolice
        {
            Bien,
            Argent,
            Famille
        }
        public enum Statut
        {
            Actif,
            Annule,
            Expire
        }
        public Policy()
        {

        }
    }
}
