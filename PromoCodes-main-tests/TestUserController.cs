using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromoCodes_main.Application.Models;
using PromoCodes_main.Controllers;
using Microsoft.AspNetCore.Mvc;
using PromoCodes_main.Application.Entities;

namespace PromoCodes_main_tests
{
    [TestClass]
    public class TestUserController
    {
        [TestMethod]
        public async void GetAllUsers_ShouldReturnAllUsers()
        {
            var users = GetTestUsers();
            var controller = new UserController(users);
            var result = controller.GetAllUsers() as IActionResult;
            Assert.AreEqual(users.Count, result);
        }



        private List<User> GetTestUsers()
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "Demo1", FirstName = "Demo1", LastName = "Demo1", Password = "xyz" });
            users.Add(new User { Id = 2, Username = "Demo2", FirstName = "Demo1", LastName = "Demo1", Password = "xyz" });
            users.Add(new User { Id = 3, Username = "Demo3", FirstName = "Demo1", LastName = "Demo1", Password = "xyz" });
            users.Add(new User { Id = 4, Username = "Demo4", FirstName = "Demo1", LastName = "Demo1", Password = "xyz" });
            return users;
        }
    }
}