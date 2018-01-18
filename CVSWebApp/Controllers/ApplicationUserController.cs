﻿using System;
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
    public class ApplicationUserController : Controller
    {
        private readonly ApplicationDbContext _context;


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;




        public ApplicationUserController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<ApplicationUserController> logger,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        // GET: ApplicationUser
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationUser.ToListAsync());
        }

        // GET: ApplicationUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: ApplicationUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,MobileNo,ManagerId,CompanyId,Active,OutletId,Id,UserName,Password,PhoneNumber")] ApplicationUserViewModel applicationUser)
        {
            //if (ModelState.IsValid)
            //{
            //    _context.Add(applicationUser);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(applicationUser);


       
    


                ApplicationUser _user = new ApplicationUser()
                {
                    FirstName = applicationUser.FirstName,
                    LastName = applicationUser.LastName,
                    MobileNo = applicationUser.MobileNo,
                    ManagerId = applicationUser.ManagerId,
                    CompanyId = applicationUser.CompanyId,
                    Active = applicationUser.Active,
                    OutletId = applicationUser.OutletId,
                    Id = applicationUser.Id,
                    UserName = applicationUser.UserName,
                    Email = applicationUser.UserName
                };


                var result = await _userManager.CreateAsync(_user, applicationUser.Password);
                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(_user, isPersistent: false);
                    return RedirectToAction(nameof(Index));
                }
        
            



            return View(applicationUser);
        }

        // GET: ApplicationUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);

            if (applicationUser == null)
            {
                return NotFound();
            }


            ApplicationUserViewModel _user = new ApplicationUserViewModel()
            {
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                MobileNo = applicationUser.MobileNo,
                ManagerId = applicationUser.ManagerId,
                CompanyId = applicationUser.CompanyId,
                Active = applicationUser.Active,
                OutletId = applicationUser.OutletId,
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                Email = applicationUser.UserName
            };

            return View(_user);
        }

        // POST: ApplicationUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,MobileNo,ManagerId,CompanyId,Active,OutletId,Id,UserName,Password")] ApplicationUserViewModel applicationUser)
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
            //return View(applicationUser);



            // ------------------- NEW CODE

            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            //if (!ModelState.IsValid)
            //{
            //    return View(applicationUser);
            //}
            var user = await _userManager.FindByEmailAsync(applicationUser.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                //return RedirectToAction(nameof(ResetPasswordConfirmation));
                return View(applicationUser);
            }
            var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;

            var result = await _userManager.ResetPasswordAsync(user, token, applicationUser.Password);
            if (result.Succeeded)
            {
                try
                {

                    user.FirstName = applicationUser.FirstName;
                    user.LastName = applicationUser.LastName;
                    user.MobileNo = applicationUser.MobileNo;
                    user.ManagerId = applicationUser.ManagerId;
                    user.CompanyId = applicationUser.CompanyId;
                    user.Active = applicationUser.Active;
                    user.OutletId = applicationUser.OutletId;

                    user.UserName = applicationUser.UserName;
                    user.Email = applicationUser.UserName;
                    
                    _context.Update(user);
                    await _context.SaveChangesAsync();
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
       
            return View();



        }

        // GET: ApplicationUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationUserExists(int id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
