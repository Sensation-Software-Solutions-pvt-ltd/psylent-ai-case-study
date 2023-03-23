using ApplicationCore.Interfaces;
using ApplicationCore.ValueObjects;

namespace ApplicationCore.Services;

public class ScoringService: IScoring
{
    public ScaledScore Scale(RawScore rawScore)
    {
        return rawScore.Scale();
    }

    public ScaledScore Scale(Score score)
    {
        var raw = new RawScore(score);
        return raw.Scale();
    }

    public RankedCulture Rank(ScaledScore score)
    {
        var arr = new List<CultureScore> { score.Collaborate, score.Create, score.Compete, score.Control };
        arr.Sort();
        return new RankedCulture
        {
            First = arr[3].Culture,
            Second = arr[2].Culture,
            Third = arr[1].Culture,
            Fourth = arr[0].Culture,
        };
    }
}