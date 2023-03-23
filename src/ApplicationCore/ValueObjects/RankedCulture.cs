using ApplicationCore.Enums;

namespace ApplicationCore.ValueObjects;

public class RankedCulture
{
    public Culture First { get; set; }
    public Culture Second { get; set; }
    public Culture Third { get; set; }
    public Culture Fourth { get; set; }
}