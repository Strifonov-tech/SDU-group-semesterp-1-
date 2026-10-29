namespace ResQFlow;

public abstract class EmergencyUnit
{
    private bool isAvailable;
    private uint unitId;
    public string stationLocation { get; }
    public string plate { get; }
}