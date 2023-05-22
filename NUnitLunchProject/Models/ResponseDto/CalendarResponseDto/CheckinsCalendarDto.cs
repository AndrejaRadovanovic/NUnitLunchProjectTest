using System;
using NUnitLunchProject.Models.ResponseDto.CheckinsResponseDto;

namespace NUnitLunchProject.Models.ResponseDto.CalendarResponseDto
{
	public class CheckinsCalendarDto
	{
        public CheckinsYes yes { get; set; }
        public CheckinsNo no { get; set; }
        public CheckinsUnanswered unanswered { get; set; }
    }
}

