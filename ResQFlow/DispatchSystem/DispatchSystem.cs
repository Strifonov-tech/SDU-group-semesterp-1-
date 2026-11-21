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
        return incidents.ToArray(); // default value so the code compiles for now
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