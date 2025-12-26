namespace ApiForSud.Models.DatabaseModels
{
    public class CurtInstance
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string NameOfCurt { get; set; }

        public DateTime? DateOfSession { get; set; }

        public string Link { get; set; }

        public string Employee { get; set; }
        public string ResultOfIstance { get; set; }

        public string Report { get; set; }

        public DateTime? DateOfResult { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public Guid CaseId { get; set; }

        public virtual Case Case { get; set; } = null!;
    }
}


