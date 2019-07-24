namespace WeatherAppApi.Models
{
    public class WeatherstoreDatabaseSettings : IWeatherstoreDatabaseSettings
    {
        public string WeatherCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        
    }

    public interface IWeatherstoreDatabaseSettings
    {
        string WeatherCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        
    }
}