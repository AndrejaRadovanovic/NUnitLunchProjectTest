using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.MenuTests
{
	[TestFixture]
	public class GetMenusTest
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
            role = Role.Admin;
            ClientProxy.Menu.DeleteMenu(_menuId, role);
            ClientProxy.Meals.DeleteMeal(_mealId, role);
        }

        [Test]
        [TestCase("Admin", "Menus successfully retrieved")]
        [TestCase("MealProvider", "Menus successfully retrieved")]
        [TestCase("User", "Menus successfully retrieved")]
        public void GetMenusHappyPathAllRolesTest(
            string roleValue,
            string message)
        {
            role = Roles.SetRole(roleValue);
            MenuResponseListDto response = ClientProxy.Menu.GetMenus( role);
            Assert.That(response.message.Equals(message));

            var menuInList = response.data.Where(c => c.id == _menuId).FirstOrDefault();
            Assert.IsNotNull(menuInList);
            AssertHelper.AssertAllMenuFromListParameters(menuInList, _menu);
        }   
    }
}

