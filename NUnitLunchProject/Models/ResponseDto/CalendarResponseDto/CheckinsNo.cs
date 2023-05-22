using System;
namespace NUnitLunchProject.Models.ResponseDto.CalendarResponseDto
{
	public class CheckinsNo
	{
        public int count { get; set; }
        public UserCheckinResponseDto[] users { get; set; }
    }
}

