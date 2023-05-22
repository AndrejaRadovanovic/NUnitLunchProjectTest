using System;
using System.Globalization;
using NUnitLunchProject.Models;
using NUnitLunchProject.Models.CalendarDtoModels;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NUnitLunchProject.Helper
{
	public class TestHelper
	{
        public static int ReturnRandomNumber()
        {
            Random r = new Random();
            int num = r.Next(1, 1000);
            return num;
        }
		
        public static CheckinDto CreateCheckin()
        {
            DateTime dateTomorow = DateTime.UtcNow.AddDays(1);
            return new CheckinDto
            {

                date = dateTomorow.ToString("yyyy-MM-dd"),
				answer = "yes"
			};
		}

        public static MealDto CreateMeal()
        {
            int num = ReturnRandomNumber();
            int[] allergens = new int[0];
            return new MealDto
            {
                name = "Test Meal" + num,
                description = "Test Description",
                meal_type_id = 2,
                allergen_ids = allergens
            };
        }
        public static MenuDto CreateMenu(int mealid)
        {
            DateTime dateTomorow = DateTime.UtcNow.AddDays(1);
            int[] mealids = new int[1];
            mealids[0] = mealid;
            return new MenuDto
            {
                date = dateTomorow.ToString("yyyy-MM-dd"),
                meal_ids = mealids
            };
        }

        public static CalendarDatesDto CreateDateSpan(string dateFrom, string dateTo)
        {
            return new CalendarDatesDto
            {
                date_from = dateFrom,
                date_to = dateTo
            };
        }
       
    }
}

