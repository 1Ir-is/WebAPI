﻿namespace CarRental_BE.Models.Auth
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string ResetKey { get; set; }
        public string NewPassword { get; set; }
    }
}
