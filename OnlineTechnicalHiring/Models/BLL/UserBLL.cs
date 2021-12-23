using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace OnlineTechnicalHiring.Models.BLL
{
    public class UserBLL
    {
        OnlineTechnicalProfessionalHiringSystemEntities _db = new OnlineTechnicalProfessionalHiringSystemEntities();
        public List<UserViewModel> ListAll()
        {

            try
            {
                List<UserViewModel> lstUser = new List<UserViewModel>();
                var users = _db.tblUsers.ToList();
                foreach (var item in users)
                {
                    lstUser.Add(new UserViewModel() { UId = item.UId, FullName = item.FullName, Latitude=item.Latitude, Longitude=item.Longitude,  Gender = item.Gender, Email = item.Email, PhoneNumber = item.PhoneNumber, Address = item.Address, IsApproved = item.IsApproved, Photo = item.Photo });
                }
                return lstUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserViewModel GetUserById(int id)
        {

            try
            {
                UserViewModel user = ListAll().Where(a => a.UId == id).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Add(UserViewModel uvm)
        {
            try
            {
                tblUser tb = new tblUser();
                //var keyNew = Helper.GeneratePassword(10);
                //var password = Helper.EncodePassword(keyNew, keyNew);

                //objNewUser.VCode = keyNew;



                tb.FullName = uvm.FullName;
                tb.Gender = uvm.Gender;
                tb.Email = uvm.Email;
                tb.PhoneNumber = uvm.PhoneNumber;
                tb.Address = uvm.Address;
                tb.Password = uvm.Password;
                tb.IsApproved = true;
                tb.Latitude = uvm.Latitude;
                tb.Longitude = uvm.Longitude;
                tb.Photo = uvm.Photo;
                tb.Country = uvm.Country;
                tb.Province = uvm.Province;
                tb.PostalCode = uvm.PostalCode;
                tb.District = uvm.District;
                tb.Locality = uvm.Locality;

                _db.tblUsers.Add(tb);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(UserViewModel uvm)
        {
            try
            {
                tblUser tb = _db.tblUsers.Where(s => s.UId == uvm.UId).FirstOrDefault();

                tb.FullName = uvm.FullName;
                tb.Gender = uvm.Gender;
                tb.Email = uvm.Email;
                tb.PhoneNumber = uvm.PhoneNumber;
                tb.Address = uvm.Address;
                // tb.Password = uvm.Password;
                // tb.IsApproved = false;
                tb.Latitude = uvm.Latitude;
                tb.Longitude = uvm.Longitude;
                //tb.Photo = uvm.Photo;
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Delete(int id)
        {
            try
            {
                tblUser tb = _db.tblUsers.Where(s => s.UId == id).FirstOrDefault();
                _db.tblUsers.Remove(tb);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //for password random generation
        public static string CreateRandomPassword(int PasswordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }

        public int SendEMail(int id)
        {

            using (OnlineTechnicalProfessionalHiringSystemEntities db = new OnlineTechnicalProfessionalHiringSystemEntities())
            {
                tblUser tb = db.tblUsers.Where(x => x.UId == id).FirstOrDefault();


                try
                {

                    if (tb != null)
                    {
                        var fromAddress = new MailAddress("santoshmandal97788@gmail.com", "santoshmandal97788");
                        var toAddress = new MailAddress(tb.Email, "To Name");
                        const string fromPassword = "password goes here";
                        const string subject = "Quick Fix";
                        var userName = tb.FullName;
                        var Email = tb.Email;
                        var Password = tb.Password;
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                        };
                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = "Hi!" + " " + userName + " " + "Thankyou For Creating Account on Quick Fix." + Email + " " + "and Password: " + Password
                        })
                        {
                            smtp.Send(message);
                        }

                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {

                }
                return id;

            }
        }
        public bool EmailExists(string Email, string phone)
        {

            var isExist = _db.tblUsers.Where(s => s.Email == Email && s.PhoneNumber == phone).FirstOrDefault();
            if (isExist != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<UserViewModel> SearchUser(string search)
        {
            List<UserViewModel> lstUser = new List<UserViewModel>();
            if (search != null)
            {

                var users = _db.tblUsers.Where(s => s.FullName.StartsWith(search) || s.Address.Contains(search) || s.Gender.Contains(search) || s.PhoneNumber.Contains(search));
                foreach (var item in users)
                {
                    lstUser.Add(new UserViewModel() { UId = item.UId, FullName = item.FullName, Email = item.Email, Gender = item.Gender, PhoneNumber = item.PhoneNumber, Address = item.Address });
                }
                return lstUser;
            }
            else
            {
                var tb = _db.tblUsers.ToList();
                foreach (var item in tb)
                {
                    lstUser.Add(new UserViewModel() { UId = item.UId, FullName = item.FullName, Email = item.Email, Gender = item.Gender, PhoneNumber = item.PhoneNumber, Address = item.Address });
                }
                return lstUser;
            }
        }
        //For Geting Distance from one point to another i






        //public string SendSMS()
        //{
        //    using (AssignmentManagementSystemEntities db = new AssignmentManagementSystemEntities())
        //    {
        //        tblStudent tb = db.tblStudents.Where(x => x.Phone == Phone).FirstOrDefault();

        //        if (tb != null)
        //        {
        //            var Email = tb.Email;
        //            var Password = tb.Password;
        //            var Phoneno = tb.Phone;
        //            var studentname = tb.Name;
        //            String message = HttpUtility.UrlEncode("Hi!" + " " + studentname + " " + "Your Assignment Management System Account is Created. And Your Login Credential is Email: " + Email + " " + "and Password: " + Password);
        //            String message = HttpUtility.UrlEncode("This is your message");
        //            using (var wb = new WebClient())
        //            {
        //                byte[] response = wb.UploadValues("https://api.txtlocal.com/send/", new NameValueCollection()
        //                {
        //                {"apikey" , "aqKeunxeo+I-QlD7juMSdiPLjDtYSmjdt25bILbmSz"},
        //                {"numbers" , "+9779816815884"},
        //                {"message" , message},
        //                {"sender" , "ISMT AMS"}
        //                });
        //                string result = System.Text.Encoding.UTF8.GetString(response);
        //                return result;
        //            }

        //        }

        //    }
        //}

    }
}