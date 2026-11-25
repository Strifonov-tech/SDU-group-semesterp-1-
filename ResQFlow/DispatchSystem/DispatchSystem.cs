namespace ResQFlow;

public class DispatchSystem
{
    private List<Incident> incidents { get; } = new();
    private List<Dispatch> dispatches { get; } = new();
    private List<EmergencyUnit> units = new();

    public DispatchSystem()
    {
        this.units = [
        // POLICE UNITS (6)
        new PoliceUnit(1, "Odense Police Station", "AB12345", PoliceUnitType.PATROL_CAR),
        new PoliceUnit(2, "Odense Police Station", "AB12346", PoliceUnitType.PATROL_CAR),
        new PoliceUnit(3, "Odense Police Station", "AB12347", PoliceUnitType.PATROL_CAR),
        new PoliceUnit(4, "Odense Police Station", "AB12348", PoliceUnitType.PATROL_CAR),
        new PoliceUnit(5, "Odense Police Station", "AB12349", PoliceUnitType.CANINE),
        new PoliceUnit(6, "Odense Police Station", "AB12350", PoliceUnitType.CANINE),

        // FIRE DEPARTMENT UNITS (6)
        new FireDepartmentUnit(7, "Odense Fire Station", "CD12345", FireDepartmentUnitType.FIRE_ENGINE),
        new FireDepartmentUnit(8, "Odense Fire Station", "CD12346", FireDepartmentUnitType.FIRE_ENGINE),
        new FireDepartmentUnit(9, "Odense Fire Station", "CD12347", FireDepartmentUnitType.FIRE_TRUCK),
        new FireDepartmentUnit(10, "Odense Fire Station", "CD12348", FireDepartmentUnitType.FIRE_TRUCK),
        new FireDepartmentUnit(11, "Odense Fire Station", "CD12349", FireDepartmentUnitType.FIRE_ENGINE),
        new FireDepartmentUnit(12, "Odense Fire Station", "CD12350", FireDepartmentUnitType.FIRE_ENGINE),

        // HOSPITAL / AMBULANCE UNITS (8)
        new HospitalUnit(13, "Odense University Hospital", "EF12345", HospitalUnitType.AMBULANCE),
        new HospitalUnit(14, "Odense University Hospital", "EF12346", HospitalUnitType.AMBULANCE),
        new HospitalUnit(15, "Odense University Hospital", "EF12347", HospitalUnitType.AMBULANCE),
        new HospitalUnit(16, "Odense University Hospital", "EF12348", HospitalUnitType.AMBULANCE),
        new HospitalUnit(17, "Odense University Hospital", "EF12349", HospitalUnitType.AMBULANCE),
        new HospitalUnit(18, "Odense University Hospital", "EF12350", HospitalUnitType.DOCTOR_CAR),
        new HospitalUnit(19, "Odense University Hospital", "EF12351", HospitalUnitType.DOCTOR_CAR),
        new HospitalUnit(20, "Odense University Hospital", "EF12352", HospitalUnitType.DOCTOR_CAR)
        ];
    }

    public EmergencyUnit[] GetAvailableUnits()
    {
        return units
            .Where(unit => unit.isAvailable)
            .ToArray();
    }

    public EmergencyUnit[] GetAllUnits()
    {
        return units.ToArray();
    }

    public Incident[] GetActiveIncidents()
    {
        // TODO replace this default value with the actual value
        Incident[] testingData = [
            new Incident(1, "help", "aa", 2, DateTime.Now, 1, IncidentStatus.DISPATCHED),
            new Incident(2, "help", "aa", 2, DateTime.Now, 2, IncidentStatus.REPORTED),
            new Incident(3, "help", "aa", 2, DateTime.Now, 3, IncidentStatus.DISPATCHED),
            new Incident(4, "help", "aa", 2, DateTime.Now, 4, IncidentStatus.REPORTED),
            new Incident(5, "help", "aa", 2, DateTime.Now, 5, IncidentStatus.DISPATCHED),
            new Incident(6, "help", "aa", 2, DateTime.Now, 6, IncidentStatus.DISPATCHED),
            new Incident(7, "help", "aa", 2, DateTime.Now, 7, IncidentStatus.DISPATCHED),
            new Incident(8, "help", "aa", 2, DateTime.Now, 8, IncidentStatus.REPORTED),
            new Incident(9, "help", "aa", 2, DateTime.Now, 9, IncidentStatus.REPORTED),
        ];
        return testingData; // default value so the code compiles for now
    }

    public Incident[] GetFinishedIncidents()
    {
        // TODO replace this default value with the actual value
        Incident[] testingData = [
            new Incident(1, "help", "aa", 2, DateTime.Now, 1, IncidentStatus.FINISHED),
            new Incident(2, "help", "aa", 2, DateTime.Now, 2, IncidentStatus.FINISHED),
            new Incident(3, "help", "aa", 2, DateTime.Now, 3, IncidentStatus.FINISHED),
            new Incident(4, "help", "aa", 2, DateTime.Now, 4, IncidentStatus.FINISHED),
            new Incident(5, "help", "aa", 2, DateTime.Now, 5, IncidentStatus.FINISHED),
            new Incident(6, "help", "aa", 2, DateTime.Now, 6, IncidentStatus.FINISHED),
            new Incident(7, "help", "aa", 2, DateTime.Now, 7, IncidentStatus.FINISHED),
            new Incident(8, "help", "aa", 2, DateTime.Now, 8, IncidentStatus.FINISHED),
            new Incident(9, "help", "aa", 2, DateTime.Now, 9, IncidentStatus.FINISHED),
        ];
        return testingData; // default value so the code compiles for now
    }

    public void CreateDispatch(Incident incident, List<EmergencyUnit> emergencyUnits)
    {
        this.dispatches.Add(new Dispatch(incident, emergencyUnits));
    }

    public Dispatch[] GetDispatches()
    {
        // TODO replace this default value with the actual value
        return dispatches.ToArray(); // default value so the code compiles for now
    }

    // TODO get dispatch based on incident id function
    public Dispatch? GetDispatchById(uint id) // possibly change to guid if incident also gets guid assigned
    {
       return dispatches.Find(dispatch => dispatch.incident.incidentId == id);
    }
}