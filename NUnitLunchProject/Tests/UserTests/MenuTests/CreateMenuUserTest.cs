using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;

namespace NUnitLunchProject.Tests.UserTests.MenuTests
{
    [TestFixture]
	public class CreateMenuUserTest
	{
        private MealDto _meal;
        private int _mealId;
        private MenuDto _menu;
        private Role role = Role.User;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _meal = TestHelper.CreateMeal();
            _mealId = ClientProxy.Meals.CreateMeal(_meal, Role.Admin).data.id;
            _menu = TestHelper.CreateMenu(_mealId);

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
        public void CreateMenuUserNegativeTest()
        {        
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.CreateMenu(_menu, role));

            Assert.IsTrue(ex.Message.Contains("You are not authorized to access this route."));
        }
    }
}

