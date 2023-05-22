using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.MenuTests
{
	[TestFixture]
	public class UpdateMenuAdminTest
	{
        private MealDto _meal;
        private int _mealId;
        private MealDto _mealNew;
        private int _mealNewId;
        private MenuDto _menu;
        private int _menuId;
        private Role role = Role.Admin;
        private MenuResponseDto _createdMenu;
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
            ClientProxy.Menu.DeleteMenu(_menuId, role);
            if (_mealId != 0)
            {
                ClientProxy.Meals.DeleteMeal(_mealId, role);
            }
            if (_mealNewId != 0)
            {
                ClientProxy.Meals.DeleteMeal(_mealNewId, role);
            }
        }
        [Test]
        public void UpdateMenuDatePositiveAdminTest()
        {
            _menu.date = DateTime.UtcNow.AddDays(2).ToString("yyyy-MM-dd");
            MenuResponseDto response = ClientProxy.Menu.UpdateMenuById(_menuId, _menu, role);
            Assert.That(response.message.Equals("Menu successfully updated"));
            AssertHelper.AssertAllMenuParameters(response, _menu);
        }
        [Test]
        public void UpdateMenuMealPositiveAdminTest()
        {
            _mealNew = TestHelper.CreateMeal();
            _mealNewId = ClientProxy.Meals.CreateMeal(_mealNew, role).data.id;
            _menu.meal_ids[0] = _mealNewId;
            MenuResponseDto response = ClientProxy.Menu.UpdateMenuById(_menuId, _menu, role);
            Assert.That(response.message.Equals("Menu successfully updated"));
            AssertHelper.AssertAllMenuParameters(response, _menu);
        }

        [TestCase("date", null, "The date field is required.")]
        [TestCase("date", "", "The date field is required.")]
        [Test]
        public void UpdateMenuWithoutDateNegativeAdminTest(
            string parameter,
            string value,
            string message)
        {
            _menu = SetPropertyValueHelper.SetMenuPropertyValue(parameter, value, _menu);
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.UpdateMenuById(_menuId ,_menu, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }
        [Test]
        public void UpdateMenuWithoutMealNegativeAdminTest()
        {
            _menu.meal_ids = new int[0];
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.UpdateMenuById(_menuId, _menu, role));

            Assert.IsTrue(ex.Message.Contains("The meal ids field is required."));
        }
        [Test]
        public void UpdateMenuWithInvalidDateNegativeAdminTest()
        {
            _menu.date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.UpdateMenuById(_menuId, _menu, role));

            Assert.IsTrue(ex.Message.Contains("The date must be a date after today."));
        }

        [Test]
        public void UpdateMenuWithInvalidMealIdNegativeAdminTest()
        {
            _menu.meal_ids[0] = 0;
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.UpdateMenuById(_menuId, _menu, role));

            Assert.IsTrue(ex.Message.Contains("The selected meal ids is invalid."));
        }
        [Test]
        public void UpdateMenuWithUnexistedMealIdNegativeAdminTest()
        {
            _menu.meal_ids[0] = _mealId + 1;
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.UpdateMenuById(_menuId, _menu, role));

            Assert.IsTrue(ex.Message.Contains("The selected meal ids is invalid."));
        }
    }
}

