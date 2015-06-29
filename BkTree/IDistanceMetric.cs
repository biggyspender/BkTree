namespace BkTree
{
    public interface IDistanceMetric<in TValue>
    {
        int CalculateDistance(TValue item1, TValue item2);
    }
}