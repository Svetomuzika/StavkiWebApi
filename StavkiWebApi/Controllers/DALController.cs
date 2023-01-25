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
        public bool CreateClient(Client a)
        {
            var c = a.ToString();
            a = JsonConvert.DeserializeObject<Client>(c);

            return unitOfWork.Clients.CreateAccount(a);


            //var a = new Client()
            //{
            //    Name = "Анастасия",
            //    Surname = "Челядникова",
            //    Patronymic = "Константиновна",
            //    PhoneNumber = "+7 (901) 453 45-15",
            //    Email = "nastya.chelyadnikova@mail.ru",
            //    Company = "ООО \"Ромашка\"",
            //    INN = "519211514",
            //    Login = "user1",
            //    Password = "user1",
            //};

            //var b = new Client()
            //{
            //    Name = "Никита",
            //    Surname = "Иванов",
            //    Patronymic = "Константинович",
            //    PhoneNumber = "+7 (912) 387 22-16",
            //    Email = "nikita.ivanov@mail.ru",
            //    Company = "ООО \"СпецТехАвто\"",
            //    INN = "726587514",
            //    Login = "user2",
            //    Password = "user2",
            //};

            //unitOfWork.Clients.CreateAccount(a);
            //unitOfWork.Clients.CreateAccount(b);
            //1, StatusEnum.Created, "Екатеринбург", "Бугульма", 20, 2, 20000, "15.09.22", "09.12.22"

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
        public void AddRequest(Request a)
        {
            //var a = new Request()
            //{
            //    RequestNumber = 2,
            //    Status = RequestStatusEnum.InProgress,
            //    DepartureCity = "Екатеринбург",
            //    ArrivalCity = "Пермь",
            //    ContainerSize = 40,
            //    CargoWeight = 25,
            //    Price = 41617,
            //    DepartureDate = "4.01.23",
            //    RequestCreateDate = "28.12.22",
            //    Comment = "Хрупкий груз!!!!",
            //    ClientId = 2
            //};


            unitOfWork.Requests.Add(a);
        }

        [HttpGet("Requests/GetAllRequests")]
        public IEnumerable<Request> GetAllRequests()
        {
            var a = unitOfWork.Requests.GetAll();

            return unitOfWork.Requests.GetAll();
        }

        [HttpPost("Requests/GetAllRequestsByClientId")]
        public IEnumerable<Request> GetAllRequestsByClientId(int clientId)
        {
            return unitOfWork.Requests.GetAll().Where(x => x.ClientId == clientId);
        }


        // Пример ссылки -- https://https://localhost:5001/Api/DAL/Requests/GetRequestSum?weight=28&city=Златоуст

        [HttpPost("Requests/GetRequestSum")]
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


        // пример ссылки -- https://localhost:5001/Api/DAL/Requests/ChangeStatus?id=0&status=0
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
    }
}