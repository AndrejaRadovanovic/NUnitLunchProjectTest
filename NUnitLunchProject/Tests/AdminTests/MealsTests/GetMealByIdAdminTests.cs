using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.MealsTests
{
    [TestFixture]
	public class GetMealByIdAdminTests
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
            role = Role.Admin;
            if (_mealId != 0)
            {
                ClientProxy.Meals.DeleteMeal(_mealId, role);
            }
        }
        [Test]
        [TestCase("Admin", "Meal successfully retrieved")]
        [TestCase("User", "Meal successfully retrieved")]
        [TestCase("MealProvider", "Meal successfully retrieved")]
        public void GetMealByIdPositiveTest(
            string roleValue,
            string message)
        {
            role = Roles.SetRole(roleValue);
            MealResponseDto response = ClientProxy.Meals.GetMealById(_mealId, role);
            Assert.That(response.message.Equals(message));
            AssertHelper.AssertAllMealParameters(response, _meal);
        }

        [Test]
        [TestCase(0, "The selected id is invalid.")]
        [TestCase(-1, "The selected id is invalid.")]
        [TestCase(99999, "The selected id is invalid.")]
        [TestCase(1, "Meal doesn't belong to the user's hub")]//Different hub meal
        public void GetMealNegativeAdminTest(
            int id,
            string message)
        {
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Meals.GetMealById(id, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }
    }
}

