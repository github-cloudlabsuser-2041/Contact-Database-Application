using NUnit.Framework;
using Moq;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;

namespace CRUD_application_2.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController _controller;
        private List<User> _userList;

        [SetUp]
        public void Setup()
        {
            _userList = new List<User>
            {
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" }
            };

            _controller = new UserController { ControllerContext = new ControllerContext() };
            UserController.userlist = _userList;
        }

        [Test]
        public void Index_ReturnsCorrectViewWithCorrectModel()
        {
            var result = _controller.Index() as ViewResult;

            Assert.AreEqual(_userList, result.Model);
        }

        [Test]
        public void Details_ReturnsCorrectViewWithCorrectModel_WhenUserExists()
        {
            var result = _controller.Details(1) as ViewResult;

            Assert.AreEqual(_userList[0], result.Model);
        }

        [Test]
        public void Details_ReturnsHttpNotFound_WhenUserDoesNotExist()
        {
            var result = _controller.Details(3);

            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }

        [Test]
        public void Create_AddsNewUserAndRedirectsToIndex_WhenModelStateIsValid()
        {
            var newUser = new User { Name = "Test User 3", Email = "test3@example.com" };

            var result = _controller.Create(newUser) as RedirectToRouteResult;

            Assert.AreEqual(3, UserController.userlist.Count);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Create_ReturnsCreateViewWithUserModel_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("error", "some error");

            var newUser = new User { Name = "Test User 3", Email = "test3@example.com" };

            var result = _controller.Create(newUser) as ViewResult;

            Assert.AreEqual(newUser, result.Model);
        }

        [Test]
        public void Edit_UpdatesUserAndRedirectsToDetails_WhenUserExists()
        {
            var updatedUser = new User { Name = "Updated User", Email = "updated@example.com" };

            var result = _controller.Edit(1, updatedUser) as RedirectToRouteResult;

            Assert.AreEqual("Updated User", UserController.userlist[0].Name);
            Assert.AreEqual("Details", result.RouteValues["action"]);
        }

        [Test]
        public void Edit_ReturnsHttpNotFound_WhenUserDoesNotExist()
        {
            var updatedUser = new User { Name = "Updated User", Email = "updated@example.com" };

            var result = _controller.Edit(3, updatedUser);

            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }

        [Test]
        public void Delete_RemovesUserAndRedirectsToIndex_WhenUserExists()
        {
            var result = _controller.Delete(1, null) as RedirectToRouteResult;

            Assert.AreEqual(1, UserController.userlist.Count);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Delete_ReturnsHttpNotFound_WhenUserDoesNotExist()
        {
            var result = _controller.Delete(3, null);

            Assert.IsInstanceOf<HttpNotFoundResult>(result);
        }
    }
}
