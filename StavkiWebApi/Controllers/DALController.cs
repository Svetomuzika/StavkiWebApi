using Microsoft.AspNetCore.Mvc;
using StavkiWebApi.Models.Entites;
using StavkiWebApi.Models.Interfaces;

namespace StavkiWebApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
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
            var a = new List<string>();

            var a1 = unitOfWork.BlizMezhGorodSNDS.GetAll().Select(x => x.City);
            var a2 = unitOfWork.MezhgorodSNDS.GetAll().Select(x => x.City);

            a.AddRange(a1);
            a.AddRange(a2);

            return a;
        }

        [HttpPost("Requests/AddRequest")]
        public void AddRequest(Request requset)
        {
            unitOfWork.Requests.Add(requset);
        }

        [HttpGet("Requests/GetAllRequests")]
        public IEnumerable<Request> GetAllRequests()
        {
            return unitOfWork.Requests.GetAll();
        }

        [HttpGet("Requests/GetAllRequestsByClientId")]
        public IEnumerable<Request> GetAllRequestsByClientId(int clientId)
        {
            return unitOfWork.Requests.GetAll().Where(x => x.ClientId == clientId);
        }


        // ������ ������ -- https://https://localhost:44360/Api/DAL/Requests/GetRequestSum?weight=28&city=��������

        [HttpGet("Requests/GetRequestSum")]
        public float GetRequestSum(float weight, string city)
        {
            var gorod = unitOfWork.Gorod.GetAll();
            var bliz = unitOfWork.BlizMezhGorodSNDS.GetAll();
            var mezh = unitOfWork.MezhgorodSNDS.GetAll();
            float? result = 0;

            if (bliz.Select(x => x.City).Contains(city))
            {
                if (weight < 24)
                    result = bliz.Where(x => x.City == city).Single().Ft20;

                if (weight >= 24 && weight <= 27)
                    result = bliz.Where(x => x.City == city).Single().Ft40;

                if (weight > 27)
                    result = bliz.Where(x => x.City == city).Single().Ot24Do30Tn;
            }

            if (mezh.Select(x => x.City).Contains(city))
            {
                if (weight < 24)
                    result = mezh.Where(x => x.City == city).Single().Do24;

                if (weight >= 24 && weight <= 27)
                    result = mezh.Where(x => x.City == city).Single().Ot24Do27;

                if (weight > 27)
                    result = mezh.Where(x => x.City == city).Single().Ot27;
            }

            return result ??= 0;
        }
    }
}