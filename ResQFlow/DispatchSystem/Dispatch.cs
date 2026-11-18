using System.ComponentModel.DataAnnotations;

namespace ResQFlow;

public class Dispatch
{
    public readonly Incident incident;
    private List<EmergencyUnit> assignedUnits { get; } = new();
    public readonly DateTime timestamp;
    public readonly UInt32 id;


   public Dispatch(Incident incident)
    {
        this.incident = incident;
    }
}