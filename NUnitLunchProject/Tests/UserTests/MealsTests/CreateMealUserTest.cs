using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;

namespace NUnitLunchProject.Tests.UserTests.MealsTests
{
    [TestFixture]
    public class CreateMealUserTest
	{
        private MealDto _meal;
        private int _mealId;
        private Role role = Role.User;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _meal = TestHelper.CreateMeal();
        }
        
        [Test]
        public void CreateMealNegativeUserTest()
        {
            Exception ex = Assert.Throws<Exception>(() =>
                       ClientProxy.Meals.CreateMeal(_meal, role));

            Assert.IsTrue(ex.Message.Contains("You are not authorized to access this route."));
        }
    }
}

