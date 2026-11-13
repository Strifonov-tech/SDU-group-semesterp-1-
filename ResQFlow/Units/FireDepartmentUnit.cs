namespace ResQFlow;

public class FireDepartmentUnit:EmergencyUnit
{
    public FireDepartmentUnitType type { get; }

    public FireDepartmentUnit(
        uint unitId,
        string stationLocation,
        string plate,
        FireDepartmentUnitType type
    ): base(unitId, stationLocation, plate)
    {
        this.type = type;
    }
}