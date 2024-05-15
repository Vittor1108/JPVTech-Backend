namespace JpvTech.Application.DTOs.PersonDTOs
{
    public class PersonRequestDTO
    {
        public string? Name { get; set; }
        public string? CPF { get; set; }
        public decimal IncomeValue { get; set; }    
        public DateTime DateBirth { get; set; }
    }
}
