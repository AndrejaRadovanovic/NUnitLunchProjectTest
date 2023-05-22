using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;

namespace NUnitLunchProject.Tests.MealProviderTests.MenuTests
{
    [TestFixture]
	public class DeleteMenuMealProviderTests
	{
        private MealDto _meal;
        private int _mealId;
        private MenuDto _menu;
        private int _menuId;
        private MenuResponseDto _createdMenu;
        private Role role = Role.MealProvider;
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
        public void DeleteMenuHappyPathMealProviderTest()
        {
            ClientProxy.Menu.DeleteMenu(_menuId, role);

            Exception ex = Assert.Throws<Exception>(() =>
                 ClientProxy.Menu.GetMenuById(_menuId, role));

            Assert.IsTrue(ex.Message.Contains("Menu not found"));
            _menuId = -1;
        }
    }
}

