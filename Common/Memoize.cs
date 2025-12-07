#nullable enable
namespace Common
{
    /// <summary>
    /// Simple memoization store: outer dictionary keyed by method name,
    /// inner dictionary keyed by method-specific key -> stored value.
    /// Ensures inner dictionaries are created on demand and provides
    /// small convenience APIs. Thread-safe for concurrent use.
    /// </summary>
    public static class Memoize
    {
        // Outer dictionary: methodName -> (key -> value)
        private static readonly Dictionary<string, Dictionary<string, object?>> _staticCache = new();

        // Global sync for creating inner dictionaries
        private static readonly object _outerSync = new();

        private static Dictionary<string, object?> EnsureInner(string methodName)
        {
            // Fast-path without lock if already exists
            if (_staticCache.TryGetValue(methodName, out var inner))
                return inner;

            lock (_outerSync)
            {
                if (!_staticCache.TryGetValue(methodName, out inner))
                {
                    inner = new Dictionary<string, object?>();
                    _staticCache[methodName] = inner;
                }
                return inner;
            }
        }

        /// <summary>
        /// Try to get a value for the given method name and key. Returns true if a value exists and is assignable to T.
        /// </summary>
        public static bool TryGet<T>(string methodName, string key, out T? value)
        {
            value = default;
            var inner = EnsureInner(methodName);
            lock (inner)
            {
                if (inner.TryGetValue(key, out var obj))
                {
                    if (obj is T t)
                    {
                        value = t;
                        return true;
                    }

                    // allow stored null to be returned for nullable/reference T
                    if (obj is null && default(T) is null)
                    {
                        value = default;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Set or overwrite a memoized value.
        /// </summary>
        public static void Set(string methodName, string key, object? value)
        {
            var inner = EnsureInner(methodName);
            lock (inner)
            {
                inner[key] = value;
            }
        }

        /// <summary>
        /// Get existing value or create/store a new one using the factory.
        /// </summary>
        public static T GetOrAdd<T>(string methodName, string key, Func<T> factory)
        {
            if (TryGet<T>(methodName, key, out var existing))
                return existing!;

            var newValue = factory();
            Set(methodName, key, newValue!);
            return newValue;
        }

        /// <summary>
        /// Remove a single entry for a method. Returns true if removed.
        /// </summary>
        public static bool Remove(string methodName, string key)
        {
            var inner = EnsureInner(methodName);
            lock (inner)
            {
                return inner.Remove(key);
            }
        }

        /// <summary>
        /// Clear all memoized entries for a specific method.
        /// </summary>
        public static void ClearMethod(string methodName)
        {
            lock (_outerSync)
            {
                _staticCache.Remove(methodName);
            }
        }

        /// <summary>
        /// Clear all memoized data.
        /// </summary>
        public static void ClearAll()
        {
            lock (_outerSync)
            {
                _staticCache.Clear();
            }
        }
    }
}
