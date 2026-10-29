namespace ResQFlow;

public class Incident
{
   private uint priority { get; set; }
   private string location { get; set; }
   private string description { get; set; }
   private uint numOfPeople { get; set; }
   public DateTime timestamp { get; }
   private DateTime timeOfOccurrence { get; set; }
   public uint incidentId { get; }
   private IncidentStatus status { get; set; }
}