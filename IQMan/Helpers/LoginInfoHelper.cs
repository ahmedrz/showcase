using IQManClient.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQMan.Helpers
{
    public class LoginInfoHelper
    {
        EmanifestContext db;
        public LoginInfoHelper(EmanifestContext db)
        {
            this.db = db;
        }

        public void ProcessLoginInfo(Users onlineUser)
        {
            if (db == null)
            {
                throw new Exception("db context is null.");
            }
            var dbUser = db.Users.Include("Carriers.ApiClients").FirstOrDefault(u => u.UserId == onlineUser.UserId);
            if (dbUser != null)
            {
                dbUser.Password = onlineUser.Password;
                dbUser.Email = onlineUser.Email;
                dbUser.IsActive = onlineUser.IsActive;
                dbUser.CarrierId = onlineUser.CarrierId;
                if (onlineUser.CarrierId != null)
                {
                    var onlineCarrier = onlineUser.Carriers;
                    var dbCarrier = dbUser.Carriers;
                    if (dbCarrier != null)
                    {
                        dbCarrier.CarrierName = onlineCarrier.CarrierName;
                        dbCarrier.Email = onlineCarrier.Email;
                        dbCarrier.IsActive = onlineCarrier.IsActive;
                        dbCarrier.Phone = onlineCarrier.Phone;
                        dbCarrier.Info = onlineCarrier.Info;
                    }
                    else
                    {
                        dbCarrier = new Carriers
                        {
                            CarrierId = onlineCarrier.CarrierId,
                            CarrierName = onlineCarrier.CarrierName,
                            Email = onlineCarrier.Email,
                            Phone = onlineCarrier.Phone,
                            Info = onlineCarrier.Info,
                            IsActive = onlineCarrier.IsActive
                        };
                        db.Carriers.Add(dbCarrier);
                    }
                    if (onlineCarrier.ApiClients.Any())
                    {
                        var onlineClient = onlineCarrier.ApiClients.First();
                        var dbClient = dbCarrier.ApiClients.FirstOrDefault();
                        if (dbClient != null)
                        {
                            dbClient.ApiClientName = onlineClient.ApiClientName;
                            dbClient.ApiClientPassword = onlineClient.ApiClientPassword;
                            dbClient.IsActive = onlineClient.IsActive;
                            dbClient.Role = onlineClient.Role;
                            dbClient.CarrierId = dbCarrier.CarrierId;
                        }
                        else
                        {
                            dbClient = new ApiClients
                            {
                                ApiClientId = onlineClient.ApiClientId,
                                ApiClientName = onlineClient.ApiClientName,
                                ApiClientPassword = onlineClient.ApiClientPassword,
                                IsActive = onlineClient.IsActive,
                                Role = onlineClient.Role,
                                CarrierId = dbCarrier.CarrierId
                            };
                            db.ApiClients.Add(dbClient);
                        }
                    }
                    else
                    {
                        db.ApiClients.Where(c => c.CarrierId == dbCarrier.CarrierId).DeleteFromQuery();
                    }
                }
            }
            else
            {
                dbUser = new Users
                {
                    UserId = onlineUser.UserId,
                    UserName = onlineUser.UserName,
                    Password = onlineUser.Password,
                    Email = onlineUser.Email,
                    IsActive = onlineUser.IsActive,
                    CarrierId = onlineUser.CarrierId,
                };
                db.Users.Add(dbUser);
                if (onlineUser.CarrierId != null)
                {
                    var onlineCarrier = onlineUser.Carriers;
                    var dbCarrier = db.Carriers.Include("ApiClients").FirstOrDefault(c => c.CarrierId == onlineUser.CarrierId);
                    if (dbCarrier != null)
                    {
                        dbCarrier.CarrierName = onlineCarrier.CarrierName;
                        dbCarrier.Email = onlineCarrier.Email;
                        dbCarrier.IsActive = onlineCarrier.IsActive;
                        dbCarrier.Phone = onlineCarrier.Phone;
                        dbCarrier.Info = onlineCarrier.Info;
                    }
                    else
                    {
                        dbCarrier = new Carriers
                        {
                            CarrierId = onlineCarrier.CarrierId,
                            CarrierName = onlineCarrier.CarrierName,
                            Email = onlineCarrier.Email,
                            Phone = onlineCarrier.Phone,
                            Info = onlineCarrier.Info,
                            IsActive = onlineCarrier.IsActive
                        };
                        db.Carriers.Add(dbCarrier);
                    }
                    if (onlineCarrier.ApiClients.Any())
                    {
                        var onlineClient = onlineCarrier.ApiClients.First();
                        var dbClient = dbCarrier.ApiClients.FirstOrDefault();
                        if (dbClient != null)
                        {
                            dbClient.ApiClientName = onlineClient.ApiClientName;
                            dbClient.ApiClientPassword = onlineClient.ApiClientPassword;
                            dbClient.IsActive = onlineClient.IsActive;
                            dbClient.Role = onlineClient.Role;
                            dbClient.CarrierId = dbCarrier.CarrierId;
                        }
                        else
                        {
                            dbClient = new ApiClients
                            {
                                ApiClientId = onlineClient.ApiClientId,
                                ApiClientName = onlineClient.ApiClientName,
                                ApiClientPassword = onlineClient.ApiClientPassword,
                                IsActive = onlineClient.IsActive,
                                CarrierId = dbCarrier.CarrierId,
                                Role = onlineClient.Role,
                            };
                            db.ApiClients.Add(dbClient);
                        }
                    }
                    else
                    {
                        db.ApiClients.Where(c => c.CarrierId == dbCarrier.CarrierId).DeleteFromQuery();
                    }
                }
            }
            db.BulkSaveChanges();
        }
    }
}
