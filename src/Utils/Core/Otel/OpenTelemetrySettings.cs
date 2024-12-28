namespace Core.Otel;

public class OpenTelemetrySettings
{
    public required Uri TracesEndpoint { get; init; }
    
    public List<TelemetryAttribute> Attributes { get; set; }

    public bool Enabled { get; set; }

    public List<KeyValuePair<string, object>> GetOtelAttributes()
    {
        return Attributes.Select(x => new KeyValuePair<string, object>(x.Key, x.Value)).ToList();
    }
    
    public class TelemetryAttribute
    {
        public string Key { get; set; }

        public object Value { get; set; }
    }
}