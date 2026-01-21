
public class Chip
{
    public ChipType Type { get; private set; }
    public int Level { get; private set; }

    public Chip(ChipType type, int level = 1)
    {
        Type = type;
        Level = level;
    }
    public bool CanMergeWith(Chip other)
    {
        return other != null &&
               Type == other.Type &&
               Level == other.Level;
    }

    public Chip CreateMergedChip()
    {
        return new Chip(Type, Level + 1);
    }
    public override string ToString()
    {
        return $"Chip({Type?.ID ?? "null"}, Level: {Level})";
    }
}
