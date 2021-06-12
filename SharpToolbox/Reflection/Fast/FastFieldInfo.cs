using System;
using System.Reflection;

namespace SharpToolbox.Reflection.Fast
{
    /// <summary>
    /// Provide a fast access to a field's Getter and Setter.
    /// Note that the creation of this class is not fast, this should be cached for later uses.
    /// </summary>
    /// <typeparam name="TClass">Class Type</typeparam>
    /// <typeparam name="TValue">Value Type</typeparam>
    public class FastFieldInfo<TClass, TValue> : FastAccessor<FieldInfo>
    {
        public FastFieldInfo(FieldInfo field) : base(field)
        {
            Get = ExpressionHelper.CreateFieldGetter<TClass, TValue>(field);
            Set = ExpressionHelper.CreateFieldSetter<TClass, TValue>(field);
        }

        /// <summary>
        /// Field Getter
        /// </summary>
        public Func<TClass, TValue> Get { get; }

        /// <summary>
        /// Field Setter
        /// </summary>
        public Action<TClass, TValue> Set { get; }
    }
}