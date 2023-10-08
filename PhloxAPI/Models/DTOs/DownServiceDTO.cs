namespace PhloxAPI.Models.DTOs
{
    public class DownServiceDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public char Building { get; set; }
        public int Floor { get; set; }
        public int ReportCount { get; set; }
        public bool IsOutOfService { get; set; } = false;
    }
}
