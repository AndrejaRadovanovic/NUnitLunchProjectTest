using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;

namespace NUnitLunchProject.Tests.UserTests.MealsTests
{
    [TestFixture]
	public class DeleteMealByIdUserTest
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
            ClientProxy.Meals.DeleteMeal(_mealId, role);       
        }
        [Test]
        public void DeleteMealUserTest()
        {

            Exception ex = Assert.Throws<Exception>(() =>
                 ClientProxy.Meals.DeleteMeal(_mealId, Role.User));

            Assert.IsTrue(ex.Message.Contains("You are not authorized to access this route."));
           
        }
    }
}

