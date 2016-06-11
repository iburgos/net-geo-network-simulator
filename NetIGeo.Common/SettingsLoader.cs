using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace NetIGeo.Common
{
    public abstract class SettingsLoader
    {
        public SettingsLoader()
            : this(ConfigurationManager.AppSettings)
        {
        }

        public SettingsLoader(NameValueCollection settings)
        {
            GetType().GetProperties().ToList().ForEach(propertyInfo =>
            {
                var propertyType = propertyInfo.PropertyType;
                var value = settings[propertyInfo.Name];

                if (value == null)
                    throw new Exception(propertyInfo.Name);

                try
                {
                    object finalValue;

                    if (propertyType != typeof(string) && (
                        typeof(IEnumerable).IsAssignableFrom(propertyType) ||
                        typeof(ICollection).IsAssignableFrom(propertyType)
                        ))
                    {
                        var elementType = GetPropertyElementType(propertyType);

                        var listValues = SplitStringValues(value);

                        finalValue = CreateTypedList(listValues, elementType);
                    }
                    else
                        finalValue = Convert.ChangeType(value, propertyInfo.PropertyType, CultureInfo.InvariantCulture);

                    propertyInfo.SetValue(this, finalValue);
                }
                catch (Exception exception)
                {
                    throw new Exception(propertyInfo.Name, exception);
                }
            });

            Validate();
        }

        protected abstract void Validate();

        private Type GetPropertyElementType(Type propertyType)
        {
            var elementType = typeof(object);
            if (propertyType.IsGenericType)
                elementType = propertyType.GetGenericArguments()[0];

            return elementType;
        }

        private IEnumerable<string> SplitStringValues(string value)
        {
            const char SETTING_LIST_SEPARATOR = ',';

            string[] listValues;

            if (value.Trim() == string.Empty)
                listValues = new string[] {};
            else
                listValues = value.Split(SETTING_LIST_SEPARATOR);

            return listValues;
        }

        private object CreateTypedList(IEnumerable<string> values, Type elementType)
        {
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(elementType);
            var list = (IList) Activator.CreateInstance(constructedListType);

            foreach (var element in values)
                list.Add(Convert.ChangeType(element.Trim(), elementType, CultureInfo.InvariantCulture));

            return list;
        }
    }
}