using System;
using NUnitLunchProject.Models.MealsDtoModels;
using System.Reflection;
using NUnitLunchProject.ClientProxy;
using NUnitLunchProject.Models.MenuDtoModels;

namespace NUnitLunchProject.Helper
{
	public class SetPropertyValueHelper
	{
        public static MealDto SetMealPropertyValue(string parameter, object value, MealDto meal)
        {
            PropertyInfo propertyInfo = meal.GetType().GetProperty(parameter);

            if (propertyInfo != null)
            {
                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                propertyInfo.SetValue(meal, safeValue, null);
            }
            return meal;

        }
        public static MenuDto SetMenuPropertyValue(string parameter, object value, MenuDto menu)
        {
            PropertyInfo propertyInfo = menu.GetType().GetProperty(parameter);

            if (propertyInfo != null)
            {
                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                propertyInfo.SetValue(menu, safeValue, null);
            }
            return menu;

        }
    }
}

