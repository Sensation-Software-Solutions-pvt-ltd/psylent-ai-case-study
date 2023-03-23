using ApplicationCore.ValueObjects;

namespace ApplicationCore.Interfaces;

public interface IScoring
{
    /// <summary>
    /// Scaling the max score to 100
    /// </summary>
    /// <param name="rawScore"></param>
    /// <returns></returns>
    ScaledScore Scale(RawScore rawScore);

    /// <summary>
    /// Scaling the max score to 100 
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    ScaledScore Scale(Score score);

    /// <summary>
    /// Rank scaled score
    /// </summary>
    /// <param name="score"></param>
    /// <returns></returns>
    RankedCulture Rank(ScaledScore score);
}