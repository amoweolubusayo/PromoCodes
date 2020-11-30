using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromoCodes_main.Application.Entities;
using PromoCodes_main.Application.Models;
using PromoCodes_main.Controllers;
using PromoCodes_main.Services;

namespace PromoCodes_main_tests
{
    [TestClass]
    public class TestUserController
    {
        UserService u = new UserService();

        [TestMethod]
        public void GetAllUsers_ShouldReturnAllUsers()
        {

            var users = GetTestUsers();
            var result = u.GetAll();
            Assert.AreEqual(users.Count, result.ToList().Count);
        }

        private List<User> GetTestUsers()
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "Demo1", FirstName = "Demo1", LastName = "Demo1", Password = "xyz" });
            users.Add(new User { Id = 2, Username = "Demo2", FirstName = "Demo1", LastName = "Demo1", Password = "xyz" });
            return users;
        }
    }
}