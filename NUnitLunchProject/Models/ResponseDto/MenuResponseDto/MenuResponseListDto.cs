using System;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;

namespace NUnitLunchProject.Models.ResponseDto.MenuResponseDto
{
	public class MenuResponseListDto
	{
        public string message { get; set; }
        public MenuDataDto[] data { get; set; }
    }
}

