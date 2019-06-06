using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using CRUDCore.DAL.Entities;
using CRUDCore.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRUDCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //[ApiController]
    public class UsersController : ControllerBase
    {
        private readonly EFContext _context;
        private readonly UserManager<DbUser> _userManager;
        private readonly RoleManager<DbRole> _roleManager;
        public UsersController(UserManager<DbUser> userManager,
         RoleManager<DbRole> roleManager, EFContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
                    Image = "https://ukr-space.com/wp-content/uploads/2017/04/Vintonyak-e1492516048779.jpg",
                    Age = 20,
                    Workplace = "PHP admin",
                    Roles = new List<RoleItemViewModel>
                    {
                        new RoleItemViewModel {Id = 2,Name="Admin"}
                    }
                },
                new UserItemViewModel
                {
                    Id = 2,
                    Email ="bombelyk@gg.ss",
                    Image = "http://protruskavets.org.ua/protrusk/wp-content/gallery/divchyna-zhovtnya-2012/1.jpg",
                    Age = 22,
                    Workplace = "Unity 3d game dev",
                    Roles = new List<RoleItemViewModel>
                    {
                        new RoleItemViewModel {Id = 2,Name="Admin"},
                        new RoleItemViewModel {Id = 3,Name="Mayson"}
                    }
                },
            };
            return model;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]UserItemViewModel model)
        {
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    DbUser dbUser = new DbUser
                    {
                        Email = model.Email,
                        Age = model.Age,
                        Phone = model.Phone,
                        WorkPlace = model.Workplace,
                        UserName = model.Email,
                    };
                    var result = await _userManager.CreateAsync(dbUser, "Qwerty1-");
                    if (result.Succeeded)
                    {
                        throw new Exception("Problem create role!");
                    }
                    foreach (var role in model.Roles)
                    {
                        var roleresult = await _roleManager.CreateAsync(new DbRole
                        {
                            Name = role.Name
                        });
                        if (!roleresult.Succeeded)
                        {
                            throw new Exception("Problem create role!");
                        }
                    }
                    scope.Complete();
                    return Ok(dbUser.Id);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

}

