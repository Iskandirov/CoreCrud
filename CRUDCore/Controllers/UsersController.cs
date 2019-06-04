using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CRUDCore.DAL.Entities;
using CRUDCore.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EFContext _context;
        public UsersController(EFContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<UserItemViewModel> GetUsers()
        {
            Thread.Sleep(5000);
            var model = new List<UserItemViewModel>
            {
                new UserItemViewModel
                {
                    Id = 1,
                    Email ="jon@gg.ss",
                    Roles = new List<RoleItemViewModel>
                    {
                        new RoleItemViewModel {Id = 2,Name="Admin"}
                    }
                },
                new UserItemViewModel
                {
                    Id = 2,
                    Email ="bombelyk@gg.ss",
                    Roles = new List<RoleItemViewModel>
                    {
                        new RoleItemViewModel {Id = 2,Name="Admin"},
                        new RoleItemViewModel {Id = 3,Name="Mayson"}
                    }
                },
            };
            return model;

        }
    }
}