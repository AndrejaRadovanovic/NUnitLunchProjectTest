using System;
using NUnitLunchProject.Models.ResponseDto.AllergensResponseDto;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;

namespace NUnitLunchProject.Models.ResponseDto.CalendarResponseDto
{
	public class MenuMealsDto
	{
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public MealTypeDto meal_type { get; set; }
        public AllergensDataDto[] allergens { get; set; }
    }
}

