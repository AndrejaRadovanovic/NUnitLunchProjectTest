using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;

namespace NUnitLunchProject.Tests.AdminTests.MealsTests
{
    [TestFixture]
	public class DeleteMealByIdAdminTests
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
            if (_mealId != -1)
            {
                ClientProxy.Meals.DeleteMeal(_mealId, role);
            }
        }
        [Test]
        public void DeleteMealHappyPathAdminTest()
        {
            ClientProxy.Meals.DeleteMeal(_mealId, role);

            Exception ex = Assert.Throws<Exception>(() =>
                 ClientProxy.Meals.GetMealById(_mealId, role));

            Assert.IsTrue(ex.Message.Contains("The selected id is invalid"));
            _mealId = -1;
        }
        [Test]
        [TestCase(0, "The selected id is invalid.")]
        [TestCase(-1, "The selected id is invalid.")]
        [TestCase(99999, "The selected id is invalid")]
        [TestCase(1, "Meal doesn't belong to the user's hub")]//Different hub meal
        public void DeleteMealNegativeAdminTest(
           int id,
           string message)
        {
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Meals.DeleteMeal(id, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }
    }
}

