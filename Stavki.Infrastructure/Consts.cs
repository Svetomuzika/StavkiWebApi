namespace Stavki.Infrastructure
{
    public class Consts
    {
        public class ApiFns
        {
            public const string API_FNS_KEY = "c96daa40a0717105e4fcde3581abd5823d5";

            public const string API_FNS_URL = "https://api-fns.ru/api/egr?req={0}&key={1}";
        }

        public class ApiPec
        {
            public const string API_PEC_CITIES_URL = "http://www.pecom.ru/ru/calc/towns.php";

            public const string API_PEC_PRICE_URL = "http://calc.pecom.ru/bitrix/components/pecom/calc/ajax.php?places[0][{0},{1},{2},{3},{4},0,1]&take[town]=-473&deliver[town]={5}";
        }
    }
}
