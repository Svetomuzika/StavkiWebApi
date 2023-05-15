using Stavki.Data.Data.Enums;

namespace Stavki.Data.Data
{
    public class GeneralStavka
    {
        public int Id { get; set; }

        public string? City { get; set; }

        public int? Distance { get; set; }

        public CityType CityType { get; set; }

        public int? FirstValue { get; set; }

        public int? SecondValue { get; set; }

        public int? ThirdValue { get; set; }
    }
}
