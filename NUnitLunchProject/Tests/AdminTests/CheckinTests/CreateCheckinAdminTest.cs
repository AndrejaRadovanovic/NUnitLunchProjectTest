using System;
using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.ResponseDto;

namespace NUnitLunchProject.Tests.AdminTests.CheckinTests
{
    [TestFixture]
    public class CreateCheckinAdminTest
    {
        private CheckinDto _checkin;
        private int _checkinId;
        private Role role = Role.Admin;
        [SetUp]
        public void Setup()
        {
            ClientProxy.Service.Initialize(Helper.CommonFile.BASEURL);
            _checkin = TestHelper.CreateCheckin();
        }
        [TearDown]
        public void TearDown()
        {
            if (_checkinId != -1)
            {
                ClientProxy.Checkins.DeleteCheckin(_checkinId, role);
            }
        }

        [Test]
        [TestCase("answer", "yes")]
        [TestCase("answer", "no")]
        public void CreateCheckinHappyPathAdminTest(
            string parameter,
            string value
            )
        {
            SetPropertyValue(parameter, value, _checkin);
            CheckinResponseDto response = ClientProxy.Checkins.CreateCheckin(_checkin, role);
            _checkinId = response.data.id;
            Assert.That(response.message.Equals("User checkin created successfully"));
            AssertHelper.AssertAllCheckinParameters(response, _checkin);

            //CheckinResponseDto responseCheckinDto = ClientProxy.Checkins.GetCheckinById(response.data.id, role);
            //responseCheckinDto.data.date.Equals(response.data.date);
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
        public void CreateCheckinNegativeAdminTests(
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
        public void CreateCheckInDoubleSameDateAdmin()
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

            if (propertyInfo != null)
            {
                Type t = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                object safeValue = (value == null) ? null : Convert.ChangeType(value, t);
                propertyInfo.SetValue(checkin, safeValue, null);
            }
        }
    }
}

