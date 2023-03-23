namespace ApplicationCore.ValueObjects;

public struct Score
{
    // datatype uint instead of int. Becase we are converting 
    public uint Collaborate { get; set; }
    public uint Create { get; set; }
    public uint Compete { get; set; }
    public uint Control { get; set; }
}