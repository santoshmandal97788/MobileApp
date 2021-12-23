using OnlineTechnicalHiring.Models;
using OnlineTechnicalHiring.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public HttpResponseMessage Post([Bind(Include = "Email,Model")] TechnicianViewModel uvm)
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

                tblTechnician tb = db.tblTechnicians.Where(x => x.Email == uvm.Email).FirstOrDefault();
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
    }
}
