using ApplicationCore.ValueObjects;

namespace ApplicationCore.TransferObjects;

public class EvaluateScoreRequest
{
    public Score? Data { get; set; }
}