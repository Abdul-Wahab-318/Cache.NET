interface ICache
{
    void Initialize();
    // Add method throws duplicate key exception if already exists.
    void Add(string key, object value);
    // Returns the cached object against the given key. Returns null if it does not exist.
    object Get(string key);
    // Updates the complete object in the cache for the given key. Throws exception if key does not exist.
    void Update(string key, object value);
    // Removes the object from cache against the given key. Does nothing if it does not exist.
    void Remove(string key);
    // Clears the cache.
    void Clear();
    // Disposes the ICache instance and all underlying connections.
    void Dispose();
}