using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.MenuTests
{
    [TestFixture]
	public class DeleteMenuAdminTests
	{
        private MealDto _meal;
        private int _mealId;
        private MenuDto _menu;
        private int _menuId;
        private MenuResponseDto _createdMenu;
        private Role role = Role.Admin;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _meal = TestHelper.CreateMeal();
            _mealId = ClientProxy.Meals.CreateMeal(_meal, role).data.id;
            _menu = TestHelper.CreateMenu(_mealId);
            _createdMenu = ClientProxy.Menu.CreateMenu(_menu, role);
            _menuId = _createdMenu.data.id;

        }
        [TearDown]
        public void TearDown()
        {
            if (_menuId != -1)
            {
                ClientProxy.Menu.DeleteMenu(_menuId, role);
            }
            
            if (_mealId != 0)
            {
                ClientProxy.Meals.DeleteMeal(_mealId, role);
            }
        }
        [Test]
        public void DeleteMenuHappyPathAdminTest()
        {
            ClientProxy.Menu.DeleteMenu(_menuId, role);

            Exception ex = Assert.Throws<Exception>(() =>
                 ClientProxy.Menu.GetMenuById(_menuId, role));

            Assert.IsTrue(ex.Message.Contains("Menu not found"));
            _menuId = -1;
        }

        [Test]
        public void DeleteMenuAfterMealDeleteAdminTest()
        {
            ClientProxy.Meals.DeleteMeal(_mealId, role);
            _mealId = 0;
            //ClientProxy.Menu.DeleteMenu(_menuId, role);

            Exception ex = Assert.Throws<Exception>(() =>
                 ClientProxy.Menu.GetMenuById(_menuId, role));

            Assert.IsTrue(ex.Message.Contains("Menu not found"));
            _menuId = -1;
        }

        [Test]
        [TestCase(0, "The selected id is invalid.")]
        [TestCase(-1, "The selected id is invalid.")]
        [TestCase(99999, "Menu not found")]
        public void DeleteMenuNegativeAdminTest(
           int id,
           string message)
        {
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Menu.DeleteMenu(id, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }
    }
}

