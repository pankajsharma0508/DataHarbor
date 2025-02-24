namespace DataHarbor.Common.Messaging
{
    public record Anchored(Guid UniqueId, string Name, string FilePath, DateTime RecievedOn);
  
    public record Docked(Guid UniqueId, string Name, string FilePath, DateTime RecievedOn);
    
    public record Adrifted(Guid UniqueId, string Name, string FilePath, DateTime RecievedOn);

    public record Dispatched(Guid UniqueId, string Name, string FilePath, DateTime RecievedOn);
}
