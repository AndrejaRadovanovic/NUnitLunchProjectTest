using System;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.MenuTests
{
    [TestFixture]
	public class GetMenuByIdTest
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
        [TestCase("Admin", "Menu successfully retrieved")]
        [TestCase("MealProvider", "Menu successfully retrieved")]
        [TestCase("User", "Menu successfully retrieved")]
        public void GetMenuByIdHappyPathAllRolesTest(
            string roleValue,
            string message)
        {
            role = Roles.SetRole(roleValue);
            MenuResponseDto response = ClientProxy.Menu.GetMenuById(_menuId, role);
            Assert.That(response.message.Equals(message));
            AssertHelper.AssertAllMenuParameters(response, _menu);
        }
        
        [Test]
        [TestCase(0, "The selected id is invalid.")]
        [TestCase(-1, "The selected id is invalid.")]
        [TestCase(99999, "Menu not found")]
        public void GetMenuNegativeAdminTest(
           int id,
           string message)
        {
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Menu.GetMenuById(id, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }
    }
}

