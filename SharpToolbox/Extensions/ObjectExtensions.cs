namespace SharpToolbox.Extensions
{
    /// <summary>
    /// Provide extensions to Object.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns true if the object is any of the specified generic parameters. 
        /// </summary>
        /// <param name="this">Object</param>
        /// <typeparam name="T1">Type 1</typeparam>
        /// <typeparam name="T2">Type 2</typeparam>
        /// <returns>true if the object match with any of the specified types, false otherwise.</returns>
        public static bool IsAnyOf<T1, T2>(this object @this)
        {
            return @this is T1 or T2;
        }

        /// <summary>
        /// Returns true if the object is any of the specified generic parameters. 
        /// </summary>
        /// <param name="this">Object</param>
        /// <typeparam name="T1">Type 1</typeparam>
        /// <typeparam name="T2">Type 2</typeparam>
        /// <typeparam name="T3">Type 3</typeparam>
        /// <returns>true if the object match with any of the specified types, false otherwise.</returns>
        public static bool IsAnyOf<T1, T2, T3>(this object @this)
        {
            return @this is T1 or T2 or T3;
        }

        /// <summary>
        /// Returns true if the object is any of the specified generic parameters. 
        /// </summary>
        /// <param name="this">Object</param>
        /// <typeparam name="T1">Type 1</typeparam>
        /// <typeparam name="T2">Type 2</typeparam>
        /// <typeparam name="T3">Type 3</typeparam>
        /// <typeparam name="T4">Type 4</typeparam>
        /// <returns>true if the object match with any of the specified types, false otherwise.</returns>
        public static bool IsAnyOf<T1, T2, T3, T4>(this object @this)
        {
            return @this is T1 or T2 or T3 or T4;
        }

        /// <summary>
        /// Returns true if the object is any of the specified generic parameters. 
        /// </summary>
        /// <param name="this">Object</param>
        /// <typeparam name="T1">Type 1</typeparam>
        /// <typeparam name="T2">Type 2</typeparam>
        /// <typeparam name="T3">Type 3</typeparam>
        /// <typeparam name="T4">Type 4</typeparam>
        /// <typeparam name="T5">Type 5</typeparam>
        /// <returns>true if the object match with any of the specified types, false otherwise.</returns>
        public static bool IsAnyOf<T1, T2, T3, T4, T5>(this object @this)
        {
            return @this is T1 or T2 or T3 or T4 or T5;
        }
    }
}