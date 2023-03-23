using ApplicationCore.ValueObjects;

namespace ApplicationCore.TransferObjects;

public class ProcessScoreRequest
{
    public Score? Data { get; set; }
}