﻿namespace Domain.Entities
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive {  get; set; }
    }
}