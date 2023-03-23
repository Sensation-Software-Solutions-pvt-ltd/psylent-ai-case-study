using ApplicationCore.Enums;

namespace ApplicationCore.ValueObjects;

public class RawScore
{
    public CultureScore Collaborate { get; set; }
    public CultureScore Create { get; set; }
    public CultureScore Compete { get; set; }
    public CultureScore Control { get; set; }

    public RawScore(Score inputScore)
    {
        Collaborate = new CultureScore { Culture = Culture.Collaborate, Value = inputScore.Collaborate };
        Create = new CultureScore { Culture = Culture.Create, Value = inputScore.Create };
        //Compete = new CultureScore { Culture = Culture.Compete, Value = Convert.ToUInt32(inputScore.Create) };
        ///for Compete Value should be inputScore.Compete instead of inputScore.Create

        Compete = new CultureScore { Culture = Culture.Compete, Value = inputScore.Compete };

        //  Control = new CultureScore { Culture = Culture.Compete, Value = Convert.ToUInt32(inputScore.Control) };
        ///for Control Culture should be Culture.Control instead of Culture.Compete

        Control = new CultureScore { Culture = Culture.Control, Value = inputScore.Control };
    }

    public override string ToString()
    {
        return
            $"Collaborate: {Collaborate.Value}, Create: {Create.Value}, Compete: {Compete.Value}, Control: {Control.Value}";
    }


    /// <summary>
    /// Get max score from all cultures
    /// </summary>
    /// <returns></returns>
    private uint MaxScore()
    {
        // data type should be defind uint instead of var  
        uint max1 = Math.Max(Collaborate.Value, Create.Value);
        uint max2 = Math.Max(Compete.Value, Control.Value);
        return Math.Max(max1, max2);
    }

    /// <summary>
    /// Scaling the score
    /// </summary>
    /// <returns></returns>
    public ScaledScore Scale()
    {
        var maxScore = MaxScore();
        var scalingFactor = maxScore / 100f;

        return new ScaledScore
        {
            Collaborate = Collaborate.Scale(scalingFactor),
            Create = Create.Scale(scalingFactor),
            Compete = Compete.Scale(scalingFactor),
            Control = Control.Scale(scalingFactor)
        };
    }
}