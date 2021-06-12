using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SharpToolbox.Reflection
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// Create a delegate of type <see cref="TDelegate"/> based on the specified <see cref="MethodInfo"/>
        /// </summary>
        /// <param name="methodInfo">Method</param>
        /// <param name="instance">Instance ( this should be null for static methods )</param>
        /// <typeparam name="TDelegate">Delegate Type</typeparam>
        /// <returns><see cref="TDelegate"/></returns>
        public static TDelegate CreateDelegate<TDelegate>(MethodInfo methodInfo, object instance = null) where TDelegate : Delegate
        {
            List<UnaryExpression> converters = new List<UnaryExpression>();
            List<ParameterExpression> parameters = new List<ParameterExpression>();

            foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
            {
                ParameterExpression parameterExpression = Expression.Parameter(typeof(object), parameterInfo.Name);
                converters.Add(Expression.Convert(parameterExpression, parameterInfo.ParameterType));
                parameters.Add(parameterExpression);
            }

            Expression methodCallExpression = Expression.Call(instance == null ? null : Expression.Constant(instance), methodInfo, converters);
            if (methodInfo.ReturnType != typeof(void))
            {
                MethodInfo delegateInfo = typeof(TDelegate).GetMethod("Invoke");
                if (delegateInfo != null && methodInfo.ReturnType != delegateInfo.ReturnType)
                    methodCallExpression = Expression.Convert(methodCallExpression, delegateInfo.ReturnType);
            }

            return Expression.Lambda<TDelegate>(methodCallExpression, parameters).Compile();
        }

        /// <summary>
        /// Create a Func that serve as a field getter.
        /// </summary>
        /// <param name="fieldInfo">Field</param>
        /// <typeparam name="TClass">Class instance</typeparam>
        /// <typeparam name="TValue">Field value</typeparam>
        /// <returns><see cref="Func{TClass, TValue}"/></returns>
        public static Func<TClass, TValue> CreateFieldGetter<TClass, TValue>(FieldInfo fieldInfo)
        {
            ParameterExpression ownerTypeParameter = Expression.Parameter(typeof(TClass));
            Expression fieldExpression = Expression.Field(ownerTypeParameter, fieldInfo);
            Expression convertExpression = Expression.Convert(fieldExpression, typeof(TValue));
            return Expression.Lambda<Func<TClass, TValue>>(convertExpression, ownerTypeParameter).Compile();
        }

        /// <summary>
        /// Create a <see cref="Action{TClass, TValue}"/> that serve as a field setter.
        /// </summary>
        /// <param name="fieldInfo">Field</param>
        /// <typeparam name="TClass">Class instance</typeparam>
        /// <typeparam name="TValue">New value</typeparam>
        /// <returns><see cref="Action{TClass, TValue}"/></returns>
        public static Action<TClass, TValue> CreateFieldSetter<TClass, TValue>(FieldInfo fieldInfo)
        {
            ParameterExpression ownerTypeParameter = Expression.Parameter(typeof(TClass));
            ParameterExpression fieldParameter = Expression.Parameter(typeof(TValue));
            Expression fieldExpression = Expression.Field(ownerTypeParameter, fieldInfo);
            Expression assignExpression = Expression.Assign(fieldExpression, Expression.Convert(fieldParameter, fieldInfo.FieldType));
            return Expression.Lambda<Action<TClass, TValue>>(assignExpression, ownerTypeParameter, fieldParameter).Compile();
        }

        /// <summary>
        /// Create a Func that serve as a property getter.
        /// </summary>
        /// <param name="propertyInfo">Property</param>
        /// <typeparam name="TClass">Class instance</typeparam>
        /// <typeparam name="TValue">Property value</typeparam>
        /// <returns><see cref="Func{TClass, TValue}"/></returns>
        public static Func<TClass, TValue> CreatePropertyGetter<TClass, TValue>(PropertyInfo propertyInfo)
        {
            ParameterExpression ownerTypeParameter = Expression.Parameter(typeof(TClass));
            Expression propertyExpression = Expression.Property(ownerTypeParameter, propertyInfo);
            Expression convertExpression = Expression.Convert(propertyExpression, typeof(TValue));
            return Expression.Lambda<Func<TClass, TValue>>(convertExpression, ownerTypeParameter).Compile();
        }

        
        /// <summary>
        /// Create a <see cref="Action{TClass, TValue}"/> that serve as a property setter.
        /// </summary>
        /// <param name="propertyInfo">Property</param>
        /// <typeparam name="TClass">Class instance</typeparam>
        /// <typeparam name="TValue">New value</typeparam>
        /// <returns><see cref="Action{TClass, TValue}"/></returns>
        public static Action<TClass, TValue> CreatePropertySetter<TClass, TValue>(PropertyInfo propertyInfo)
        {
            ParameterExpression ownerTypeParameter = Expression.Parameter(typeof(TClass));
            ParameterExpression propertyParameter = Expression.Parameter(typeof(TValue));
            Expression propertyExpression = Expression.Property(ownerTypeParameter, propertyInfo);
            Expression assignExpression = Expression.Assign(propertyExpression, Expression.Convert(propertyParameter, propertyInfo.PropertyType));
            return Expression.Lambda<Action<TClass, TValue>>(assignExpression, ownerTypeParameter, propertyParameter).Compile();
        }
    }
}