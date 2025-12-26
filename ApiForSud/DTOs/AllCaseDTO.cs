namespace ApiForSud.DTOs
{
    public class AllCaseDTO : CaseDTO
    {
        public Guid Id { get; set; }
        public string UserFIO { get; set; }
        public bool IsMarkeredByAdmin { get; set; } = false;

        public bool IsUnMarkeredByAdmin { get; set; } = false;

        public bool IsNotificated { get; set; } = false;
        public bool IsArhcived { get; set; } = false;

        public DateTime? ArchivedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
