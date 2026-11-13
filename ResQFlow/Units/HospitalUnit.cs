namespace ResQFlow;

public class HospitalUnit:EmergencyUnit
{
    public HospitalUnitType type { get; }

    public HospitalUnit (
        uint unitId,
        string stationLocation,
        string plate,
        HospitalUnitType type        
    ):base(unitId, stationLocation, plate) {
        this.type = type;
    }
}