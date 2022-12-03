﻿using PhloxAPI.Models;

namespace PhloxAPI.Services.AccountsService
{
    public interface IAccountsService
    {
        string RegisterUser(string firstname, string lastname, string username, string email, string password);
        string Login(string username, string password);
    }
}
