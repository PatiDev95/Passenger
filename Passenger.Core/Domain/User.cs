﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace Passenger.Core.Domain
{
    public class User
    {

        public Guid Id { get; protected set; }
        public string Email { get; protected set; }

        public string Password { get; protected set; }

        public string Salt { get; protected set; }

        public string UserName { get; protected set; }

        public string FullName { get; protected set; }

        public string Role { get; protected set; }

        public DateTime CreateAt { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        private static readonly Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        protected User()
        {
        }

        public User (Guid userId, string email, string username, string password, string salt, string fullname, string role)
        {
            Id = userId;
            Salt = salt;
            CreateAt = DateTime.UtcNow;
            SetPassword(password);
            SetEmail(email);
            SetUserName(username);
            SetFullName(fullname);
            Role = role;
        }

        private void SetEmail(string email)
        {
            if (email==null || email == "" || email == " ")
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "Email can not be empty.");
            }

            if (!EmailRegex.IsMatch(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail, "The format of the email address is not valid.");
               
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }


        private void SetPassword(string password)
        {
            if (password == null || password == "" || password == " ")
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can not be empty.");
            }

            if (password.Length < 8)
            {
                throw new DomainException(ErrorCodes.InvalidPassword, "Password can not contain less than 8 characters.");
            }

            Password = password;
        }

        private void SetUserName(string username)
        {
            if (username == null || username == "" || username == " ")
            {
                throw new DomainException(ErrorCodes.InvalidUsername, "You didn't enter a name.");
            }

            //if (!NameRegex.IsMatch(username))
            //{
            //     throw new Exception("Username is invalid.");
            //}
            UserName = username;
            UpdatedAt = DateTime.UtcNow;
        }

        private void SetFullName(string fullname)
        {
            if (fullname == null)
            {
                throw new Exception("You didn't enter your last name.");
            }
            else
            {
                FullName = fullname;
            }
        }
    }
}
