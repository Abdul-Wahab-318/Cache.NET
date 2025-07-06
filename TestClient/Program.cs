using CacheClient.Cache;
public class Program
{
    private static void Main(string[] args)
    {
        NCache cache = new NCache("127.0.0.1" , 1337 , "myCache");
        cache.Initialize();
        cache.Add("first_item", "deez nuts");

        Console.ReadKey();

        string cache_item = cache.Get("first_item") as string ;
        Console.WriteLine("My cache item : " + cache_item);

        Console.ReadKey();
        cache.Dispose();
    }
}