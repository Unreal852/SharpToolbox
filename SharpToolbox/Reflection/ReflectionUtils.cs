using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpToolbox.Reflection;

/// <summary>
/// Provide reflection
/// </summary>
public static class ReflectionHelper
{
    /// <summary>
    /// Cached MemberWiseClone Method
    /// </summary>
    private static readonly MethodInfo CloneMethod = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

    public static bool HasAttribute<T>(this MemberInfo memberInfo) where T : Attribute
    {
        return TryGetAttribute(memberInfo, out T _);
    }

    public static bool TryGetAttribute<T>(this MemberInfo memberInfo, out T attributeOut) where T : Attribute
    {
        attributeOut = memberInfo.GetCustomAttribute<T>();
        return attributeOut != null;
    }

    public static IEnumerable<FieldInfo> GetFieldsByAttribute<T>(Type type) where T : Attribute
    {
        Type attributeType = typeof(T);
        foreach (FieldInfo field in type.GetAllFields())
        {
            if (Attribute.IsDefined(field, attributeType))
                yield return field;
        }
    }

    /*
    
    /// <summary>
    /// Returns the specified object's copy
    /// </summary>
    /// <param name="source">Object to copy</param>
    /// <typeparam name="T">Object Type</typeparam>
    /// <returns>Copy</returns>
    public static T Copy<T>(T source)
    {
        Type sourceType = source.GetType();
        if (sourceType.IsPrimitive) // Todo : implements Non-Primitive values thats should be listed, TimeSpan, Guid, etc
            return source;
        T copiedObj = (T) CloneMethod.Invoke(source, null);
        Validate.Require<NotImplementedException>(!sourceType.IsArray, nameof(source), "Array not yet supported.");
        FieldInfo[] sourceFields = sourceType.GetAllFields();
        PropertyInfo[] sourceProperties = sourceType.GetAllProperties();
        for (int i = 0; i < sourceFields.Length; i++)
        {
            FieldInfo field = sourceFields[i];
            if (field.FieldType.IsPrimitive)
                continue;
            field.SetValue(copiedObj, Copy(field.GetValue(source)));
        }

        for (int i = 0; i < sourceProperties.Length; i++)
        {
            PropertyInfo property = sourceProperties[i];
            if(property.PropertyType.IsPrimitive)
                continue;
            property.SetValue(copiedObj, Copy(property.GetValue(source)));
        }

        return copiedObj;
    } */
}