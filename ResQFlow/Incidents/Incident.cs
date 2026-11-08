namespace ResQFlow;

public class Incident
{
   public uint priority { get; private set; }
   public string location { get; private set; }
   public string description { get; private set; }
   public uint numOfPeople { get; private set; }
   public DateTime timestamp { get; }
   public DateTime timeOfOccurrence { get; private set; }
   public uint incidentId { get; }
   public IncidentStatus status { get; private set; }

   public Incident(uint priority, string location, string description, uint numOfPeople, DateTime timeOfOccurrence, uint incidentId, IncidentStatus status)
   {
      this.priority = priority;
      this.location = location;
      this.description = description;
      this.numOfPeople = numOfPeople;
      this.timeOfOccurrence = timeOfOccurrence;
      this.incidentId = incidentId;
      this.status = status;
   }
}