namespace StudentManagement.API.Features.Classes.DTOs
{
    public class GetAllClassesResponse
    {
        public List<ClassResponse> Classes { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
