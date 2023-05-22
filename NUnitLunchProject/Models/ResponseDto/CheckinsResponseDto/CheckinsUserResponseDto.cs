using System;
namespace NUnitLunchProject.Models.ResponseDto.CheckinsResponseDto
{
    public class CheckinsUserResponseDto
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string nickname { get; set; }
        public CheckinsByUserResponseDto[] checkins {get; set;}
    }
}

