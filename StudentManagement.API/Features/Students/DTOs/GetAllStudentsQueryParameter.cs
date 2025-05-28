namespace StudentManagement.API.Features.Students.DTOs
{
    public class GetAllStudentsQueryParameter
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
    }
}
