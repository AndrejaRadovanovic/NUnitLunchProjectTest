using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;

namespace NUnitLunchProject.Tests.MealProviderTests.MealsTests
{
	public class DeleteMealByIdMealProviderTest
	{
        private MealDto _meal;
        private int _mealId;
        private Role role = Role.MealProvider;
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
        public void DeleteMealHappyPathMealProviderTest()
        {
            ClientProxy.Meals.DeleteMeal(_mealId, role);

            Exception ex = Assert.Throws<Exception>(() =>
                 ClientProxy.Meals.GetMealById(_mealId, role));

            Assert.IsTrue(ex.Message.Contains("The selected id is invalid."));
            _mealId = -1;
        }
    }
}

