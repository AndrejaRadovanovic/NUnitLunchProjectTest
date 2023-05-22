using System;
using NUnitLunchProject.Models.ResponseDto.CheckinsResponseDto;

namespace NUnitLunchProject.Models.ResponseDto
{
	public class CheckinResponseListDto
	{
        public string message { get; set; }
        public CheckinsUserResponseDto[] data { get; set; }
    }
}

