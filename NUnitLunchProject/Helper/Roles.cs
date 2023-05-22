using System;
using System.Data;
using System.Reflection.Emit;
using RestSharp;

namespace NUnitLunchProject.Helper
{
    public enum Role
    {
        Admin,
        MealProvider,
        User
    }

    public class Roles
    {
        public static RestRequest ReturnRequestWithToken(RestRequest request, Role role) {
            switch (role)
            {
                case Role.Admin:
                    request = request.AddOrUpdateHeader("Authorization", "Bearer eyJpdiI6Ik9XdUM2Tm52R0l2TCtyNXE0M0pxWUE9PSIsInZhbHVlIjoiay9GRk5FU0hhd0lJYjg3ZXhWc2VScmo0YTI3OU0zbFVUNkgvanJWZ0s2Y2NsYzcyUnd2d0d1VFA2Nk9qdTloUXAxbHFrZW80dHhwU0ZyNUF0dlQveWJVUDJicWlLUmJodXU5TWVjcllXL1BrQzdmWXZxZFp3NHpydFRHL3MzSEpmbVpidjFQYkVoRnJpYmlHQVlCRGU4RXplMVBSeHpkYzVuZXk5M1FERUlFbXRlUHhFYW92OXJNeER0SDM3M3hxL0V2d1o1cXJwMWRPRG1laTErYjYwamt5Q2J5UjU2NTNQMkZZTlV5WjZucWZiYUhOUVlodmxNak9xQ3NlRnluYURLWkdRWE5wM0RpY2NGeGJkY05ZTFhBeEdqbmtGenJKQ2V1cjd2K3MxbWpQQ3Avc0xwR0NQemFvM0g5bjZhT1o4eDU4bzdGdEhzeFhlSEFlSzAyNDFnPT0iLCJtYWMiOiJlY2Y2MTY1YzhiNWVlY2NmNDAzNDY4ZDhlYjc1ODU3OGI5YTA4YjM0ZTEwZDBlNTk0ZjdhOTBjNzIzMjBmNDI3IiwidGFnIjoiIn0%3D");
                    break;
                case Role.MealProvider:
                    request = request.AddOrUpdateHeader("Authorization", "Bearer eyJpdiI6Imc3Y2xMK2hHWFc1bUM3T3FMS0JROEE9PSIsInZhbHVlIjoiTVl0ZkFQT3RiWXMzbVFJV0JUak85Z1pIUCszTVptdER2a0tOT2hIVXhoeXRjMHIvVVRyMndOUjM2MHpPWVZGRUMxb3g4eU1wUjduWUFlcWgzSnNmTktaY3o2SlFBV2VxMFd3aWVQUENJUFc4dEIwZU9HZkVnV0I1TWtkb0FQaStnSUFNeUhYWXBYeHRubXd3YVBVMDljWndOckQzczNRMStvMVo2cVd2Wmg3WkpPN1Exd05KYUgzNUtzcitEdnhGYXFuL3VmZngzMmdZUG8rZFhqZ2lOb2ZBVXlMU2RSMWx3bnViRmJBOFk0Q3U0S3FUMUNPby9OL0YzRzhjQ3R6dm9XOVVSUFdKMHdUcXVsU2dQOUxxNWpjWFN6cEdwR2pwQUd6NDk3cWhOVVo1aW5LYmNBWWhRWHJ4VVRlOWJvdDIrdThxRkpOVkZVb0lTcERCMVZBQU93PT0iLCJtYWMiOiJkNzVlOWM0NWUwNDE0Y2VkMjViYTAwY2VjNWIxNWM0MTQyY2I1MDYzYTMwY2I3YjUwZDc3YzZjOTRjMTVkOTg4IiwidGFnIjoiIn0%3D");
                    break;
                case Role.User:
                    request = request.AddOrUpdateHeader("Authorization", "Bearer eyJpdiI6IlFEbnUrUVFKOHd3S3liY2ZCSlJCb2c9PSIsInZhbHVlIjoiVFhvVmk2WlU5dmc1T0R1anVyeEdzZFIrZ1ErY3J2d3hSKzN3NVBZSndyZ2UxNXErTkR6SzNmVVJXdDhreVhyYnkyQ2FDTTNWYk1tZkE4eWovYzBsRzFSOHAzckljY0RsemlTRFNkb2xPZmFZWmFIbDk1N21qZ1R0TGpaN2xZUFlTNW83STlFMkRSS0h2ZnVDWEhCWE5vcGVHUmdjTlVaTWljdlpzbFA3RzZlNjE5UHZtUXlPTkE1TWJCNHVXQTNFTmNUSENXaU43KzFERVFhV2dMQzZuN09jZnlZNVE1MjY0dG1GRytSV2JWT3JpTitYR3Q5SHJHSjN1OXU5V2F1S1Vpdk5jaDRSa0xkNEZvbVJRZklESjlJdVRBczd6S0tma0p1QnF1QmlHaG1pT1cxNnJlMnVRdEMxcWhaSDhOVjAyY0xZMEpPRFdBZDVxU2xXQnRhbWJnPT0iLCJtYWMiOiIwZDRmYzQ3YmNhMmQ0OWVlMjA2MDlmYjY3NjMyNTJjODM4ODRkMzM2MmRjOTg4MjE4MGM1YmVkNmZkOGQ5M2NhIiwidGFnIjoiIn0%3D");
                    break;
                default: throw new ArgumentOutOfRangeException();             
            }
            return request;
        }
        public static Role SetRole(string roleValue)
        {
            Role role = Role.Admin;
            switch (roleValue)
            {
                case "Admin":
                    role = Role.Admin;
                    break;
                case "MealProvider":
                    role = Role.MealProvider;
                    break;
                case "User":
                    role = Role.User;
                    break;
            }
            return role;
        }
    }
}

