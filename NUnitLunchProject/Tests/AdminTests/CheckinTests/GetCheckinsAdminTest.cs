using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.ResponseDto;
using NUnitLunchProject.Models.ResponseDto.CheckinsResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.CheckinTests
{
    [TestFixture]
	public class GetCheckinsAdminTest
	{
        private CheckinDto _checkin;
        private int _checkinId;
        private Role role = Role.Admin;
        private CheckinResponseDto createResponseCheckin;

        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _checkin = TestHelper.CreateCheckin();
            createResponseCheckin = ClientProxy.Checkins.CreateCheckin(_checkin, role);
            _checkinId = createResponseCheckin.data.id;
        }

        [TearDown]
        public void TearDown()
        {
            role = Role.Admin;
            ClientProxy.Checkins.DeleteCheckin(_checkinId, role);
        }

        [Test]
        [TestCase("Admin", 3, "User checkin successfully retrieved")]
        [TestCase("User", 3, "User checkin successfully retrieved")]
        [TestCase("MealProvider", 3, "User checkin successfully retrieved")]
        public void GetAdminCheckinPositiveTest(
            string roleValue,
            int adminId,
            string message)
        {
            role = Roles.SetRole(roleValue);
            CheckinResponseListDto getResponseCheckin = ClientProxy.Checkins.GetCheckins(role, "filter[checkins.answer]=yes");
            
            var testCheckins = getResponseCheckin.data.Where(x => x.id == adminId);
            Assert.NotNull(testCheckins);
            
            var checkin = testCheckins.FirstOrDefault().checkins.Where(c => c.id ==_checkinId);
            
            Assert.That(checkin.FirstOrDefault().id.Equals(_checkinId));
            Assert.That(checkin.FirstOrDefault().answer.Equals(_checkin.answer));

        }
    }
}

