using System.Data;
using NUnitLunchProject.Helper;
using NUnitLunchProject.Models;
using NUnitLunchProject.Models.CalendarDtoModels;
using NUnitLunchProject.Models.CheckinDtoModels;
using NUnitLunchProject.Models.MealsDtoModels;
using NUnitLunchProject.Models.MenuDtoModels;
using NUnitLunchProject.Models.ResponseDto;
using NUnitLunchProject.Models.ResponseDto.CalendarMoreDatesResponseDto;
using NUnitLunchProject.Models.ResponseDto.CalendarResponseDto;
using NUnitLunchProject.Models.ResponseDto.MealsResponseDto;
using NUnitLunchProject.Models.ResponseDto.MenuResponseDto;
using RestSharp;

namespace NUnitLunchProject.ClientProxy
{

    public static class Checkins
    {
        public static CheckinResponseDto CreateCheckin(CheckinDto checkin, Role role)
        {
            var request = new RestRequest("/checkins", Method.Post);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddJsonBody(checkin);
            return Service.Execute<CheckinResponseDto>(request);
        }
        public static CheckinResponseDto UpdateCheckinById(int id, UpdateCheckinDto checkin, Role role)
        {
            var request = new RestRequest("/checkins/{id}", Method.Patch);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddJsonBody(checkin);
            return Service.Execute<CheckinResponseDto>(request);
        }
        public static CheckinResponseDto GetCheckinById(int id, Role role)
        {
            var request = new RestRequest("/checkins/{id}", Method.Get);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return Service.Execute<CheckinResponseDto>(request);
        }

        public static CheckinResponseListDto GetCheckins(Role role, string query)
        {
            var request = new RestRequest("/checkins", Method.Get);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("filter[{query}]", query, ParameterType.UrlSegment);
            /*request.AddParameter("userId", userId, ParameterType.UrlSegment);
            request.AddParameter("date", date, ParameterType.UrlSegment);
            request.AddParameter("answer", answer, ParameterType.UrlSegment);
            request.AddParameter("sortField", sortField, ParameterType.UrlSegment);
            request.AddParameter("sortType", sortType, ParameterType.UrlSegment);
            request.AddParameter("page", page, ParameterType.UrlSegment);
            request.AddParameter("size", size, ParameterType.UrlSegment);*/
            return Service.Execute<CheckinResponseListDto>(request);
        }

        public static string DeleteCheckin(int id, Role role)
        {
            var request = new RestRequest("/checkins/{id}", Method.Delete);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return Service.Execute(request);
        }
    }
    public static class Meals
    {
        public static MealResponseDto CreateMeal(MealDto meal, Role role) {
            var request = new RestRequest("/meals", Method.Post);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddJsonBody(meal);
            return Service.Execute<MealResponseDto>(request);
        }
        public static string DeleteMeal(int id, Role role)
        {
            var request = new RestRequest("/meals/{id}", Method.Delete);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return Service.Execute(request);
        }
        public static MealResponseDto UpdateMealById(int id, MealDto meal, Role role)
        {
            var request = new RestRequest("/meals/{id}", Method.Put);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddJsonBody(meal);
            return Service.Execute<MealResponseDto>(request);
        }
        public static MealResponseDto GetMealById(int id, Role role)
        {
            var request = new RestRequest("/meals/{id}", Method.Get);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return Service.Execute<MealResponseDto>(request);
        }
        public static MealResponseListDto GetMeals(Role role)
        {
            var request = new RestRequest("/meals", Method.Get);
            request = Roles.ReturnRequestWithToken(request, role);
            
            return Service.Execute<MealResponseListDto>(request);
        }
    }
    public static class Menu {
        public static MenuResponseDto CreateMenu(MenuDto menu, Role role)
        {
            var request = new RestRequest("/menus", Method.Post);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddJsonBody(menu);
            return Service.Execute<MenuResponseDto>(request);
        }
        public static MenuResponseDto UpdateMenuById(int id, MenuDto menu, Role role)
        {
            var request = new RestRequest("/menus/{id}", Method.Put);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            request.AddJsonBody(menu);
            return Service.Execute<MenuResponseDto>(request);
        }
        public static string DeleteMenu(int id, Role role)
        {
            var request = new RestRequest("/menus/{id}", Method.Delete);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return Service.Execute(request);
        }
        public static MenuResponseDto GetMenuById(int id, Role role)
        {
            var request = new RestRequest("/menus/{id}", Method.Get);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return Service.Execute<MenuResponseDto>(request);
        }
        public static MenuResponseListDto GetMenus(Role role)
        {
            var request = new RestRequest("/menus", Method.Get);
            request = Roles.ReturnRequestWithToken(request, role);

            return Service.Execute<MenuResponseListDto>(request);
        }
    }

    public static class Calendar {
        public static CalendarResponseDto GetCalendarByDate(string date, Role role) {
            var request = new RestRequest("/calendar/{date}", Method.Get);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddParameter("date", date, ParameterType.UrlSegment);
            return Service.Execute<CalendarResponseDto>(request);
        }
        public static CalendarMoreDatesResponseDto GetCalendar(CalendarDatesDto dates, Role role)
        {
            var request = new RestRequest("/calendar", Method.Get);
            request = Roles.ReturnRequestWithToken(request, role);
            request.AddJsonBody(dates);
            return Service.Execute<CalendarMoreDatesResponseDto>(request);
        }
    }

}

