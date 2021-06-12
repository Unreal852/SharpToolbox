using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SharpToolbox.Reflection
{
    /// <summary>
    /// Provide reflection extensions.
    /// </summary>
    public static class ReflectionExtensions
    {
        private static Type CompilerGeneratedAttributeType { get; } = typeof(CompilerGeneratedAttribute);

        /// <summary>
        /// Get all fields in the specified type.
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="flags">Binding Flags</param>
        /// <param name="ignoreCompilerGenerated">Ignore Compiler Generated fields</param>
        /// <returns>Fields</returns>
        public static IEnumerable<FieldInfo> GetAllFields(this Type type,
                                                          BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                                               BindingFlags.Static, bool ignoreCompilerGenerated = true)
        {
            foreach (FieldInfo field in type.GetFields(flags))
            {
                if (ignoreCompilerGenerated && Attribute.IsDefined(field, CompilerGeneratedAttributeType))
                    continue;
                yield return field;
            }
        }

        /// <summary>
        /// Get all properties in the specified type.
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="flags">Binding Flags</param>
        /// <param name="ignoreCompilerGenerated">Ignore Compiler Generated properties</param>
        /// <returns>Properties</returns>
        public static IEnumerable<PropertyInfo> GetAllProperties(this Type type,
                                                                 BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                                                      BindingFlags.Static, bool ignoreCompilerGenerated = true)
        {
            foreach (PropertyInfo property in type.GetProperties(flags))
            {
                if (ignoreCompilerGenerated && Attribute.IsDefined(property, CompilerGeneratedAttributeType))
                    continue;
                yield return property;
            }
        }

        /// <summary>
        /// Get all methods in the specified type.
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="flags">Binding Flags</param>
        /// <param name="ignoreCompilerGenerated">Ignore Compiler Generated methods</param>
        /// <returns>Methods</returns>
        public static IEnumerable<MethodInfo> GetAllMethods(this Type type,
                                                            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                                                 BindingFlags.Static, bool ignoreCompilerGenerated = true)
        {
            foreach (MethodInfo method in type.GetMethods(flags))
            {
                if (ignoreCompilerGenerated && Attribute.IsDefined(method, CompilerGeneratedAttributeType))
                    continue;
                yield return method;
            }
        }

        /// <summary>
        /// Get all constructors in the specified type.
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="flags">Binding Flags</param>
        /// <param name="ignoreCompilerGenerated">Ignore Compiler Generated constructors</param>
        /// <returns>Connstructors</returns>
        public static IEnumerable<ConstructorInfo> GetAllConstructors(this Type type,
                                                                      BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                                                                                           BindingFlags.Static,
                                                                      bool ignoreCompilerGenerated = true)
        {
            foreach (ConstructorInfo ctor in type.GetConstructors(flags))
            {
                if (ignoreCompilerGenerated && Attribute.IsDefined(ctor, CompilerGeneratedAttributeType))
                    continue;
                yield return ctor;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<(FieldInfo Field, T Attribute)> GetFieldsByAttribute<T>(this Type type) where T : Attribute
        {
            foreach (FieldInfo field in type.GetAllFields())
            {
                var attribute = field.GetCustomAttribute<T>();
                if (attribute != null)
                    yield return (field, attribute);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<(PropertyInfo Property, T Attribute)> GetPropertiesByAttribute<T>(this Type type) where T : Attribute
        {
            ;
            foreach (PropertyInfo property in type.GetAllProperties())
            {
                var attribute = property.GetCustomAttribute<T>();
                if (attribute != null)
                    yield return (property, attribute);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<(MethodInfo Method, T Attribute)> GetMethodsByAttribute<T>(this Type type) where T : Attribute
        {
            foreach (MethodInfo method in type.GetAllMethods())
            {
                var attribute = method.GetCustomAttribute<T>();
                if (attribute != null)
                    yield return (method, attribute);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<(ConstructorInfo Constructor, T Attribute)> GetConstructorsByAttribute<T>(this Type type) where T : Attribute
        {
            foreach (ConstructorInfo ctor in type.GetAllConstructors())
            {
                var attribute = ctor.GetCustomAttribute<T>();
                if (attribute != null)
                    yield return (ctor, attribute);
            }
        }
    }
}