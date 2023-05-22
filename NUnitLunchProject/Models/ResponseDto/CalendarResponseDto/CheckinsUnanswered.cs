using System;
namespace NUnitLunchProject.Models.ResponseDto.CalendarResponseDto
{
	public class CheckinsUnanswered
	{
        public int count { get; set; }
        public UserCheckinResponseDto[] users { get; set; }
    }
}

