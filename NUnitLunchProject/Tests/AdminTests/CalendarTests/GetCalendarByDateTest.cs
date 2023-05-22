using System;
using NUnitLunchProject.ClientProxy;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using NUnitLunchProject.Models.ResponseDto;
using NUnitLunchProject.Models.ResponseDto.CalendarResponseDto;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.CalendarTests
{
    [TestFixture]
	public class GetCalendarByDateTest
	{
        private MealDto _meal;
        private int _mealId;
        private MenuDto _menu;
        private int _menuId;
        private MenuResponseDto _createdMenu;
        private CheckinDto _checkin;
        private int _checkinId;
        private CheckinResponseDto _actualCheckin;
        private Role role = Role.Admin;
        private string dateTomorow = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd");
        
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _meal = TestHelper.CreateMeal();
            _mealId = ClientProxy.Meals.CreateMeal(_meal, role).data.id;
            _menu = TestHelper.CreateMenu(_mealId);
            _createdMenu = ClientProxy.Menu.CreateMenu(_menu, role);
            _menuId = _createdMenu.data.id;
            _checkin = TestHelper.CreateCheckin();
            _actualCheckin = ClientProxy.Checkins.CreateCheckin(_checkin, role);
            _checkinId = _actualCheckin.data.id;



        }
        [TearDown]
        public void TearDown()
        {
            role = Role.Admin;
            ClientProxy.Menu.DeleteMenu(_menuId, role);
            if (_mealId != 0)
            {
                ClientProxy.Meals.DeleteMeal(_mealId, role);
            }
            if (_checkinId != -1)
            {
                ClientProxy.Checkins.DeleteCheckin(_checkinId, role);
            }
        }
        [Test]
        [TestCase("Admin", "Single date for calendar successfully retrieved")]
        [TestCase("MealProvider", "Single date for calendar successfully retrieved")]
        [TestCase("User", "Single date for calendar successfully retrieved")]
        public void GetCalendarByDateHappyPathAllRolesTest(
            string roleValue,
            string message)
        {
            role = Roles.SetRole(roleValue);
            CalendarResponseDto response = ClientProxy.Calendar.GetCalendarByDate(dateTomorow, role);
            
            Assert.That(response.message.Equals(message));
            Assert.IsNotNull(response);

            var calendarCheckin = response.data.checkins.yes.users.Where(x => x.id == 3); //admin id

            Assert.That(calendarCheckin.FirstOrDefault().id.Equals(3)); // admin id
            Assert.That(calendarCheckin.FirstOrDefault().answer.Equals(_checkin.answer));

            var calendarMenu = response.data.menu.Where(c => c.id == _menuId);
            Assert.That(calendarMenu.FirstOrDefault().meals.FirstOrDefault().id.Equals(_mealId));
            Assert.That(calendarMenu.FirstOrDefault().meals.FirstOrDefault().name.Equals(_meal.name));
            Assert.That(calendarMenu.FirstOrDefault().meals.FirstOrDefault().meal_type.id.Equals(_meal.meal_type_id));

        }
    }
}

