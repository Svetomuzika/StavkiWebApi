using Microsoft.AspNetCore.Mvc;
using StavkiWebApi.Models.Entites;
using StavkiWebApi.Models.Interfaces;
using StavkiWebApi.Data;
using Newtonsoft.Json;

namespace StavkiWebApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class DALController : ControllerBase
    {
        IUnitOfWork unitOfWork;

        public DALController(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        [HttpGet("Auth/Client")] //login = "login/password"
        public Client AuthClient(string data) => unitOfWork.Clients.Auth(data);

        [HttpGet("Auth/Client/Create")]
        public bool CreateClient(string a) => unitOfWork.Clients.CreateAccount(JsonConvert.DeserializeObject<Client>(a));

        [HttpGet("Stavki/GorodSNDS")]
        public IEnumerable<Gorod> GetStavkiGorodSNDS() => unitOfWork.Gorod.GetAll();

        [HttpGet("Stavki/Gorod")]
        public IEnumerable<Gorod> GetStavkiGorod()
        {
            var allCities = unitOfWork.Gorod.GetAll().ToList();

            foreach (var city in allCities)
            {
                city.Ft20 = (int)(city.Ft20 * 0.8);
                city.Ft40 = (int)(city.Ft40 * 0.8);
                city.Ot24Do30Tn = (int)(city.Ot24Do30Tn * 0.8);
            }

            return allCities;
        }

        [HttpGet("Stavki/BlizMezhGorodSNDS")]
        public IEnumerable<BlizMezhGorodSNDS> GetStavkiBlizMezhGorodSNDS() => unitOfWork.BlizMezhGorodSNDS.GetAll().ToList();

        [HttpGet("Stavki/BlizMezhGorod")]
        public IEnumerable<BlizMezhGorodSNDS> GetStavkiBlizMezhGorod()
        {
            var allBliz = unitOfWork.BlizMezhGorodSNDS.GetAll().ToList();

            foreach (var city in allBliz)
            {
                city.Ft20 = (int)(city.Ft20 * 0.8);
                city.Ft40 = (int)(city.Ft40 * 0.8);
                city.Ot24Do30Tn = (int)(city.Ot24Do30Tn * 0.8);
            }

            return allBliz;
        }

        [HttpGet("Stavki/MezhgorodSNDS")]
        public IEnumerable<MezhgorodSNDS> GetStavkiMezhgorodSNDS() => unitOfWork.MezhgorodSNDS.GetAll().ToList();

        [HttpGet("Stavki/Mezhgorod")]
        public IEnumerable<MezhgorodSNDS> GetStavkiMezhgorod()
        {
            var allMezh = unitOfWork.MezhgorodSNDS.GetAll().ToList();

            foreach (var city in allMezh)
            {
                city.Ot27 = city.Ot27 is null ? 0 : (int)(city.Ot27 * 0.8);
                city.Do24 = city.Do24 is null ? 0 : (int)(city.Ot27 * 0.8);
                city.Ot24Do27 = city.Ot24Do27 is null ? 0 : (int)(city.Ot27 * 0.8);
            }

            return allMezh;
        }

        [HttpGet("Stavki/GetAllPuncts")]
        public IEnumerable<string> GetAllPuncts()
        {
            var allPuncts = new List<string>();

            var allBliz = unitOfWork.BlizMezhGorodSNDS.GetAll().Select(x => x.City);
            var allMezh = unitOfWork.MezhgorodSNDS.GetAll().Select(x => x.City);

            allPuncts.AddRange(allBliz);
            allPuncts.AddRange(allMezh);

            return allPuncts;
        }

        [HttpPost("Requests/AddRequest")]
        public void AddRequest(Request a) => unitOfWork.Requests.Add(a);

        [HttpGet("Requests/GetAllRequests")]
        public IEnumerable<Request> GetAllRequests() => unitOfWork.Requests.GetAll();

        [HttpPost("Requests/GetAllRequestsByClientId")]
        public IEnumerable<Request> GetAllRequestsByClientId(int clientId) => unitOfWork.Requests.GetAll().Where(x => x.ClientId == clientId);

        [HttpGet("Requests/GetRequestSum")]
        public float GetRequestSum(int weight, string city)
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

        //Значения status 
        // 0 - Created
        // 1 - InProgress
        // 2 - Done
        [HttpPut("Requests/ChangeStatus")]
        public bool ChangeStatus(int id, RequestStatusEnum status)
        {
            var request = unitOfWork.Requests.GetAll().Where(x => x.Id == id).SingleOrDefault();

            if (request == null)
                return false;

            request.Status = status;

            unitOfWork.Requests.Update(request);

            return true;
        }

        [HttpGet("Clients/GetAll")]
        public IEnumerable<Client> GetAllClients() => unitOfWork.Clients.GetAll();

        [HttpGet("Clients/GetClientById")]
        public Client GetClientById(int id) => unitOfWork.Clients.GetById(id);
    }
}