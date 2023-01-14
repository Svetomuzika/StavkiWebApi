using Microsoft.AspNetCore.Mvc;
using StavkiWebApi.Models.Entites;
using StavkiWebApi.Models.Interfaces;

namespace StavkiWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DALController : ControllerBase
    {
        IUnitOfWork unitOfWork;

        public DALController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("Auth/Client")] //login = "login/password"
        public Client AuthClient(string data)
        {
            var client = unitOfWork.Clients.Auth(data);

            return client;
        }

        [HttpPost("Auth/Client/Create")]
        public bool CreateClient(Client client)
        {
            return unitOfWork.Clients.CreateAccount(client);
        }
    }
}