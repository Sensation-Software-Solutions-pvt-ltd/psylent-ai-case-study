using ApplicationCore.Enums;
using ApplicationCore.ValueObjects;
using ApplicationCore.Interfaces;


namespace ApplicationCore.Services;

/// <summary>
///     Evaluator
/// </summary>
public static class Evaluator
{
    /// <summary>
    ///     When the score for all four quadrants is zero the profile should be balanced
    ///     <code>Collaborate == 0 &amp;&amp; Create == 0 &amp;&amp; Compete == 0 &amp;&amp; Control == 0</code>
    /// </summary>
    /// <param name="score"> Raw Score</param>
    /// <returns><see cref="EvaluationResult" /> Whether the rule is applied or failed</returns>
    public static EvaluationResult CheckAllZeroRule(RawScore score)
    {
        var collaborate = score.Collaborate.Value;
        var create = score.Collaborate.Value;
        var compete = score.Compete.Value;
        var control = score.Control.Value;
        var allZeros = collaborate == 0 && create == 0 && compete == 0 && control == 0;
        var checkResult = allZeros switch
        {
            true => EvaluationResult.Applied,
            false => EvaluationResult.FailedChecks
        };
        return checkResult;
    }

    /// <summary>
    ///     When the raw scores of the four quadrants are less than 10
    ///     Checks:
    ///     <code>Collaborate &lt; 10 and Create &lt; 10 and Compete &lt; 10 and Control &lt; 10</code>
    /// </summary>
    /// <param name="score">Raw Score</param>
    /// <returns><see cref="EvaluationResult" /> Whether the rule is applied or failed</returns>
    public static EvaluationResult CheckAllLowScoreRule(RawScore score)
    {
        var collaborate = score.Collaborate.Value;
        var create = score.Create.Value;
        var compete = score.Compete.Value;
        var control = score.Compete.Value;
        var allLowScore = collaborate < 10 && create < 10 && compete < 10 && control <= 10;

        var checkResult = allLowScore switch
        {
            true => EvaluationResult.Applied,
            false => EvaluationResult.FailedChecks
        };
        return checkResult;
    }
}