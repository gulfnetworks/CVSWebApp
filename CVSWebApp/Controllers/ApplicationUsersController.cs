using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVSWebApp.Data;
using CVSWebApp.Models;
using Microsoft.AspNetCore.Identity;
using CVSWebApp.Services;
using Microsoft.Extensions.Logging;

namespace CVSWebApp.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;


        public ApplicationUsersController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<ApplicationUsersController> logger,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

   

        private async Task<ApplicationUserViewModel> ConvertUserToViewModel(ApplicationUser applicationUser, string UserRole = "")
        {
            ApplicationUser _user = new ApplicationUser();
            IList<string> _roles = null;
            string userRole = "";

            if (UserRole == "") {
      
                _roles = await _userManager.GetRolesAsync(applicationUser);

                userRole = _roles.Count != 0 ? _roles.FirstOrDefault() : "";
                
            } else { userRole = UserRole; }
       

            ApplicationUserViewModel user = new ApplicationUserViewModel()
            {
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                MobileNo = applicationUser.MobileNo,
                ManagerId = applicationUser.ManagerId,
                CompanyId = applicationUser.CompanyId,
                Active = applicationUser.Active,
                UserOutletId = applicationUser.UserOutletId,
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                Email = applicationUser.UserName,
                UserRole = userRole,
                CompanyName = applicationUser.Company == null ? "" : applicationUser.Company.CompanyName
            };


            return user;
        }

        // GET: ApplicationUsers
        public async Task<IActionResult> Index()
        {

            var _users = new List<ApplicationUserViewModel>();

    

            var applicationDbContext =  await _context.Users.Include(a=> a.Company).ToListAsync();

            foreach (var _user in applicationDbContext)
            {
                var user = await ConvertUserToViewModel(_user);

                _users.Add(user);
            }



            return View( _users);
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.Company)
                .SingleOrDefaultAsync(m => m.Id == id);



            if (applicationUser == null)
            {
                return NotFound();
            }

            var user = await ConvertUserToViewModel(applicationUser);
   

            return View(user);
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId");
            ViewData["ManagerId"] = new SelectList(_context.ApplicationUsers, "Id", "FirstName");
            ViewData["UserRole"] = new SelectList(new List<string>() { "Admin", "Manager", "Member" });
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,FirstName,LastName,MobileNo,ManagerId,CompanyId,Active,UserOutletId,Id,Email,Password,UserRole")] ApplicationUserViewModel applicationUser)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(applicationUser);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", applicationUser.CompanyId);
            //return View(applicationUser);





            var _user = await ConvertUserToViewModel(applicationUser);


            var result = await _userManager.CreateAsync(_user, applicationUser.Password);
            if (result.Succeeded)
            {
                //await _signInManager.SignInAsync(_user, isPersistent: false);

                await _userManager.AddToRoleAsync(_user, applicationUser.UserRole);
                return RedirectToAction(nameof(Index));
            }



            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", applicationUser.CompanyId);
            ViewData["ManagerId"] = new SelectList(_context.ApplicationUsers, "Id", "FirstName", applicationUser.ManagerId);
            ViewData["UserRole"] = new SelectList(new List<string>() { "Admin", "Manager", "Member" });
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            //if (applicationUser == null)
            //{
            //    return NotFound();
            //}
            //ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", applicationUser.CompanyId);
            //return View(applicationUser);



            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            var _user = await ConvertUserToViewModel(applicationUser);


            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName", applicationUser.CompanyId);
            ViewData["ManagerId"] = new SelectList(_context.ApplicationUsers, "Id", "FirstName", applicationUser.ManagerId);
            ViewData["UserRole"] = new SelectList(new List<string>() { "Admin", "Manager", "Member" });
            
            return View(_user);


        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserName,FirstName,LastName,MobileNo,ManagerId,CompanyId,Active,UserOutletId,Id,Email,Password,UserRole")] ApplicationUserViewModel applicationUser)
        {
            //if (id != applicationUser.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(applicationUser);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ApplicationUserExists(applicationUser.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyId", applicationUser.CompanyId);
            //return View(applicationUser);

            // NEW CODE -----------------------------------------------

            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            //if (!ModelState.IsValid)
            //{
            //    return View(applicationUser);
            //}
            if(applicationUser.UserName == null)
            {
               var tempUser = await _userManager.FindByIdAsync(applicationUser.Id.ToString());
               applicationUser.UserName = tempUser.UserName;
            }

            var user = await _userManager.FindByEmailAsync(applicationUser.UserName);
      


            if (user == null)
            {
                // Don't reveal that the user does not exist
                //return RedirectToAction(nameof(ResetPasswordConfirmation));
                return View(applicationUser);
            }
            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
            var roles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRoleAsync(user, "Admin");
            await _userManager.RemoveFromRoleAsync(user, "Member");
            await _userManager.RemoveFromRoleAsync(user, "Manager");

            user.FirstName = applicationUser.FirstName;
            user.LastName = applicationUser.LastName;
            user.MobileNo = applicationUser.MobileNo;
            user.ManagerId = applicationUser.ManagerId;
            user.CompanyId = applicationUser.CompanyId;
            user.Active = applicationUser.Active;
            user.UserOutletId = applicationUser.UserOutletId;

            user.UserName = applicationUser.UserName;
            user.Email = applicationUser.UserName;

            if (applicationUser.UserName != null && applicationUser.Password != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, token, applicationUser.Password);
                if (result.Succeeded)
                {
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();

                        await _userManager.AddToRoleAsync(user, applicationUser.UserRole);

                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ApplicationUserExists(applicationUser.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
     
                return RedirectToAction(nameof(Index));
            }
            else
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    await _userManager.AddToRoleAsync(user, applicationUser.UserRole);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }


            ViewData["CompanyId"] = new SelectList(_context.Companies, "CompanyId", "CompanyName", applicationUser.CompanyId);
            ViewData["ManagerId"] = new SelectList(_context.ApplicationUsers, "Id", "FirstName", applicationUser.ManagerId);

            ViewData["UserRole"] = new SelectList(new List<string>() { "Admin", "Manager", "Member" });
            return View();



        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUsers
                .Include(a => a.Company)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUsers.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(int id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
    }
}
