using ApplicationCore.Enums;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using ApplicationCore.TransferObjects;
using ApplicationCore.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace PublicApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ScoresController : ControllerBase
{
    private readonly IScoring _scoring;

    public ScoresController(IScoring scoring)
    {
        _scoring = scoring;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("process")]
    public IActionResult ProcessScoreHandler([FromBody] ProcessScoreRequest request)
    {

        var raw = new RawScore(request.Data.Value);

        //We should check this request body cause if all values are 0 so we are getting null exception.
        if (raw.Create.Value < 1 && raw.Compete.Value < 1 && raw.Collaborate.Value < 1 && raw.Control.Value < 1)
        {
            return BadRequest("Please input atleast one value");
        }
        var scaled = _scoring.Scale(raw);
        var ranked = _scoring.Rank(scaled);
        var response = new ProcessedScoreResponse
        {
            Data = new ProcessedScore
            {
                Raw = raw,
                Scaled = scaled,
                Ranked = ranked
            }
        };
        return Ok(response);
    }

    [HttpPost("rules-evaluator/check")]
    public IActionResult CheckRulesHandler([FromBody] EvaluateScoreRequest request)
    {
        var rawScore = new RawScore(request.Data.Value);
        var allZeroResult = Evaluator.CheckAllZeroRule(rawScore);
        var allLowResult = Evaluator.CheckAllLowScoreRule(rawScore);
        var response = new EvaluatedScoreResponse
        {
            Results = new List<EvaluatedResult>()
            {
                new() { Result = allZeroResult, Name = RuleType.AllZeros },
                new() { Result = allLowResult, Name = RuleType.AllLowScore }
            }
        };
        return Ok(response);
    }
}