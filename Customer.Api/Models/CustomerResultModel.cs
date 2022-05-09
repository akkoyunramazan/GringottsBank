namespace Customer.Api.Models
{
    public class CustomerResultModel
    {
        public int? CustomerNumber { get; set; }
        public bool IsSuccessful { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
    }
}