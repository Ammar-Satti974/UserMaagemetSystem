
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Models;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Nest;
using System.Net.Mail;
using System.Text;
using System;
using Microsoft.AspNetCore.Http;

namespace StudentManagementSystem.Controllers
{
    public class UsersController : Controller
    {


        private readonly DataContext _db;

        public UsersController(DataContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var users = _db.Users.ToList();
            return View(users);
        }

        public IActionResult Dashboard()
        {
            var users = _db.Users.Count();
            ViewData["UserRegistered"] = users;
            return View(users);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Users usr)
        {
            {
                if (ModelState.IsValid)
                {
                    if (!_db.Users.Any(u => u.Username == usr.Username || u.Email == usr.Email))
                    {
                        string to = usr.Email; //To address    
                        string from = "ammarsatti755@gmail.com"; //From address    
                        MailMessage message = new MailMessage(from, to);
                        string mailbody = CreateRandomPassword(10);
                        message.Subject = "Student Management System";
                        message.Body = mailbody;
                        message.BodyEncoding = Encoding.UTF8;
                        message.IsBodyHtml = true;
                        SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                        System.Net.NetworkCredential basicCredential1 = new
                        System.Net.NetworkCredential("ammarsatti755@gmail.com", "vtisjcobeoiqizso");
                        client.EnableSsl = true;
                        client.UseDefaultCredentials = false;
                        client.Credentials = basicCredential1;
                        try
                        {
                            client.Send(message);
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        usr.ConfirmPassword = "";
                        usr.Password = mailbody;
                        usr.Bit = 1;
                        _db.Users.Add(usr);
                        _db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User Alraedy Exist!");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Some Error Occured while adding a new User!");
                }

            }

            return View(usr);

        }
        private static string CreateRandomPassword(int length = 10)
        {

            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login usrs)
        {
            if (ModelState.IsValid)
            {
                var obj = _db.Users.Where(u => u.Username.Equals(usrs.Username) && u.Password.Equals(usrs.Password)).FirstOrDefault();
                if (obj != null)
                {
                    CookieOptions cookies = new CookieOptions();
                    cookies.Expires = new DateTimeOffset(DateTime.Now.AddDays(1));
                    HttpContext.Response.Cookies.Append("UserId", obj.UserId.ToString(), cookies);

                    if (obj.Bit == 1)
                    {
                        return RedirectToAction("UpdatePassword", "Users");
                    }
                    else
                    {
                        return RedirectToAction("Dashboard", "Users");
                    }
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password.");
            }
            return View(usrs);
        }

        public IActionResult Delete(int id)
        {

            var usr = _db.Users.SingleOrDefault(u => u.UserId.Equals(id));
            _db.Users.Remove(usr);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Users users = _db.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault();
            return View(users);
        }
        [HttpPost]
        public IActionResult Edit(Users users, int id)
        {
            _db.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault().Username = users.Username;
            _db.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault().Email = users.Email;
            _db.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault().Password = users.Password;
            _db.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault().ConfirmPassword = users.ConfirmPassword;
            _db.Users.Where(u => u.UserId.Equals(id)).FirstOrDefault().UserId = users.UserId;
            _db.SaveChanges();
            return RedirectToAction("Index");

        }


        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UpdatePassword(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                var UserId = HttpContext.Request.Cookies["UserId"].ToString();

                var user = _db.Users.Where(u => u.UserId==Convert.ToInt32(UserId)).FirstOrDefault();
                if (user != null)
                {
                    user.Password = changePassword.Password;
                    user.ConfirmPassword = changePassword.ConfirmPassword;
                    _db.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    user.Bit = 0;
                    _db.SaveChanges();
                    return RedirectToAction("Login", "Users");
                }
            }
            else
            {
                ModelState.AddModelError("", "Some error occured while updating password!");
            }
            return View();

        }
        public IActionResult LogOut()
        {
            return RedirectToAction("Login", "Users");
        }
    }
}
