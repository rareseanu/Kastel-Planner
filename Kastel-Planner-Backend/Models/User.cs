﻿using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kastel_Planner_Backend.Models
{
    public class User
    {
        public int Id { get; set; }
        public Nullable<int> PersonId { get; set; }

        [RegexValidator(@"^[a-z0-9\-_.@]*$")]
        public string Email { get; set; }

        [RegexValidator(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*?[#?!@$%^&*-])(?=\\S+$)\\S{8,12}$",
        MessageTemplate = "Password must contain at least one digit, a lower case letter, an upper case letter and a character.")]
        public string Password { get; set; }

        public virtual Person Person { get; set; }
    }
}