using System;
namespace NUnitLunchProject.Models.ResponseDto.CalendarResponseDto
{
    public class MenuCalendarDto
    {
        public int id { get; set; }
        public MenuMealsDto[] meals { get; set;}
    }
}

