using System;
using NUnitLunchProject.Models.ResponseDto.CalendarResponseDto;

namespace NUnitLunchProject.Models.ResponseDto.CalendarMoreDatesResponseDto
{
	public class CheckinMenuCalendarMoreDatesDto
	{
		public UserAnswerDto[] user_answer { get; set; }
		public CheckinsCalculatorDto checkins { get; set; }
        public MenuCalendarMoreDatesDto menu { get; set; }
		public bool allergens_match { get; set; }
    }
}

