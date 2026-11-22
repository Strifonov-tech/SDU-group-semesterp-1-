namespace ResQFlow;

public class DispatchSystem
{
    private List<Incident> incidents { get; } = new();
    private List<Dispatch> dispatches { get; } = new();

    public Incident[] GetActiveIncidents()
    {
        // TODO replace this default value with the actual value
        return incidents.ToArray(); // default value so the code compiles for now
    }

    public Incident[] GetFinishedIncidents()
    {
        // TODO replace this default value with the actual value
        Incident[] testingData = [
            new Incident(1, "help", "aa", 2, DateTime.Now, 1, IncidentStatus.REPORTED),
            new Incident(2, "help", "aa", 2, DateTime.Now, 2, IncidentStatus.REPORTED),
            new Incident(3, "help", "aa", 2, DateTime.Now, 3, IncidentStatus.REPORTED),
            new Incident(4, "help", "aa", 2, DateTime.Now, 4, IncidentStatus.REPORTED),
            new Incident(5, "help", "aa", 2, DateTime.Now, 5, IncidentStatus.REPORTED),
            new Incident(6, "help", "aa", 2, DateTime.Now, 6, IncidentStatus.REPORTED),
            new Incident(7, "help", "aa", 2, DateTime.Now, 7, IncidentStatus.REPORTED),
            new Incident(8, "help", "aa", 2, DateTime.Now, 8, IncidentStatus.REPORTED),
            new Incident(9, "help", "aa", 2, DateTime.Now, 9, IncidentStatus.REPORTED),
        ];
        return testingData; // default value so the code compiles for now
    }

    public void CreateDispatch(Incident incidents, List<EmergencyUnit> emergencyUnits)
    {

    }

    public Dispatch[] GetDispatches()
    {
        // TODO replace this default value with the actual value
        return dispatches.ToArray(); // default value so the code compiles for now
    }
}