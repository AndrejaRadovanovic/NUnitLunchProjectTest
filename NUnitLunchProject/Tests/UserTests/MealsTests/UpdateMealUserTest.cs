using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;

namespace NUnitLunchProject.Tests.UserTests.MealsTests
{
    [TestFixture]
	public class UpdateMealUserTest
	{
        private MealDto _meal;
        private int _mealId;
        private Role role = Role.User;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _meal = TestHelper.CreateMeal();
            _mealId = ClientProxy.Meals.CreateMeal(_meal, Role.Admin).data.id;
        }
        [TearDown]
        public void TearDown()
        {
            if (_mealId != 0)
            {
                ClientProxy.Meals.DeleteMeal(_mealId, Role.Admin);
            }
        }

        [Test]
        public void UpdateMealNegativeUserTest()
        {
            Exception ex = Assert.Throws<Exception>(() =>
                       ClientProxy.Meals.UpdateMealById(_mealId, _meal, role));

            Assert.IsTrue(ex.Message.Contains("You are not authorized to access this route."));
        }
    }
}

