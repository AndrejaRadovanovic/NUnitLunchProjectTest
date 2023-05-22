using System;
namespace NUnitLunchProject.Models.ResponseDto.CheckinsResponseDto
{
	public class CheckinsByUserResponseDto
	{
        public int id { get; set; }
        public int user_id { get; set; }
        public string date { get; set; }
        public string answer { get; set; }
        
    }
}

