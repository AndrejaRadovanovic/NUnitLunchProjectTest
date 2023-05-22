using System;
using NUnitLunchProject.Models.ResponseDto.CheckinsResponseDto;

namespace NUnitLunchProject.Models.ResponseDto.CalendarResponseDto
{
	public class UserCheckinResponseDto
	{
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string answer { get; set; }
    }
}

