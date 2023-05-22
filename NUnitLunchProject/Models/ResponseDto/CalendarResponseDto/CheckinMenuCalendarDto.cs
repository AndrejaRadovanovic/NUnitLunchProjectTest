using System;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;

namespace NUnitLunchProject.Models.ResponseDto.CalendarResponseDto
{
	public class CheckinMenuCalendarDto
	{
        public CheckinsCalendarDto checkins { get; set; }
        public MenuCalendarDto[] menu { get; set; }
    }
}

