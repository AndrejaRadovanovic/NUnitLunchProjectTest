using System;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.ResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.CheckinTests
{
	[TestFixture]
	public class DeleteCheckinAdminTest
	{
        private CheckinDto _checkin;
        private int _checkinId;
        private Role role = Role.Admin;
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
        public void DeleteCheckinHappyPathAdminTest()       
        {        
            ClientProxy.Checkins.DeleteCheckin(_checkinId, role);

            Exception ex = Assert.Throws<Exception>(() =>
                 ClientProxy.Checkins.GetCheckinById(_checkinId, role));

            Assert.IsTrue(ex.Message.Contains("The selected id is invalid."));
            _checkinId = -1;
        }

        [Test]
        [TestCase(0, "The selected id is invalid.")]
        [TestCase(-1, "The selected id is invalid.")]
        [TestCase(999999, "The selected id is invalid.")]
        [TestCase(16, "You aren't authorized to perform this action")]//Different hub user checkin
        public void DeleteAdminCheckinNegativeTest(
            int id,
            string message)
        {
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Checkins.DeleteCheckin(id, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }

        [Test]
        [TestCase("User", "You are not authorized to access this route")]
        [TestCase("MealProvider", "You are not authorized to access this route")]
        public void DeleteAdminCheckinAuthorizationTest(
            string roleValue,
            string message)
        {
            role = Roles.SetRole(roleValue);
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Checkins.DeleteCheckin(_checkinId, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }
        [Test]
        public void DeleteDeletedCheckinTest()
        {
            ClientProxy.Checkins.DeleteCheckin(_checkinId, role);
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Checkins.DeleteCheckin(_checkinId, role));

            Assert.IsTrue(ex.Message.Contains("The selected id is invalid."));
            _checkinId = -1;
        }
    }
}

