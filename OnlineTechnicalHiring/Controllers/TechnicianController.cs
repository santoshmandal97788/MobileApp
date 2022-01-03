using OnlineTechnicalHiring.Models;
using OnlineTechnicalHiring.Models.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OnlineTechnicalHiring.Controllers
{
    public class TechnicianController : ApiController
    {

        TechnicianBll _bll = new TechnicianBll();
        //[AuthorizeAttribute]
        //[Authorize(Roles = "user")]
        [System.Web.Http.Route("api/Technician/GetAllElectrician")]
        public List<TechnicianViewModel> GetAllElectrician()
        {
            return _bll.ListAllElectrician();
        }
        [System.Web.Http.Route("api/Technician/GetElectricianbyId")]
        public TechnicianViewModel GetElectricianById(int id)
        {
            var tech= _bll.GetUserById(id);
            return tech;
        }
        [System.Web.Http.Route("api/Technician/GetAllPlumber")]
        public List<TechnicianViewModel> GetAllPlumber()
        {
            return _bll.ListAllPlumber();
        }

        [System.Web.Http.Route("api/Technician/GetPlumberbyId")]
        public TechnicianViewModel GetPlmberById(int id)
        {
            var tech = _bll.GetUserById(id);
            return tech;
        }
        [System.Web.Http.Route("api/Technician/Register")]
        public HttpResponseMessage Post()
        {
            //OnlineTechnicalProfessionalHiringSystemEntities db = new OnlineTechnicalProfessionalHiringSystemEntities();
            //string message = "User Added Successfully";
            //bool isexists = _bll.EmailExists(uvm.Email, uvm.PhoneNumber);
            //if (isexists)
            //{
            //    ModelState.AddModelError("Email", "Email Already Exist");
            //    ModelState.AddModelError("PhoneNumber", "PhoneNumber Already Exist");
            //}
            //if (ModelState.IsValid)
            //{
            //    _bll.Add(uvm);
            //    //stuDB.SendSMS();

            //    tblTechnician tb = db.tblTechnicians.Where(x => x.Email == uvm.Email).FirstOrDefault();
            //    if (tb != null)
            //    {
            //        //_bll.SendEMail(tb.UId);
            //    }

            //    //tblStudent tb1 = db.tblStudents.Where(x => x.Phone == svm.Phone).FirstOrDefault();
            //    //if (tb1 != null)
            //    //{
            //    //    stuDB.SendSMS(tb1.Phone);
            //    //}

            //    return Request.CreateResponse(HttpStatusCode.OK, message);
            //}
            //else
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            //}


            //Read the File data from Request.Form collection.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            //Convert the File data to Byte Array.
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            string message = "Technician Added Successfully";
            var FullName = HttpContext.Current.Request.Params["FullName"];
            var Gender = HttpContext.Current.Request.Params["Gender"];
            var Email = HttpContext.Current.Request.Params["Email"];
            var PhoneNumber = HttpContext.Current.Request.Params["PhoneNumber"];
            var Address = HttpContext.Current.Request.Params["Address"];
            var Password = HttpContext.Current.Request.Params["Password"];
            var Latitude = HttpContext.Current.Request.Params["Latitude"];
            var Longitude = HttpContext.Current.Request.Params["Latitude"];
            var TType= HttpContext.Current.Request.Params["TType"];
            //Insert the File to Database Table.
            OnlineTechnicalProfessionalHiringSystemEntities _db = new OnlineTechnicalProfessionalHiringSystemEntities();
            if (ModelState.IsValid)
            {
                tblTechnician tb = new tblTechnician();
                tb.FullName = FullName;
                tb.Gender = Gender;
                tb.Email = Email;
                tb.PhoneNumber = PhoneNumber;
                tb.Address = Address;
                tb.Password = Password;
                tb.TType = Convert.ToInt32(TType);
                tb.WorkCount = 0;
                tb.IsApproved = true;
                tb.Latitude = Convert.ToDecimal(Latitude);
                tb.Longitude = Convert.ToDecimal(Longitude);
                tb.Photo = bytes;
                _db.tblTechnicians.Add(tb);
                _db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }


        }

        [System.Web.Http.Route("api/Technician/Update")]
        public HttpResponseMessage Put()
        {
            //OnlineTechnicalProfessionalHiringSystemEntities db = new OnlineTechnicalProfessionalHiringSystemEntities();
            //var user = _bll.ListAll().Find(t => t.TID.Equals(uvm.TID));
            //string message = "User Updated Successfully";
            //tblTechnician tb = db.tblTechnicians.Where(s => s.TID == uvm.TID).FirstOrDefault();
            //bool isexists = _bll.EmailExists(uvm.Email, uvm.PhoneNumber);
            //if (isexists)
            //{
            //    if (tb.Email == uvm.Email)
            //    {
            //        if (ModelState.IsValid)
            //        {
            //            _bll.Update(uvm);
            //            return Request.CreateResponse(HttpStatusCode.OK, message);
            //        }
            //        else
            //        {
            //            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            //        }
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("Email", "Email Already Exist");
            //        ModelState.AddModelError("PhoneNumber", "PhoneNumber Already Exist");
            //    }
            //}

            //if (ModelState.IsValid)
            //{
            //    _bll.Update(uvm);
            //    return Request.CreateResponse(HttpStatusCode.OK, message);
            //}
            //else
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            //}

            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];
            //Convert the File data to Byte Array.
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }
            string message = "Profile Updated Successfully";
            var Id = Convert.ToInt32(HttpContext.Current.Request.Params["Id"]);
            var FullName = HttpContext.Current.Request.Params["FullName"];
            var Gender = HttpContext.Current.Request.Params["Gender"];
            var Email = HttpContext.Current.Request.Params["Email"];
            var PhoneNumber = HttpContext.Current.Request.Params["PhoneNumber"];
            var Address = HttpContext.Current.Request.Params["Address"];
            var TType = HttpContext.Current.Request.Params["TType"];
            var Password = HttpContext.Current.Request.Params["Password"];
            var Latitude = HttpContext.Current.Request.Params["Latitude"];
            var Longitude = HttpContext.Current.Request.Params["Latitude"];
            OnlineTechnicalProfessionalHiringSystemEntities _db = new OnlineTechnicalProfessionalHiringSystemEntities();

            tblTechnician tb = _db.tblTechnicians.Where(a => a.TID == Id).FirstOrDefault();
            //Insert the File to Database Table.
            if (ModelState.IsValid)
            {

                tb.FullName = FullName;
                tb.Gender = Gender;
                tb.Email = Email;
                tb.PhoneNumber = PhoneNumber;
                tb.Address = Address;
                tb.TType = Convert.ToInt32(TType);
                tb.Latitude = Convert.ToDecimal(Latitude);
                tb.Longitude = Convert.ToDecimal(Longitude);
                tb.Photo = bytes;
                _db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }
        }

        [System.Web.Http.Route("api/Technician/UpdateLoc")]
        public HttpResponseMessage UpdateLatAndLong()
        {
          
            string message = "Profile Updated Successfully";
            var Id = Convert.ToInt32(HttpContext.Current.Request.Params["Id"]);
            var Latitude = HttpContext.Current.Request.Params["Latitude"];
            var Longitude = HttpContext.Current.Request.Params["Longitude"];
            OnlineTechnicalProfessionalHiringSystemEntities _db = new OnlineTechnicalProfessionalHiringSystemEntities();

            tblTechnician tb = _db.tblTechnicians.Where(a => a.TID == Id).FirstOrDefault();
            //Insert the File to Database Table.
          

                tb.FullName = tb.FullName;
                tb.Gender = tb.Gender;
                tb.Email = tb.Email;
                tb.PhoneNumber = tb.PhoneNumber;
                tb.Address = tb.Address;
                tb.TType = tb.TType;
                tb.Latitude = Convert.ToDecimal(Latitude);
                tb.Longitude = Convert.ToDecimal(Longitude);
                tb.Photo = tb.Photo;
                tb.IsApproved = tb.IsApproved;
                tb.WorkCount = tb.WorkCount;
                _db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, message);
            
           
        }


    }
}
