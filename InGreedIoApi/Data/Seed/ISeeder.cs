namespace InGreedIoApi.Data.Seed
{
    public interface ISeeder<T>
    {
        IEnumerable<T> Seed { get; }
    }
}