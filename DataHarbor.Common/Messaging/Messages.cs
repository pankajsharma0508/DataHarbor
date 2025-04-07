namespace DataHarbor.Common.Messaging
{
    public record Anchored(Guid DeclarationId);
  
    public record Docked(Guid DeclarationId);
    
    public record Adrifted(Guid DeclarationId);

    public record Dispatched(Guid DeclarationId);
}
