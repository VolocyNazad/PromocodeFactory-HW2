﻿using System.Collections.Generic;

namespace PromoCodeFactory.WebHost.Models
{
    public class EmployeeUpdateModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public List<RoleItemResponse> Roles { get; set; }

        public int AppliedPromocodesCount { get; set; }
    }
}