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

        [HttpGet("Stavki/GorodSNDS")]
        public IEnumerable<Gorod> GetStavkiGorodSNDS()
        {
            return unitOfWork.Gorod.GetAll();
        }

        [HttpGet("Stavki/Gorod")]
        public IEnumerable<Gorod> GetStavkiGorod()
        {
            var a = unitOfWork.Gorod.GetAll().ToList();

            foreach (var e in a)
            {
                e.Ft20 = (int)(e.Ft20 * 0.8);
                e.Ft40 = (int)(e.Ft40 * 0.8);
                e.Ot24Do30Tn = (int)(e.Ot24Do30Tn * 0.8);
            }

            return a;
        }

        [HttpGet("Stavki/BlizMezhGorodSNDS")]
        public IEnumerable<BlizMezhGorodSNDS> GetStavkiBlizMezhGorodSNDS()
        {
            return unitOfWork.BlizMezhGorodSNDS.GetAll();
        }

        [HttpGet("Stavki/BlizMezhGorod")]
        public IEnumerable<BlizMezhGorodSNDS> GetStavkiBlizMezhGorod()
        {
            var a = unitOfWork.BlizMezhGorodSNDS.GetAll().ToList();

            foreach (var e in a)
            {
                e.Ft20 = (int)(e.Ft20 * 0.8);
                e.Ft40 = (int)(e.Ft40 * 0.8);
                e.Ot24Do30Tn = (int)(e.Ot24Do30Tn * 0.8);
            }

            return a;
        }

        [HttpGet("Stavki/MezhgorodSNDS")]
        public IEnumerable<MezhgorodSNDS> GetStavkiMezhgorodSNDS()
        {
            return unitOfWork.MezhgorodSNDS.GetAll();
        }

        [HttpGet("Stavki/Mezhgorod")]
        public IEnumerable<MezhgorodSNDS> GetStavkiMezhgorod()
        {
            var a = unitOfWork.MezhgorodSNDS.GetAll().ToList();

            foreach (var e in a)
            {
                e.Ot27 = e.Ot27 is null ? 0 : (int)(e.Ot27 * 0.8);
                e.Do24 = e.Do24 is null ? 0 : (int)(e.Ot27 * 0.8);
                e.Ot24Do27 = e.Ot24Do27 is null ? 0 : (int)(e.Ot27 * 0.8);
            }

            return a;
        }

        [HttpGet("Stavki/GetAllPuncts")]
        public IEnumerable<string> GetAllPuncts()
        {
            var a = new List<string>() { "Екатеринбург" };

            var a1 = unitOfWork.BlizMezhGorodSNDS.GetAll().Select(x => x.City).ToList();
            var a2 = unitOfWork.MezhgorodSNDS.GetAll().Select(x => x.City).ToList();

            a.AddRange(a1);
            a.AddRange(a2);

            return a;
        }
    }
}