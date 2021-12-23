using OnlineTechnicalHiring.Models;
using OnlineTechnicalHiring.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Device.Location;


namespace OnlineTechnicalHiring.Controllers
{
    public class UserController : ApiController
    {
        UserBLL _bll = new UserBLL();
        TechnicianBll _techBLL = new TechnicianBll();
        [AuthorizeAttribute]
        [Authorize(Roles = "user")]
        [System.Web.Http.Route("api/User/List")]
        public List<UserViewModel> GetAll()
        {
            return _bll.ListAll();
        }
        public JsonResult GetUserList(string sortColumnName = "FullName", string sortOrder = "asc", int pageSize = 3, int currentPage = 1, string searchText = "")
        {
            List<UserViewModel> List = new List<UserViewModel>();
            int totalPage = 0;
            int totalRecord = 0;


            using (OnlineTechnicalProfessionalHiringSystemEntities dc = new OnlineTechnicalProfessionalHiringSystemEntities())
            {
                dc.Configuration.ProxyCreationEnabled = false;
                //var emp = dc.tblStudents.Select(a => a);
                var emp = _bll.ListAll().Select(a => a);

                //var emp = stuDB.ListAll().Select(x => new { Id = x.Student_Id, Name = x.Name, Email = x.Email, Gender = x.Gender, Phone = x.Phone ,Address=x.Address, YearBatchId=x.YearBatchId, YearBatch=x.tblYearBatch.YearBatchId, FacultyId=x.Faculty_Id, FacultyName=x.tblFaculty.Faculty_Name, SectionId=x.Section_Id, SectionName=x.tblSection.Sec_Name, SemesterId=x.Semester_Id, SemesterName=x.tblSemester.Semester_Name});

                //Search
                if (!string.IsNullOrEmpty(searchText))
                {
                    emp = emp.Where(x => x.FullName.Contains(searchText) || x.Email.Contains(searchText) || x.Gender.Contains(searchText) || x.PhoneNumber.Contains(searchText) || x.Address.Contains(searchText));
                }
                totalRecord = emp.Count();
                if (pageSize > 0)
                {
                    totalPage = totalRecord / pageSize + ((totalRecord % pageSize) > 0 ? 1 : 0);
                    List = emp.Skip(pageSize * (currentPage - 1)).Take(pageSize).ToList();
                }
                else
                {
                    List = emp.ToList();
                }
            }

            return new JsonResult
            {
                //Data = new { List = List, totalPage = totalPage, sortColumnName = sortColumnName, sortOrder = sortOrder, currentPage = currentPage},
                Data = new { List = List, totalRecord = totalRecord, totalPage = totalPage, sortColumnName = sortColumnName, sortOrder = sortOrder, currentPage = currentPage, pageSize = pageSize },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        public UserViewModel Get(int ID)
        {
            var user = _bll.ListAll().Find(t => t.UId.Equals(ID));
            return user;
        }
        //List Student Submitted Assignment By StudentId
        public List<UserViewModel> GetSearchUser(string search)
        {
            return _bll.SearchUser(search);
        }

        [System.Web.Http.Route("api/User/Register")]
        public HttpResponseMessage Post([Bind(Include = "Email,Model")] UserViewModel uvm)
        {
            OnlineTechnicalProfessionalHiringSystemEntities db = new OnlineTechnicalProfessionalHiringSystemEntities();
            string message = "User Added Successfully";
            bool isexists = _bll.EmailExists(uvm.Email, uvm.PhoneNumber);
            if (isexists)
            {
                ModelState.AddModelError("Email", "Email Already Exist");
                ModelState.AddModelError("PhoneNumber", "PhoneNumber Already Exist");
            }
            if (ModelState.IsValid)
            {
                _bll.Add(uvm);
                //stuDB.SendSMS();

                tblUser tb = db.tblUsers.Where(x => x.Email == uvm.Email).FirstOrDefault();
                if (tb != null)
                {
                    //_bll.SendEMail(tb.UId);
                }

                //tblStudent tb1 = db.tblStudents.Where(x => x.Phone == svm.Phone).FirstOrDefault();
                //if (tb1 != null)
                //{
                //    stuDB.SendSMS(tb1.Phone);
                //}

                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

        }


        public HttpResponseMessage Put([Bind(Include = "Email,Model")] UserViewModel uvm)
        {
            OnlineTechnicalProfessionalHiringSystemEntities db = new OnlineTechnicalProfessionalHiringSystemEntities();
            var user = _bll.ListAll().Find(t => t.UId.Equals(uvm.UId));
            string message = "User Updated Successfully";
            tblUser tb = db.tblUsers.Where(s => s.UId == uvm.UId).FirstOrDefault();
            bool isexists = _bll.EmailExists(uvm.Email, uvm.PhoneNumber);
            if (isexists)
            {
                if (tb.Email == uvm.Email)
                {
                    if (ModelState.IsValid)
                    {
                        _bll.Update(uvm);
                        return Request.CreateResponse(HttpStatusCode.OK, message);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email Already Exist");
                    ModelState.AddModelError("PhoneNumber", "PhoneNumber Already Exist");
                }
            }

            if (ModelState.IsValid)
            {
                _bll.Update(uvm);
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

        }

        public HttpResponseMessage Delete(int ID)
        {
            var user = _bll.ListAll().Find(t => t.UId.Equals(ID));
            string message = "User Deleted Successfully";
            string messagenodata = "Data Not found/ Might be Deleted or Removed";
            HttpResponseMessage response;
            if (user == null)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent, messagenodata);
            }
            else
            {
                _bll.Delete(ID);
                response = Request.CreateResponse(HttpStatusCode.OK, message);
            }
            return response;
        }
        public class TechnicianDetails
        {
            public int Id { get; set; }

            public string Name { get; set; }
            public Double Latitude { get; set; }
            public Double Longitude { get; set; }
            public string Address { get; set; }
            public string Gender { get; set; }
            public string Phone { get; set; }
            public float DisatanceAway { get; set; }
            public string Technician { get; set; }
        }

        [System.Web.Http.Route("api/User/ShowAllTechnician")]
        [System.Web.Http.HttpGet]
        public List<TechnicianDetails> CalculateDistance()
        {
         
        ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var id = principal.Claims.ToList();
            //var Id = id[1];
            // int userId = Convert.ToInt32(Id.Value);
             int userId = 3;
            var user = _bll.ListAll().Find(t => t.UId.Equals(userId));
            GeoCoordinate pin1 = new GeoCoordinate((double)user.Latitude, (double)user.Longitude);
            var technicianList= _techBLL.ListAll();
            List<TechnicianDetails> lst = new List<TechnicianDetails>();
            foreach (var item in technicianList)
            {
                GeoCoordinate pin2 = new GeoCoordinate((double)item.Latitude, (double)item.Longitude);
                double distanceBetween = pin1.GetDistanceTo(pin2);
                if (distanceBetween <=5000)
                {
                    
                    lst.Add(new TechnicianDetails() {  Name = item.FullName,Latitude=(double)item.Latitude,Longitude=(double)item.Longitude , Gender=item.Gender==true? "Male":"Female", Phone = item.PhoneNumber, Address = item.Address, DisatanceAway=(float)distanceBetween/1000, Technician="Electrician" });

                }
            }
            return lst;
            //return new JsonResult
            //{
            //    Data = new { details = lst },
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //};
        }

        [System.Web.Http.Route("api/User/ShowElectricianNearBy")]
        [System.Web.Http.HttpGet]
        public List<TechnicianDetails> ShowElectrician()
        {

            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var id = principal.Claims.ToList();
            //var Id = id[1];
            //int userId = Convert.ToInt32(Id.Value);
            int userId = 3;
            var user = _bll.ListAll().Find(t => t.UId.Equals(userId));
            GeoCoordinate pin1 = new GeoCoordinate((double)user.Latitude, (double)user.Longitude);
            var electricianlist = _techBLL.ListAllElectrician();
            List<TechnicianDetails> lst = new List<TechnicianDetails>();
            foreach (var item in electricianlist)
            {
                GeoCoordinate pin2 = new GeoCoordinate((double)item.Latitude, (double)item.Longitude);
                double distanceBetween = pin1.GetDistanceTo(pin2);
                if (distanceBetween <= 5000)
                {

                    lst.Add(new TechnicianDetails() {Id=item.TID, Name = item.FullName, Latitude = (double)item.Latitude, Longitude = (double)item.Longitude, Gender = item.Gender == true ? "Male" : "Female", Phone = item.PhoneNumber, Address = item.Address, DisatanceAway = (float)distanceBetween / 1000, Technician = "Electrician" });

                }
            }
            return lst;
        }

        [System.Web.Http.Route("api/User/ShowPlumberNearBy")]
        [System.Web.Http.HttpGet]
        public List<TechnicianDetails> ShowPlumber()
        {

            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var id = principal.Claims.ToList();
            //var Id = id[1];
            //int userId = Convert.ToInt32(Id.Value);
            int userId = 3;
            var user = _bll.ListAll().Find(t => t.UId.Equals(userId));
            GeoCoordinate pin1 = new GeoCoordinate((double)user.Latitude, (double)user.Longitude);
            var plumberList = _techBLL.ListAllPlumber();
            List<TechnicianDetails> lst = new List<TechnicianDetails>();
            foreach (var item in plumberList)
            {
                GeoCoordinate pin2 = new GeoCoordinate((double)item.Latitude, (double)item.Longitude);
                double distanceBetween = pin1.GetDistanceTo(pin2);
                if (distanceBetween <= 5000)
                {

                    lst.Add(new TechnicianDetails() { Id=item.TID, Name = item.FullName, Latitude = (double)item.Latitude, Longitude = (double)item.Longitude, Gender = item.Gender == true ? "Male" : "Female", Phone = item.PhoneNumber, Address = item.Address, DisatanceAway = (float)distanceBetween / 1000, Technician = "Electrician" });

                }
            }
            return lst;          
        }

        [System.Web.Http.Route("api/User/GetUserDetails")]
        [System.Web.Http.HttpGet]
        public Users GetUserByToken()
        {

            ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var id = principal.Claims.ToList();
            var role = id[0].Value;
            var Id = id[1];
            int userId = Convert.ToInt32(Id.Value);
            Users u = new Users();
            if(role== "user")
            {
                var user = _bll.ListAll().Find(t => t.UId.Equals(userId));
                u.ID = user.UId;
                u.FullName = user.FullName;
                u.Email = user.Email;
                u.PhoneNumber = user.PhoneNumber;
                u.Gender = user.Gender;

                u.Photo = user.Photo;
                u.Role = "user";
            }
            else
            {
                var tech = _techBLL.ListAll().Find(t => t.TID.Equals(userId));
                u.ID = tech.TID;
                u.FullName = tech.FullName;
                u.Email = tech.Email;
                u.PhoneNumber = tech.PhoneNumber;
                if (tech.Gender == true)
                {
                    u.Gender = "Male";
                }
                else
                {
                    u.Gender = "Female";
                }
                u.Photo = tech.Photo;
                u.Role = "technicians";
            }
         
            return u;
        }

    }
}