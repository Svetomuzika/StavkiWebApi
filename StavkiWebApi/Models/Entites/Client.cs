using System.ComponentModel.DataAnnotations.Schema;

namespace StavkiWebApi.Models.Entites
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Company { get; set; }

        public string INN { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<Request> Requests { get; set; } = new List<Request>();
    }
}