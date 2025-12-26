using ApiForSud.Models.DatabaseModels;

namespace ApiForSud.DTOs
{
    public class CaseWtihCurt
    {
        public Guid Id { get; set; }

        public string UserFIO { get; set; }
        public string NomerOfCase { get; set; }

        public string Notes { get; set; }
        public string ThirdParties { get; set; }

        public int CurtId { get; set; }

        public CurtDTO? Curt { get; set; }

        public string Applicant { get; set; }

        public string Defendant { get; set; }

        public string Reason { get; set; }

        public DateTime? DateOfCurt { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

        public string ResultOfCurt { get; set; }

        public DateTime? DateOfResult { get; set; }

        public Guid UserId { get; set; }

        public bool IsMarkeredByAdmin { get; set; } = false;

        public bool IsUnMarkeredByAdmin { get; set; } = false;

        public bool IsNotificated { get; set; } = false;
        public bool IsArhcived { get; set; } = false;

        public DateTime? ArchivedDate { get; set; }

        public List<ShortInstance> Instances { get; set; } = new();
    }
}
