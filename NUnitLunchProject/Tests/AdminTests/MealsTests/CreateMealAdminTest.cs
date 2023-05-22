using System;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnitLunchProject.ClientProxy;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.ResponseDto;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.MealsTests
{
	[TestFixture]
	public class CreateMealAdminTest
	{
        private MealDto _meal;
        private int _mealId;
        private Role role = Role.Admin;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _meal = TestHelper.CreateMeal();
        }
        [TearDown]
        public void TearDown()
        {
            if (_mealId != 0)
            {
                ClientProxy.Meals.DeleteMeal(_mealId, role);
            }
        }
        [Test]
        [TestCase("allergenList", "empty")]
        [TestCase("allergenList", "withAllergens")]
        public void CreateMealHappyPathTest(
            string parameter, string value)
        {
            if (value.Equals("withAllergens")){
                _meal.allergen_ids = new int[2];
                _meal.allergen_ids[0] = 1;
                _meal.allergen_ids[1] = 2; }

            MealResponseDto response = ClientProxy.Meals.CreateMeal(_meal, role);
            _mealId = response.data.id;
            Assert.That(response.message.Equals("Meal created successfully"));
            AssertHelper.AssertAllMealParameters(response, _meal);
        }
        [Test]
        [TestCase("mealType", 1)]
        [TestCase("mealType", 2)]
        public void CreateMealHappyPathMealTypeTest(
            string parameter, int value)
        {
            _meal.meal_type_id = value;

            MealResponseDto response = ClientProxy.Meals.CreateMeal(_meal, role);
            _mealId = response.data.id;
            Assert.That(response.message.Equals("Meal created successfully"));
            AssertHelper.AssertAllMealParameters(response, _meal);
        }

        [Test]
        [TestCase("name", "", "The name field is required.")]
        [TestCase("name", null, "The name field is required.")]
        [TestCase("name", "123", "The name format is invalid.")]
        [TestCase("description", "", "The description must be a string.")]
        [TestCase("description", null, "The description must be a string.")]
        [TestCase("description", "123", "The description format is invalid.")]
        [TestCase("description", "<a>", "The description format is invalid.")]
        
        public void CreateMealNegativeAdminTests(
           string parameter,
           string value,
           string message
           )
        {
            _meal = SetPropertyValueHelper.SetMealPropertyValue(parameter, value, _meal);
            Exception ex = Assert.Throws<Exception>(() =>
                   _mealId = ClientProxy.Meals.CreateMeal(_meal, role).data.id);

            Assert.IsTrue(ex.Message.Contains(message));          
        }

        [Test]
        public void CreateMealWithSameNameNegativeAdminTest()
        {
            _mealId = ClientProxy.Meals.CreateMeal(_meal, role).data.id;
            Exception ex = Assert.Throws<Exception>(() =>
                   ClientProxy.Meals.CreateMeal(_meal, role));

            Assert.IsTrue(ex.Message.Contains("The name has already been taken."));
        }
    }
}

