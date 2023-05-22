using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;

namespace NUnitLunchProject.Tests.UserTests.MenuTests
{
    [TestFixture]
	public class DeleteMenuUserTest
	{
        private MealDto _meal;
        private int _mealId;
        private MenuDto _menu;
        private int _menuId;
        private Role role = Role.User;
        private MenuResponseDto _createdMenu;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _meal = TestHelper.CreateMeal();
            _mealId = ClientProxy.Meals.CreateMeal(_meal, Role.Admin).data.id;
            _menu = TestHelper.CreateMenu(_mealId);
            _createdMenu = ClientProxy.Menu.CreateMenu(_menu, Role.Admin);
            _menuId = _createdMenu.data.id;

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
        public void DeleteMenuUserNegativeTest()
        {
            
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.DeleteMenu(_menuId, role));

            Assert.IsTrue(ex.Message.Contains("You are not authorized to access this route."));
        }
    }
}

