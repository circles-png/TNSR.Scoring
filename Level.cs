using System.Collections;

namespace TNSR.Scoring;
sealed class Level : ICollection<Time>
{
    public List<Time> times = new();
    public int levelNumber;

    public Level(List<Time>? times, int levelNumber)
    {
        this.times = times ?? new();
        this.levelNumber = levelNumber;
    }

    public int Count
        => ((ICollection<Time>)times).Count;
    public bool IsReadOnly
        => ((ICollection<Time>)times).IsReadOnly;
    public void Add(Time item)
        => ((ICollection<Time>)times).Add(item);
    public void Clear()
        => ((ICollection<Time>)times).Clear();
    public bool Contains(Time item)
        => ((ICollection<Time>)times).Contains(item);
    public void CopyTo(Time[] array, int arrayIndex)
        => ((ICollection<Time>)times).CopyTo(array, arrayIndex);
    public IEnumerator<Time> GetEnumerator()
        => ((IEnumerable<Time>)times).GetEnumerator();
    public bool Remove(Time item)
        => ((ICollection<Time>)times).Remove(item);
    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)times).GetEnumerator();
}
