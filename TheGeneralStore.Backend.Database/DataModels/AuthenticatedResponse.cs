﻿namespace TheGeneralStore.Backend.Database.DataModels
{
    public class AuthenticatedResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
