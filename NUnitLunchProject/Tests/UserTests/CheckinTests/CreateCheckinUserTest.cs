using System;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.ResponseDto;

namespace NUnitLunchProject.Tests.UserTests.CheckinTests
{
    [TestFixture]
    public class CreateCheckinUserTest
    {
        private CheckinDto _checkin;
        private int _checkinId;
        private Role role = Role.User;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _checkin = TestHelper.CreateCheckin();
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
        public void CreateCheckinHappyPathUserTest(
            string parameter,
            string value
            )
        {
            SetPropertyValue(parameter, value, _checkin);
            CheckinResponseDto response = ClientProxy.Checkins.CreateCheckin(_checkin, role);
            _checkinId = response.data.id;
            Assert.That(response.message.Equals("User checkin created successfully"));
            AssertHelper.AssertAllCheckinParameters(response, _checkin);
        }

        [Test]
        [TestCase("answer", "", "The answer field is required.")]
        [TestCase("answer", null, "The answer field is required.")]
        [TestCase("answer", "something", "The selected answer is invalid.")]
        [TestCase("date", "", "The date field is required.")]
        [TestCase("date", null, "The date field is required.")]
        [TestCase("date", "2022-04-15", "The date must be a date after today.")]
        [TestCase("date", "15-04-2022", "The date does not match the format Y-m-d.")]
        [TestCase("date", "something", "The date does not match the format Y-m-d.")]
        public void CreateCheckinNegativeUserTests(
            string parameter,
            string value,
            string message
            )
        {
            SetPropertyValue(parameter, value, _checkin);
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Checkins.CreateCheckin(_checkin, role));

            Assert.IsTrue(ex.Message.Contains(message));
            _checkinId = -1;
        }

        [Test]
        public void CreateCheckInDoubleSameDateUser()
        {
            CheckinResponseDto response = ClientProxy.Checkins.CreateCheckin(_checkin, role);
            _checkinId = response.data.id;
            Exception ex = Assert.Throws<Exception>(() =>
                  ClientProxy.Checkins.CreateCheckin(_checkin, role));

            Assert.IsTrue(ex.Message.Contains("The date has already been taken."));
        }
        private static void SetPropertyValue(string parameter, object value, CheckinDto checkin)
        {
            PropertyInfo propertyInfo = checkin.GetType().GetProperty(parameter);

            propertyInfo.SetValue(checkin, value, null);
        }
    }
}

