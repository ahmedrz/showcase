using EManifestServices.Attributes;
using EManifestServices.DAL;
using EManifestServices.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using EManifestServices.DAL.Enums;
using System.Data.Entity.Validation;

namespace EManifestServices.Controllers
{
    public class AccountController : Controller
    {
        EmanifestContext db;
        public AccountController()
        {
            db = new EmanifestContext();
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult Login(string url)
        {
            UserLoginModel model = new UserLoginModel();
            model.url = url;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var checkUser = db.Users.Include("Carriers")
                    .FirstOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
                if (checkUser == null)
                {
                    //return error message
                    ModelState.AddModelError("user", "User not found or not yet approved.");
                    return View(model);
                }
                else
                {
                    if (checkUser.IsActive == false || checkUser.Carriers?.IsActive == false)
                    {
                        //return error message
                        ModelState.AddModelError("user", "User Is Banned call support for more info");
                        return View(model);
                    }
                    //add session 
                    Session["User"] = AutoMapper.Mapper.Map<UserModel>(checkUser);
                    if (model.url != null)
                    {
                        return Redirect(model.url);
                    }
                    else
                        return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return View(model);
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!this.IsCaptchaValid("Captcha is not valid"))
            {
                TempData["Error"] = "Captcha is not valid";
                return View(model);
            }
            if (ModelState.IsValid)
            {
                var userRequest = new UserRequest
                {
                    RequestDate = DateTime.Now,
                    UserRequestId = Guid.NewGuid(),
                    CompanyName = model.CompanyName,
                    Info = model.CompanyInfo,
                    Email = model.Email,
                    Phone = model.PhoneNo,
                    Password = model.Password,
                    UserName = model.UserName,
                    UserEmail = model.UserEmail
                };
                var checkUser = db.UserRequest.FirstOrDefault(r => r.UserName == model.UserName);
                if (checkUser != null)
                {
                    TempData["Error"] = "Username not avaiable choose another username.";
                    return View(model);
                }
                db.UserRequest.Add(userRequest);
                await db.SaveChangesAsync();
                TempData["Success"] = "Request has been sent wait for the admin approval.";
                return RedirectToAction("Login");
            }
            else
            {
                return View(model);
            }
        }
        [MyAuthorize(UserTypes.Admin)]
        public ActionResult GetRequests([DataSourceRequest]DataSourceRequest request)
        {
            return Json(db.UserRequest
                .OrderByDescending(m => m.RequestDate)
                .Select(m => new RegisterModel
                {
                    Approved = m.Approved == true,
                    CompanyName = m.CompanyName,
                    Email = m.Email,
                    PhoneNo = m.Phone,
                    RequestDate = m.RequestDate,
                    UserName = m.UserName,
                    RequestId = m.UserRequestId
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [MyAuthorize(UserTypes.Admin)]
        public ActionResult Requests()
        {
            return View();
        }
        [HttpPost]
        [MyAuthorize(UserTypes.Admin)]
        public async Task<JsonResult> ApproveRequest(Guid requestId)
        {
            try
            {
                var currentUser = (UserModel)Session["User"];
                var request = await db.UserRequest.FindAsync(requestId);
                if (request.Approved == true)
                {
                    return Json(new { Success = true, Message = "Success" });
                }

                //save carrier 
                var checkCarrier = db.Carriers.Include("ApiClients").FirstOrDefault(c => c.CarrierName == request.CompanyName);
                if (checkCarrier == null)
                {
                    checkCarrier = new Carriers
                    {
                        CarrierId = Guid.NewGuid(),
                        CarrierName = request.CompanyName,
                        Email = request.Email,
                        Phone = request.Phone,
                        Info = request.Info,
                        IsActive = true
                    };
                    db.Carriers.Add(checkCarrier);
                }
                //save user 
                var checkUser = db.Users.FirstOrDefault(c => c.UserName == request.UserName);
                if (checkUser == null)
                {
                    var newUser = new Users
                    {
                        UserId = Guid.NewGuid(),
                        UserName = request.UserName,
                        Password = request.Password,
                        Email = request.UserEmail,
                        IsActive = true,
                        CarrierId = checkCarrier.CarrierId,
                        UserType = 2
                    };
                    db.Users.Add(newUser);
                }
                else
                {
                    return Json(new { Success = false, Message = "Username not available choose another username." });
                }
                request.Approved = true;
                //add api client for the carrier
                if (!checkCarrier.ApiClients.Any())
                {
                    var apiName = checkCarrier.CarrierName.Length > 20 ? checkCarrier.CarrierName.Substring(0, 20) : checkCarrier.CarrierName;
                    var newApiClient = new ApiClients
                    {
                        ApiClientId = Guid.NewGuid(),
                        ApiClientName = apiName,
                        ApiClientPassword = "ClientPassword",
                        IsActive = true,
                        CarrierId = checkCarrier.CarrierId,
                        Role = "IQManClient"
                    };
                    db.ApiClients.Add(newApiClient);
                }
                //save carrier users
                await db.SaveChangesAsync();

                return Json(new { Success = true, Message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    Success = false,
                    ex.Message
                });
            }
        }
        [MyAuthorize]
        public ActionResult GetCarriersCombo(string text)
        {
            var query = db.Carriers.AsQueryable();
            if (text != null)
            {
                query = query.Where(c => c.CarrierName.Contains(text));
            }
            return Json(query
                .Select(c => new CarrierModel
                {
                    CarrierId = c.CarrierId,
                    CarrierName = c.CarrierName,
                    IsActive = c.IsActive == true
                }), JsonRequestBehavior.AllowGet);
        }

        [MyAuthorize(UserTypes.Admin)]
        public ActionResult GetUsers([DataSourceRequest]DataSourceRequest request)
        {
            return Json(db.Users.Include("Carriers").Where(m => m.UserId != new Guid("6bd107f3-75a6-4c28-b2c0-2f75a2db29af"))
                .ToList()
                .Select(u => new UserModel
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Password = u.Password,
                    IsActive = u.IsActive == true,
                    CarrierId = u.CarrierId,
                    CarrierName = u.Carriers?.CarrierName,
                    UserType = u.UserType,
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [MyAuthorize(UserTypes.Admin)]
        public ActionResult Users()
        {
            var userTypes = new UserTypesSource().UserTypes;
            ViewData["UserTypesSource"] = userTypes;
            return View();
        }

        [MyAuthorize(UserTypes.Admin)]
        public ActionResult GetCarriers([DataSourceRequest]DataSourceRequest request)
        {
            return Json(db.Carriers
                .Select(c => new CarrierModel
                {
                    CarrierId = c.CarrierId,
                    CarrierName = c.CarrierName,
                    Email = c.Email,
                    Phone = c.Phone,
                    IsActive = c.IsActive == true
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [MyAuthorize(UserTypes.Admin)]
        public ActionResult Carriers()
        {
            return View();
        }
        [HttpGet]
        [MyAuthorize(UserTypes.Admin)]
        public async Task<ActionResult> CarrierData(Guid? id)
        {
            CarrierModel model = new CarrierModel();
            if (id != null)
            {
                var dbCarrier = await db.Carriers.FindAsync(id);
                if (dbCarrier == null)
                {
                    TempData["Error"] = "Carrier not found";
                }
                else
                {
                    model.CarrierId = dbCarrier.CarrierId;
                    model.CarrierName = dbCarrier.CarrierName;
                    model.IsActive = dbCarrier.IsActive == true;
                    model.Email = dbCarrier.Email;
                    model.Phone = dbCarrier.Phone;
                    model.Info = dbCarrier.Info;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize(UserTypes.Admin)]
        public ActionResult CarrierData(CarrierModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Carriers carrier;
            if (model.CarrierId == Guid.Empty)
            {
                carrier = new Carriers
                {
                    CarrierId = Guid.NewGuid(),
                    CarrierName = model.CarrierName,
                    Email = model.Email,
                    Phone = model.Phone,
                    IsActive = model.IsActive,
                    Info = model.Info,
                };
                db.Carriers.Add(carrier);
            }
            else
            {
                carrier = db.Carriers.Find(model.CarrierId);
                if (carrier != null)
                {
                    carrier.CarrierName = model.CarrierName;
                    carrier.Email = model.Email;
                    carrier.Phone = model.Phone;
                    carrier.Info = model.Info;
                    carrier.IsActive = model.IsActive;

                }
            }
            db.SaveChanges();
            TempData["Success"] = "Saving done.";
            return RedirectToAction("Carriers");

        }
        [HttpGet]
        [MyAuthorize(UserTypes.Admin)]
        public async Task<ActionResult> UserData(Guid? id)
        {
            var userTypes = new UserTypesSource().UserTypes;
            ViewData["UserTypesSource"] = userTypes;
            UserModel model = new UserModel();
            if (id != null)
            {
                var dbUser = await db.Users.FindAsync(id);
                if (dbUser == null)
                {
                    TempData["Error"] = "User not found";
                }
                else
                {
                    model.UserId = dbUser.UserId;
                    model.UserName = dbUser.UserName;
                    model.Email = dbUser.Email;
                    model.IsActive = dbUser.IsActive == true;
                    model.CarrierId = dbUser.CarrierId;
                    model.UserType = dbUser.UserType;


                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize(UserTypes.Admin)]
        public ActionResult UserData(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                var userTypes = new UserTypesSource().UserTypes;
                ViewData["UserTypesSource"] = userTypes;
                return View(model);
            }
            Users user;
            if (model.UserId == Guid.Empty)
            {
                user = new Users
                {
                    UserId = Guid.NewGuid(),
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    IsActive = model.IsActive,
                    CarrierId = model.CarrierId,
                    UserType = model.UserType

                };
                db.Users.Add(user);
            }
            else
            {
                user = db.Users.Find(model.UserId);
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    if (model.Password != null)
                    {
                        user.Password = model.Password;
                    }
                    user.IsActive = model.IsActive;
                    user.CarrierId = model.CarrierId;
                    user.UserType = model.UserType;

                }
            }
            db.SaveChanges();
            TempData["Success"] = "Saving done.";
            return RedirectToAction("Users");

        }

        [HttpGet]
        [MyAuthorize(UserTypes.Admin)]
        public async Task<ActionResult> RequestData(Guid? id)
        {
            ApproveRequestModel model = new ApproveRequestModel();
            if (id != null)
            {
                var dbRequest = await db.UserRequest.FindAsync(id);
                if (dbRequest == null)
                {
                    TempData["Error"] = "Request not found";
                    return RedirectToAction("Requests");
                }
                else
                {
                    model.RequestDate = dbRequest.RequestDate;
                    model.RequestId = dbRequest.UserRequestId;
                    model.CompanyName = dbRequest.CompanyName;
                    model.Email = dbRequest.Email;
                    model.PhoneNo = dbRequest.Phone;
                    model.CompanyInfo = dbRequest.Info;
                    model.UserName = dbRequest.UserName;
                    model.UserEmail = dbRequest.UserEmail;
                    model.Approved = dbRequest.Approved == true;
                    return View(model);
                }
            }
            TempData["Error"] = "Request not found";
            return RedirectToAction("Requests");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize(UserTypes.Admin)]
        public async Task<ActionResult> RequestData(ApproveRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                //var currentUser = (Users)Session["User"];
                var request = await db.UserRequest.FindAsync(model.RequestId);
                if (request.Approved == true)
                {
                    TempData["Success"] = "Request already approved.";
                    return RedirectToAction("Requests");
                }

                //save carrier 
                if (model.CarrierId == null)
                {
                    var checkCarrier = db.Carriers.FirstOrDefault(c => c.CarrierName == request.CompanyName);
                    if (checkCarrier == null)
                    {
                        checkCarrier = new Carriers
                        {
                            CarrierId = Guid.NewGuid(),
                            CarrierName = request.CompanyName,
                            Email = request.Email,
                            Phone = request.Phone,
                            Info = request.Info,
                            IsActive = true,

                        };
                        db.Carriers.Add(checkCarrier);
                        model.CarrierId = checkCarrier.CarrierId;
                    }
                    //add api client for the carrier
                    if (!checkCarrier.ApiClients.Any())
                    {
                        var apiName = checkCarrier.CarrierName.Length > 20 ? checkCarrier.CarrierName.Substring(0, 20) : checkCarrier.CarrierName;
                        var newApiClient = new ApiClients
                        {
                            ApiClientId = Guid.NewGuid(),
                            ApiClientName = apiName,
                            ApiClientPassword = "ClientPassword",
                            IsActive = true,
                            CarrierId = checkCarrier.CarrierId,
                            Role = "IQManClient"
                        };
                        db.ApiClients.Add(newApiClient);
                    }
                }

                //save user 
                var checkUser = db.Users.FirstOrDefault(c => c.UserName == request.UserName);
                if (checkUser == null)
                {
                    var newUser = new Users
                    {
                        UserId = Guid.NewGuid(),
                        UserName = request.UserName,
                        Email = request.UserEmail,
                        Password = request.Password,
                        IsActive = true,
                        CarrierId = model.CarrierId,
                        UserType = 2
                    };
                    db.Users.Add(newUser);
                }
                else
                {
                    TempData["Error"] = "Username not available choose another one";
                    return RedirectToAction("Requests");
                }
                request.Approved = true;
                //save carrier users
                await db.SaveChangesAsync();
                TempData["Success"] = "Request approved.";
                return RedirectToAction("Requests");
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                TempData["Error"] = exceptionMessage;
                return RedirectToAction("Requests");

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message; ;
                return RedirectToAction("Requests");
            }


        }
        //api clients
        [MyAuthorize(UserTypes.Admin)]
        public ActionResult ApiClients()
        {
            return View();
        }
        [MyAuthorize(UserTypes.Admin)]
        public ActionResult GetClients([DataSourceRequest]DataSourceRequest request)
        {
            var clientId = Guid.Parse("42061E62-13AD-45F3-9427-BE8651DB13F4");
            return Json(db.ApiClients
                .Where(c => c.ApiClientId != clientId)
                .Select(c => new ApiClientModel
                {
                    ApiClientId = c.ApiClientId,
                    ApiClientName = c.ApiClientName,
                    ApiClientPassword = c.ApiClientPassword,
                    Role = c.Role,
                    IsActive = c.IsActive == true
                }).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [MyAuthorize(UserTypes.Admin)]
        public async Task<ActionResult> ClientData(Guid? id)
        {
            ApiClientModel model = new ApiClientModel();
            if (id != null)
            {
                var dbClient = await db.ApiClients.FindAsync(id);
                if (dbClient == null)
                {
                    TempData["Error"] = "ApiClient not found";
                }
                else
                {
                    model = AutoMapper.Mapper.Map<ApiClientModel>(dbClient);
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize(UserTypes.Admin)]
        public ActionResult clientData(ApiClientModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ApiClients client;
            if (model.ApiClientId == Guid.Empty)
            {
                client = new ApiClients
                {
                    ApiClientId = Guid.NewGuid(),
                    ApiClientName = model.ApiClientName,
                    ApiClientPassword = model.ApiClientPassword,
                    Role = model.Role,
                    IsActive = model.IsActive,
                    CarrierId = model.CarrierId,

                };
                db.ApiClients.Add(client);
            }
            else
            {
                client = db.ApiClients.Find(model.ApiClientId);
                if (client != null)
                {
                    client.ApiClientName = model.ApiClientName;
                    if (model.ApiClientPassword != null)
                    {
                        client.ApiClientPassword = model.ApiClientPassword;
                    }
                    client.Role = model.Role;
                    client.IsActive = model.IsActive;
                    client.CarrierId = model.CarrierId;

                }
            }
            db.SaveChanges();
            TempData["Success"] = "Saving done.";
            return RedirectToAction("ApiClients");

        }

        public async Task<ActionResult> DeleteApiClient(Guid? id)
        {
            try
            {
                var apiClient = await db.ApiClients.FindAsync(id);
                if (apiClient != null)
                {
                    db.ApiClients.Remove(apiClient);
                    await db.SaveChangesAsync();
                }
                return Json(new { Success = true, Message = "Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }


        [MyAuthorize]
        public ActionResult Chat()
        {
            return View();
        }

        [MyAuthorize]
        public ActionResult UserProfile()
        {
            var currentUser = Session["User"] as UserModel;
            var profile = new ProfileModel
            {
                UserId = currentUser.UserId,
                UserName = currentUser.UserName,
                Email = currentUser.Email
            };
            return View(profile);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MyAuthorize]
        public ActionResult UserProfile(ProfileModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (EmanifestContext db = new EmanifestContext())
            {
                var currentUser = Session["User"] as UserModel;
                if (model.NewPassword != null && model.OldPassword == null)
                {
                    ModelState.AddModelError("OldPassword", "Enter old password.");
                    return View(model);
                }
                if (model.OldPassword != null)
                {
                    if (model.OldPassword != currentUser.Password)
                    {
                        ModelState.AddModelError("OldPassword", "Old password is not correct.");
                        return View(model);
                    }
                }
                var dbUser = db.Users.Find(model.UserId);
                dbUser.UserName = model.UserName;
                dbUser.Email = model.Email;
                if (model.NewPassword != null)
                {
                    dbUser.Password = model.NewPassword;
                }
                db.SaveChanges();
                TempData["Success"] = "User info updated successfully.";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}