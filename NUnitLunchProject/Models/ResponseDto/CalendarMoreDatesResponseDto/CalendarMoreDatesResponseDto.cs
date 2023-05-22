using System;
using NUnitLunchProject.Models.ResponseDto.CalendarMoreDatesResponseDto;

namespace NUnitLunchProject.Models.ResponseDto.CalendarResponseDto
{
	public class CalendarMoreDatesResponseDto
	{
        public string message { get; set; }
        public CalendarMoreDatesDto data { get; set; }      
    }
}