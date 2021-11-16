using System;
using System.Reflection;
using SharpToolbox.Exceptions;

namespace SharpToolbox.Reflection.Fast;

/// <summary>
/// The base class for fast accessors.
/// </summary>
/// <typeparam name="TAccessor">Accessor Type</typeparam>
public abstract class FastAccessor<TAccessor> where TAccessor : MemberInfo
{
    protected FastAccessor(TAccessor accessor)
    {
        Throw.IfNull(accessor, nameof(accessor));
        Info = accessor;
    }

    /// <summary>
    /// Member Info
    /// </summary>
    public TAccessor Info { get; }
}

public static class FastAccessor
{
        
    /// <summary>
    /// Returns <see cref="FastFieldInfo{TClass,TValue}"/> for the specified field.
    /// </summary>
    /// <param name="fieldName">Field Name</param>
    /// <param name="bindingFlags">Field Binding Flags</param>
    /// <typeparam name="TClass">Field's owner class type</typeparam>
    /// <typeparam name="TValue">Field's value type</typeparam>
    /// <returns><see cref="FastFieldInfo{TClass,TValue}"/></returns>
    public static FastFieldInfo<TClass, TValue> GetField<TClass, TValue>(string fieldName, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
    {
        Type type = typeof(TClass);
        FieldInfo fieldInfo = type.GetField(fieldName, bindingFlags);
        return new FastFieldInfo<TClass, TValue>(fieldInfo);
    }

    /// <summary>
    /// Returns <see cref="FastPropertyInfo{TClass,TValue}"/> for the specified property.
    /// </summary>
    /// <param name="fieldName">Property Name</param>
    /// <param name="bindingFlags">Property Binding Flags</param>
    /// <typeparam name="TClass">Property's owner class type</typeparam>
    /// <typeparam name="TValue">Property's value type</typeparam>
    /// <returns><see cref="FastPropertyInfo{TClass,TValue}"/></returns>
    public static FastPropertyInfo<TClass, TValue> GetProperty<TClass, TValue>(string fieldName, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)
    {
        Type type = typeof(TClass);
        PropertyInfo fieldInfo = type.GetProperty(fieldName, bindingFlags);
        return new FastPropertyInfo<TClass, TValue>(fieldInfo);
    }
}