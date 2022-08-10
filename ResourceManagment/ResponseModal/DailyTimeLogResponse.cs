using System.ComponentModel.DataAnnotations.Schema;

namespace ResourceManagment.ResponseModal
{
    public class DailyTimeLogResponse
    {
        public int? AssignId { get; set; }
        public int? LogId { get; set; }
        public string? EmpName { get; set; }
        public string? ProjectName { get; set; }
        public string? Coordinator { get; set; }
        public string? Manager { get; set; }
        public int? status { get; set; }
        public int Bilable { get; set; }
        public string? createddate { get; set; }
        public string? Avalibiltty { get; set; }
        public double? BillingHour { get; set; }
        public string? Comments { get; set; }
        [NotMapped]
        public string? Srtdate { get; set; }
        [NotMapped]
        public string? enddate { get; set; }
    }
}
