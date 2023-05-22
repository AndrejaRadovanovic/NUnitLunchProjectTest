using System;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnitLunchProject.ClientProxy;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.MealsTests
{
    [TestFixture]
    public class UpdateMealAdminTest
	{
        private MealDto _meal;
        private int _mealId;
        private Role role = Role.Admin;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _meal = TestHelper.CreateMeal();
            _mealId = ClientProxy.Meals.CreateMeal(_meal, role).data.id;
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
        [TestCase("name", "newName")]
        [TestCase("description", "newDescription")]
        [TestCase("mealType", "1")]
        [TestCase("mealType", "2")]
        public void UpdateMealHappyPathAdminTest(
            string parameter, string value)
        {
            _meal = SetPropertyValueHelper.SetMealPropertyValue(parameter, value, _meal);
           

            MealResponseDto response = ClientProxy.Meals.UpdateMealById(_mealId, _meal, role);

            Assert.That(response.message.Equals("Meal successfully updated"));
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

        public void UpdateMealNegativeAdminTests(
           string parameter,
           string value,
           string message
           )
        {
            _meal = SetPropertyValueHelper.SetMealPropertyValue(parameter, value, _meal);
            Exception ex = Assert.Throws<Exception>(() =>
                   ClientProxy.Meals.UpdateMealById(_mealId, _meal, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }
        [Test]
        public void UpdateMealToExistingMealNameAdminTest()
        {
            MealDto newMeal = TestHelper.CreateMeal();
            int _newMealId = ClientProxy.Meals.CreateMeal(newMeal, role).data.id;
            _meal.name = newMeal.name;

            Exception ex = Assert.Throws<Exception>(() =>
                   ClientProxy.Meals.UpdateMealById(_mealId, _meal, role));
            Assert.IsTrue(ex.Message.Contains("The name has already been taken."));
        }
    }
}

