namespace ExampleIdentity.Core.Dto
{
    public class StudenModelDto
    {
        public int? Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Subjects { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string? Marks { get; set; }
        public DateTime? Datecreated { get; set; }
        public List<StudenModelDto>? StudentList { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public int? TotalRecords { get; set; }

    }
}
