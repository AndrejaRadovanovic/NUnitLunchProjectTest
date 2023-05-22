using System;
namespace NUnitLunchProject.Models.MealsDtoModels
{
	public class MealDto
	{
        public string name { get; set; }
        public string description { get; set; }
        public int meal_type_id { get; set; }
        public int[] allergen_ids { get; set; }
    }
}

