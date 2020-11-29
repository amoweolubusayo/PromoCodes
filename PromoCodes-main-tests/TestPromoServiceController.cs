using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromoCodes_main.Application.Models;
using Microsoft.AspNetCore.Mvc;
using PromoCodes_main.Controllers;
using System.Threading.Tasks;
using PromoCodes_main.Application.Entities;

namespace PromoCodes_main_tests
{
    [TestClass]
    public class TestPromoServiceController
    {
        [TestMethod]
        public async void GetAllPromoServices_ShouldReturnAllPromoServices()
        {
            var promoServices = GetTestPromoServices();
            var controller = new PromoServiceController(promoServices);
            var result = await controller.GetAll() as IActionResult;
            Assert.AreEqual(promoServices.Count, result);
        }

        [TestMethod]
        public async void GetPromoServices_ShouldReturnCorrectPromoService()
        {
            var promoServices = GetTestPromoServices();
            var controller = new PromoServiceController(promoServices);

            var result = await controller.SearchServiceByName("Demo2") as IActionResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(promoServices[1].RefName, result);
        }

        private List<TemppData> GetTestPromoServices()
        {
            var promoServices = new List<TemppData>();
            promoServices.Add(new TemppData { Id = 1, RefName = "Demo1", Codes = "Demo1" });
            promoServices.Add(new TemppData { Id = 2, RefName = "Demo2", Codes = "Demo1" });
            promoServices.Add(new TemppData { Id = 3, RefName = "Demo3", Codes = "Demo1" });
            promoServices.Add(new TemppData { Id = 4, RefName = "Demo4", Codes = "Demo1" });
            return promoServices;
        }
    }
}