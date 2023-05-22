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
	public class CreateMenuAdminTest
	{
        private MealDto _meal;
        private int _mealId;
        private MenuDto _menu;
        private int _menuId;
        private Role role = Role.Admin;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _meal = TestHelper.CreateMeal();
            _mealId = ClientProxy.Meals.CreateMeal(_meal, role).data.id;
            _menu = TestHelper.CreateMenu(_mealId);

        }
        [TearDown]
        public void TearDown()
        {
            ClientProxy.Menu.DeleteMenu(_menuId, role);
            if (_mealId != 0)
            {
                ClientProxy.Meals.DeleteMeal(_mealId, role);
            }       
        }
        [Test]
        public void CreateMenuHappyPathAdminTest() {
            MenuResponseDto response = ClientProxy.Menu.CreateMenu(_menu, role);
            _menuId = response.data.id;
            Assert.That(response.message.Equals("Meal is added to the menu successfully"));
            AssertHelper.AssertAllMenuParameters(response, _menu);
        }

        [TestCase("date", null, "The date field is required.")]
        [TestCase("date", "", "The date field is required.")]
        [Test]
        public void CreateMenuWithoutDateNegativeAdminTest(
            string parameter,
            string value,
            string message)
        {
            _menu = SetPropertyValueHelper.SetMenuPropertyValue(parameter, value, _menu);
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.CreateMenu(_menu, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }

        [Test]
        public void CreateMenuWithoutMealNegativeAdminTest()
        {
            _menu.meal_ids = new int[0];
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.CreateMenu(_menu, role));

            Assert.IsTrue(ex.Message.Contains("The meal ids field is required."));
        }

        [Test]
        public void CreateMenuWithInvalidDateNegativeAdminTest()
        {
            _menu.date = DateTime.UtcNow.ToString("yyyy-MM-dd");
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.CreateMenu(_menu, role));

            Assert.IsTrue(ex.Message.Contains("The date must be a date after today."));
        }

        [Test]
        public void CreateMenuWithInvalidMealIdNegativeAdminTest()
        {
            _menu.meal_ids[0] = 0;
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.CreateMenu(_menu, role));

            Assert.IsTrue(ex.Message.Contains("The selected meal ids is invalid."));
        }
        [Test]
        public void CreateMenuWithUnexistedMealIdNegativeAdminTest()
        {
            _menu.meal_ids[0] = _mealId+1;
            Exception ex = Assert.Throws<Exception>(() =>
                    ClientProxy.Menu.CreateMenu(_menu, role));

            Assert.IsTrue(ex.Message.Contains("The selected meal ids is invalid."));
        }
    }
}

