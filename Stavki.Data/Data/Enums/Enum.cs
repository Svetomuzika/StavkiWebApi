namespace Stavki.Data.Data.Enums
{
    public enum RequestStatus
    {
        Created = 0,
        InProgress = 1,
        Done = 2,
        Canceled = 3,
        Rejected = 4
    }

    public enum DataSourceType
    {
        Employee = 0,
        Client = 1,
    }
    public enum CityType
    {
        NearInCity = 0,
        InCity = 1,
        NearInCityNDS = 3,
        InCityNDS = 4
    }
}
