using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Reflection.ObjectInflation.Models;

public class Frog
{
    [FroggyApiShiteName("NAME")]
    public string? KnownAs { get; set; }

    [FroggyApiShiteName("FMLY")]
    public string? Family { get; set; }

    [FroggyApiShiteName("GNUS")]
    public string? Genus { get; set; }

    [FroggyApiShiteName("SPCS")]
    public string? Species { get; set; }

    [FroggyApiShiteName("IUCN")]
    [JsonConverter(typeof(StringEnumConverter))]
    public IUCNStatusEnum IUCNStatus { get; set; }
}

public enum IUCNStatusEnum
{
    Extinct = 0,
    ExtinctInWild = 1,
    CriticallyEndangered = 2,
    Endangered = 3,
    Vulnerable = 4,
    NearThreatened = 5,
    LeastConcern = 6
}
