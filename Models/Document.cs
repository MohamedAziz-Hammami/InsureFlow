namespace AstreeWebApp.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public Guid PolicyId { get; set; }
        public string Nom { get; set; }
        public string Type { get; set; }
        public string Lien { get; set; }
        public string Tag { get; set; }
        public Document() { }
    }
}
