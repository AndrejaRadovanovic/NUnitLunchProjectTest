using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.ResponseDto;

namespace NUnitLunchProject.Tests.MealProviderTests.CheckinTests
{
	[TestFixture]
	public class GetCheckinByIdMealProviderTest
	{
        private CheckinDto _checkin;
        private int _checkinId;
        private Role role = Role.MealProvider;
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
        [TestCase("Admin", "User checkin successfully retrieved")]
        [TestCase("User", "User checkin successfully retrieved")]
        [TestCase("MealProvider", "User checkin successfully retrieved")]
        public void GetMealProviderCheckinPositiveTest(
            string roleValue,
            string message)
        {
            role = Roles.SetRole(roleValue);
            CheckinResponseDto getResponseCheckin = ClientProxy.Checkins.GetCheckinById(_checkinId, role);
            Assert.That(getResponseCheckin.message.Equals(message));
            AssertHelper.AssertAllCheckinParameters(getResponseCheckin, _checkin);
            AssertHelper.AssertAllCheckinResponseParameters(getResponseCheckin, createResponseCheckin);
        }

        [Test]
        [TestCase(0, "The selected id is invalid.")]
        [TestCase(-1, "The selected id is invalid.")]
        [TestCase(16, "You aren't authorized to perform this action")]//Different hub user checkin
        public void GetMealProviderCheckinNegativeTest(
            int id,
            string message)
        {
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Checkins.GetCheckinById(id, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }
    }
}

