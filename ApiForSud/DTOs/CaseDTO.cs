namespace ApiForSud.DTOs
{
    public class CaseDTO
    {
        public Guid Id { get; set; }
        public string NomerOfCase { get; set; }
        public string Notes { get; set; }
        public string Third { get; set; }
        public string NameOfCurt { get; set; }

        public string Applicant { get; set; }

        public string Defendant { get; set; }

        public string Reason { get; set; }

        public DateTime? DateOfCurt { get; set; }

        public string ResultOfCurt { get; set; }

        public DateTime? DateOfResult { get; set; }

        public bool IsMarkeredByAdmin { get; set; }

        public bool IsUnMarkeredByAdmin { get; set; }



    }
}
