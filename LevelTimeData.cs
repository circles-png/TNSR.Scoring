using System.Collections;

namespace TNSR.Scoring;
sealed class LevelTimeData : ICollection<Level>
{
    internal List<Level> levels = new();

    internal Level this[int levelNumber]
    {
        get => levels
            .Where(level => level.levelNumber == levelNumber)
            .Single();
    }

    public int Count
        => ((ICollection<Level>)levels).Count;
    public bool IsReadOnly
        => ((ICollection<Level>)levels).IsReadOnly;
    public void Add(Level item)
        => ((ICollection<Level>)levels).Add(item);
    public void Clear()
        => ((ICollection<Level>)levels).Clear();
    public bool Contains(Level item)
        => ((ICollection<Level>)levels).Contains(item);
    public void CopyTo(Level[] array, int arrayIndex)
        => ((ICollection<Level>)levels).CopyTo(array, arrayIndex);
    public IEnumerator<Level> GetEnumerator()
        => ((IEnumerable<Level>)levels).GetEnumerator();
    public bool Remove(Level item)
        => ((ICollection<Level>)levels).Remove(item);
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)levels).GetEnumerator();
}
