using ApplicationCore.Enums;

namespace ApplicationCore.TransferObjects;

public class EvaluatedScoreResponse
{
    public List<EvaluatedResult> Results { get; set; }
}

public class EvaluatedResult
{
    public RuleType Name { get; set; }
    public EvaluationResult Result { get; set; }
}