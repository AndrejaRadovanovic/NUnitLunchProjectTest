using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.MealsTests
{
	[TestFixture]
	public class GetMealsTests
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
        [TestCase("Admin", "Meals successfully retrieved")]
        [TestCase("User", "Meals successfully retrieved")]
        [TestCase("MealProvider", "Meals successfully retrieved")]
        public void GetMealsPositiveTest(
            string roleValue,
            string message)
        {
            role = Roles.SetRole(roleValue);
           
            MealResponseListDto response = ClientProxy.Meals.GetMeals(role);
            Assert.That(response.message.Equals(message));

            var mealInList = response.data.Where(c => c.id == _mealId).FirstOrDefault();
                       
            AssertHelper.AssertAllMealFromListParameters(mealInList, _meal);

        }
    }
}

