using ApplicationCore.ValueObjects;

namespace ApplicationCore.TransferObjects;

public class ProcessedScoreResponse
{
    public ProcessedScore? Data { get; set; }
}

public class ProcessedScore
{
    public RawScore? Raw { get; set; }
    public ScaledScore? Scaled { get; set; }
    public RankedCulture Ranked { get; set; }
}