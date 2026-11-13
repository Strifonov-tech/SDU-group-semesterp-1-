namespace ResQFlow;

public abstract class EmergencyUnit
{
    public bool isAvailable { get; set; }
    public uint unitId { get; private set; }
    public string stationLocation { get; private set;}
    public string plate { get; private set;}

    protected EmergencyUnit(uint unitId, string stationLocation, string plate)
    {
        this.isAvailable = true;
        this.unitId = unitId;
        this.stationLocation = stationLocation;
        this.plate = plate;
    }
}