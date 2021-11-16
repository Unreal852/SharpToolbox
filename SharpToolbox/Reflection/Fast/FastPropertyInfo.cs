using System;
using System.Reflection;
using SharpToolbox.Reflection.Fast;

namespace SharpToolbox.Reflection.Fast;

/// <summary>
/// Provide a fast access to a property's Getter and Setter.
/// Note that the creation of this class is not fast, this should be cached for later uses.
/// </summary>
/// <typeparam name="TClass">Class Type</typeparam>
/// <typeparam name="TValue">Value Type</typeparam>
public class FastPropertyInfo<TClass, TValue> : FastAccessor<PropertyInfo>
{
    public FastPropertyInfo(PropertyInfo property) : base(property)
    {
        if (property.CanRead)
            Get = ExpressionHelper.CreatePropertyGetter<TClass, TValue>(property);
        if (property.CanWrite)
            Set = ExpressionHelper.CreatePropertySetter<TClass, TValue>(property);
    }

    /// <summary>
    /// Property Getter
    /// </summary>
    public Func<TClass, TValue> Get { get; }

    /// <summary>
    /// Property Setter
    /// </summary>
    public Action<TClass, TValue> Set { get; }
}