﻿class Program
{
    static async Task Main()
    {
        for (int i = 0; i < 20; i++)
        {
            IEnumerable<string> data = await GetSomeDataAsync();
            await Task.Delay(1000);
        }
        Console.ReadLine();
    }

    private static DateTime _retrieved = default;
    private static IEnumerable<string>? _cachedData;
    public static async ValueTask<IEnumerable<string>> GetSomeDataAsync()
    {
        if (_retrieved >= DateTime.Now.AddSeconds(-5) && _cachedData != null)
        {
            Console.WriteLine("data from the cache");
            return await new ValueTask<IEnumerable<string>>(_cachedData);
        }

        Console.WriteLine("data from the service");
        (_cachedData, _retrieved) = await GetTheRealData();
        return _cachedData;
    }

    public static Task<(IEnumerable<string> data, DateTime retrievedTime)> GetTheRealData() =>
        Task.FromResult(
            (Enumerable.Range(0, 10)
                .Select(x => $"item {x}").AsEnumerable(),
            DateTime.Now));
}
