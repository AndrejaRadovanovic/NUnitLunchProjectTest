using System;
namespace NUnitLunchProject.Models.ResponseDto
{
	public class CheckinResponseDto
	{
		public string statusCode { get; set; }
		public string message { get; set; }
		public CheckinDataDto data { get; set; }
	}
}

