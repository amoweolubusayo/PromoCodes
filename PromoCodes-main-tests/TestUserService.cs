using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromoCodes_main.Application.Models;
using PromoCodes_main.Controllers;
using Microsoft.AspNetCore.Mvc;
using PromoCodes_main.Application.Entities;
using PromoCodes_main.Services;
namespace PromoCodes_main_tests
{
    [TestClass]
    public class TestUserService
    {
        [TestMethod]
        public void UserService_CheckImplementationOfGettingAllUsers()
        {

            IUserService userService = new UserService();
            var result = userService.GetAll();
            Assert.AreEqual("Test", result.ElementAt(0).FirstName);
        }
    }
}