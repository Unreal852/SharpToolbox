using System.Threading.Tasks;

namespace SharpToolbox.Serializers
{
    /// <summary>
    /// Provide a basic interface for serializers.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Returns the serialized data of the specified object.
        /// </summary>
        /// <param name="object">Object to serialize</param>
        /// <typeparam name="T">Object Type</typeparam>
        byte[] Serialize<T>(T @object);

        /// <summary>
        /// Returns the serialized date of the specified object.
        /// Note: the default implementation use the Non-Async Serialize method and run it into a Task.
        /// </summary>
        /// <param name="object">Object to serialize</param>
        /// <typeparam name="T">Object Type</typeparam>
        /// <returns>Serialized Data</returns>
        Task<byte[]> SerializeAsync<T>(T @object)
        {
            return Task.Run(() => Serialize(@object));
        }

        /// <summary>
        /// Returns the deserialized object of the specified serialized data.
        /// </summary>
        /// <param name="serializedData">Object to deserialize</param>
        /// <typeparam name="T">Object Type</typeparam>
        /// <returns>Deserialized Data</returns>
        T Deserialize<T>(byte[] serializedData);

        /// <summary>
        /// Returns the deserialized object of the specified serialized data.
        /// Note: the default implementation use the Non-Async Deserialize method and run it into a Task.
        /// </summary>
        /// <param name="serializedData">Object to deserialize</param>
        /// <typeparam name="T">Object Type</typeparam>
        /// <returns>Deserialized Data</returns>
        Task<T> DeserializeAsync<T>(byte[] serializedData)
        {
            return Task.Run(() => Deserialize<T>(serializedData));
        }
    }
}