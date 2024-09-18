namespace api_finance.DTO
{
    public class NotesDto
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public int UserCreated { get; set; }

        public int UserUpdated { get; set; }
    }
}