namespace ApiForSud.Models.DatabaseModels
{
    public class Curt
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Case>? Cases { get; set; }
    }
}
