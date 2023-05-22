using System;
using NUnitLunchProject.Models;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using NUnitLunchProject.Models.ResponseDto;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;

namespace NUnitLunchProject.Helper
{
	public class AssertHelper
	{
		public static void AssertAllCheckinParameters(
            CheckinResponseDto actualCheckin,
            CheckinDto expectedCheckin)
		{
			Assert.That(actualCheckin.data.answer.Equals(expectedCheckin.answer));
            Assert.That(actualCheckin.data.date.Equals(expectedCheckin.date));
		}
        public static void AssertAllCheckinResponseParameters(
            CheckinResponseDto actualCheckin,
            CheckinResponseDto expectedCheckin)
        {
            Assert.That(actualCheckin.data.created_at.Equals(expectedCheckin.data.created_at));
            Assert.That(actualCheckin.data.id.Equals(expectedCheckin.data.id));
            Assert.That(actualCheckin.data.updated_at.Equals(expectedCheckin.data.updated_at));
            Assert.That(actualCheckin.data.answer.Equals(expectedCheckin.data.answer));
            Assert.That(actualCheckin.data.date.Equals(expectedCheckin.data.date));
        }
        public static void AssertAllMealParameters(
            MealResponseDto actualMeal,
            MealDto expectedMeal)
        {
            Assert.That(actualMeal.data.name.Equals(expectedMeal.name));
            Assert.That(actualMeal.data.description.Equals(expectedMeal.description));
            Assert.That(actualMeal.data.meal_type.id.Equals(expectedMeal.meal_type_id));
            Assert.That(actualMeal.data.allergens.Count().Equals(expectedMeal.allergen_ids.Count()));
            if (expectedMeal.allergen_ids.Count() > 0) {
                for (int i = 0; i < expectedMeal.allergen_ids.Count(); i++) {
                    Assert.That(actualMeal.data.allergens[i].id.Equals(expectedMeal.allergen_ids[i]));
                }
            }
        }
        public static void AssertAllMealFromListParameters(
           MealDataDto actualMeal,
           MealDto expectedMeal)
        {
            Assert.That(actualMeal.name.Equals(expectedMeal.name));
            Assert.That(actualMeal.description.Equals(expectedMeal.description));
            Assert.That(actualMeal.meal_type.id.Equals(expectedMeal.meal_type_id));
            Assert.That(actualMeal.allergens.Count().Equals(expectedMeal.allergen_ids.Count()));
            if (expectedMeal.allergen_ids.Count() > 0)
            {
                for (int i = 0; i < expectedMeal.allergen_ids.Count(); i++)
                {
                    Assert.That(actualMeal.allergens[i].id.Equals(expectedMeal.allergen_ids[i]));
                }
            }
        }
        public static void AssertAllMenuParameters(
            MenuResponseDto actualMenu,
            MenuDto expectedMenu)
        {
            Assert.That(actualMenu.data.date.Equals(expectedMenu.date));
            Assert.That(actualMenu.data.items.Count().Equals(expectedMenu.meal_ids.Count()));
            if (expectedMenu.meal_ids.Count() > 0)
            {
                for (int i = 0; i < expectedMenu.meal_ids.Count(); i++)
                {
                    Assert.That(actualMenu.data.items[i].id.Equals(expectedMenu.meal_ids[i]));
                }
            }
        }
        public static void AssertAllMenuFromListParameters(
            MenuDataDto actualMenu,
            MenuDto expectedMenu)
        {
            Assert.That(actualMenu.date.Equals(expectedMenu.date));
            Assert.That(actualMenu.items.Count().Equals(expectedMenu.meal_ids.Count()));
            if (expectedMenu.meal_ids.Count() > 0)
            {
                for (int i = 0; i < expectedMenu.meal_ids.Count(); i++)
                {
                    Assert.That(actualMenu.items[i].id.Equals(expectedMenu.meal_ids[i]));
                }
            }
        }
    }
}

