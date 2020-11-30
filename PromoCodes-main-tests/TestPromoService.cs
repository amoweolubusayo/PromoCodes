using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PromoCodes_main.Application.Entities;
using PromoCodes_main.Application.Models;
using PromoCodes_main.Application.Queries;
using PromoCodes_main.Controllers;
using PromoCodes_main.Services;

namespace PromoCodes_main_tests
{
    [TestClass]
    public class TestPromoService
    {

        private Mock<IMediator> Mediator;

        public TestPromoService()
        {
            Mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public void GetAllPromoServices_ShouldCheckAllPromoServices()
        {
            var controller = new PromoServiceController(Mediator.Object);
            var result = controller.GetAll();
            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetPromoServiceByName_ShouldCheckPromoService()
        {
            var controller = new PromoServiceController(Mediator.Object);
            var result = controller.SearchServiceByName("Appvision.com");
            Assert.IsNotNull(result);
        }
       
    }
}