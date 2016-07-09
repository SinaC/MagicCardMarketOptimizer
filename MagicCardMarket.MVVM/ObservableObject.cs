using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace MagicCardMarket.MVVM
{
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public void RaisePropertyChangedOnAll()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }

        #region INotifyPropertyChanging

        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanging == null)
                return;
            var propertyName = GetPropertyName(propertyExpression);
            PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [Obsolete("Use RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression) instead please. Sample: RaisePropertyChanged(()=> Property);")]
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (PropertyChanged == null)
                return;
            var propertyName = GetPropertyName(propertyExpression);
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        protected virtual bool Set<TProperty>(Expression<Func<TProperty>> propertyExpression, ref TProperty field, TProperty newValue, bool forceRaisePropertyChanged = false)
        {
            var propertyName = GetPropertyName(propertyExpression);

            if (!forceRaisePropertyChanged && EqualityComparer<TProperty>.Default.Equals(field, newValue))
                return false;

            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));

            field = newValue;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            return true;
        }

        public static string GetPropertyName<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            var propertyInfo = GetPropertyInfo(propertyExpression);
            return propertyInfo?.Name;
        }

        public static TProperty GetPropertyValue<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
                return default(TProperty);

            string propertyName = GetPropertyName(propertyExpression);
            TProperty value = propertyExpression.Compile()();
            Debug.WriteLine("{0} : {1}", propertyName, value);
            return value;
        }

        public static PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TProperty>> propertyExpression)
        {
            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            var body = propertyExpression.Body as MemberExpression;
            if (body == null)
            {
                var ubody = (UnaryExpression) propertyExpression.Body;
                body = ubody.Operand as MemberExpression;
                if (body == null)
                    throw new ArgumentException(String.Format("Expression '{0}' refers to a method, not a property.", propertyExpression));
            }

            var propInfo = body.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(String.Format("Expression '{0}' refers to a field, not a property.", propertyExpression));

            return propInfo;
        }

        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(TSource source, Expression<Func<TProperty>> propertyExpression) where TSource : ObservableObject
        {
            var propInfo = GetPropertyInfo(propertyExpression);
            var type = typeof (TSource);
            if (propInfo.ReflectedType != null && (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType)))
                throw new ArgumentException(String.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyExpression, type));

            return propInfo;
        }
    }
}
