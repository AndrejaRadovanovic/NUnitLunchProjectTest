using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.ResponseDto;

namespace NUnitLunchProject.Tests.UserTests.CheckinTests
{
	[TestFixture]
	public class DeleteCheckinUserTest
	{
        private CheckinDto _checkin;
        private int _checkinId;
        private Role role = Role.User;
        private CheckinResponseDto _createdCheckin;

        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _checkin = TestHelper.CreateCheckin();
            _createdCheckin = ClientProxy.Checkins.CreateCheckin(_checkin, role);
            _checkinId = _createdCheckin.data.id;
        }
        [TearDown]
        public void TearDown()
        {
            role = Role.Admin;
            if (_checkinId != -1)
            {
                ClientProxy.Checkins.DeleteCheckin(_checkinId, role);
            }
        }

        [Test]
        public void DeleteCheckinUserNegativeTest()
        {
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Checkins.DeleteCheckin(_checkinId, role));

            Assert.IsTrue(ex.Message.Contains("You are not authorized to access this route"));
        }

        [Test]
        public void DeleteUserCheckinByAdminTest()
        {
            role = Role.Admin;
            ClientProxy.Checkins.DeleteCheckin(_checkinId, role);

            Exception ex = Assert.Throws<Exception>(() =>
                 ClientProxy.Checkins.GetCheckinById(_checkinId, role));

            Assert.IsTrue(ex.Message.Contains("The selected id is invalid."));
            _checkinId = -1;
        }
    }
}
