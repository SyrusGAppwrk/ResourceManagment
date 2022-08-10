namespace ResourceManagment.ResponseModal
{
    public class AssignTaskResponse
    {
        public int id { get; set; }
        public string? EmpName { get; set; }
        public string? ProjectName { get; set; }
        public string? Coordinator { get; set; }
        public string? Manger { get; set; }
        public string? CreatedDate { get; set; }
        public int? status { get; set; }
        public int? Billable { get; set; }
    }
}
