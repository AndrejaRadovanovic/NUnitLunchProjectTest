using System;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;

namespace NUnitLunchProject.Models.ResponseDto.MenuResponseDto
{
	public class MenuDataDto
	{
        public int id { get; set; }
        public string date { get; set; }
        public MealDataDto[] items { get; set; }
    }
}

