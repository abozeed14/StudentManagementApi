namespace StudentManagement.API.Features.Students.DTOs
{
    public class GetAllStudentsResponse
    {
        public List<StudentResponse> Students { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
