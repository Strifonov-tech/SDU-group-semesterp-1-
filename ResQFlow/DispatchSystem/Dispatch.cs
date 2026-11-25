namespace ResQFlow;

public class Dispatch
{
    public readonly Incident incident;
    public List<EmergencyUnit> assignedUnits { get; } = new();
    public DateTime timestamp { get; }
    public Guid id { get; } = Guid.NewGuid(); //The function Guid id = Guid.NewGuid() generates automaticly an unique Id for the index "id", everytime a new object is created;

    public Dispatch(Incident incident, List<EmergencyUnit> assignedUnits)
    {
        
        this.incident = incident;
        this.assignedUnits = assignedUnits;
        incident.IncidentStatus(IncidentStatus.DISPATCHED);
    }

    public void assignUnit(EmergencyUnit unit)
    {
        if (unit == null || !unit.isAvailable)
        {
            throw new ArgumentException("Unit not available or field not filled");
        } //In case the "unit" variable is either a null or it isn't available;
        else 
        {
            assignedUnits.Add(unit);
            unit.isAvailable = false;
        } //If the periferals are available and the unit is not a null, then said unit is getting dispatched and its availability is set to false(cant be sent anymore till it gets back);
    }

    public void ComeBackUnit (EmergencyUnit unit)
    {
        if (unit == null || unit.isAvailable == true)
        {
            throw new ArgumentException("Unit already available or field not filled");
        } //If the "unit" variable is a null or is already available;
        else 
        {
            assignedUnits.Remove(unit);
            unit.isAvailable = true;
        } //Basiclly the unit is called back, it can be because its be wrongfully sent or some other reason;
    }



}