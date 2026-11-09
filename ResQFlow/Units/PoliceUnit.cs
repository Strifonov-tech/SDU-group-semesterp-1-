namespace ResQFlow;

public class PoliceUnit:EmergencyUnit
{
    public PoliceUnitType type { get; }

    public PoliceUnit(
        uint unitId,
        string stationLocation,
        string plate,
        PoliceUnitType type
        ):base(unitId, stationLocation,plate)
    {
        this.type = type;
    }
}