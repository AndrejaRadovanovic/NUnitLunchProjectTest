using System;
using NUnitLunchProject.Models.ResponseDto.AllergensResponseDto;

namespace NUnitLunchProject.Models.ResponseDto.MealsResponseDto
{
	public class MealDataDto
	{
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public MealTypeDto meal_type { get; set; }
        public HubDto hub { get; set; }
        public AllergensDataDto[] allergens { get; set; }

    }
}

