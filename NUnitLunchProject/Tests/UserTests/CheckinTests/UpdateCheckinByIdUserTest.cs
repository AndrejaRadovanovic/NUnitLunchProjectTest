using System;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.ResponseDto;

namespace NUnitLunchProject.Tests.UserTests.CheckinTests
{
    [TestFixture]
	public class UpdateCheckinByIdUserTest
	{
        private CheckinDto _checkin;
        private int _checkinId;
        private Role role = Role.User;
        private CheckinResponseDto _createdCheckin;
        private UpdateCheckinDto _updateCheckinDto = new UpdateCheckinDto();
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
        [TestCase("answer", "yes")]
        [TestCase("answer", "no")]
        public void UpdateCheckinByIdHappyPathUserTest(
           string parameter,
           string value
           )
        {
            _updateCheckinDto.answer = value;

            CheckinResponseDto response = ClientProxy.Checkins.UpdateCheckinById(_checkinId, _updateCheckinDto, role);

            Assert.That(response.message.Equals("User checkin successfully updated"));
            Assert.That(response.data.answer.Equals(_updateCheckinDto.answer));
        }
        [Test]
        [TestCase("answer", "", "The answer field is required.")]
        [TestCase("answer", null, "The answer field is required.")]
        [TestCase("answer", "something", "The selected answer is invalid.")]
        public void UpdateCheckinByIdNegativeUserTest(
            string parameter,
            string value,
            string message
            )
        {
            _updateCheckinDto.answer = value;
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Checkins.UpdateCheckinById(_checkinId, _updateCheckinDto, role));

            Assert.IsTrue(ex.Message.Contains(message));
        }

        [Test]
        public void UpdateUserCheckinByAdminTest()
        {
            _updateCheckinDto.answer = "no";
            role = Role.Admin;
            CheckinResponseDto response = ClientProxy.Checkins.UpdateCheckinById(_checkinId, _updateCheckinDto, role);

            Assert.That(response.message.Equals("User checkin successfully updated"));
            Assert.That(response.data.answer.Equals(_updateCheckinDto.answer));
        }

        [Test]
        public void UpdateUserCheckinByMealProviderTest()
        {
            _updateCheckinDto.answer = "no";
            role = Role.MealProvider;
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Checkins.UpdateCheckinById(_checkinId, _updateCheckinDto, role));

            Assert.IsTrue(ex.Message.Contains("You aren't authorized to perform this action"));
        }
    }

}

