using System;
namespace NUnitLunchProject.Models.ResponseDto.MealsResponseDto
{
	public class MealResponseListDto
	{
        public string message { get; set; }
        public MealDataDto[] data { get; set; }
    }
}

