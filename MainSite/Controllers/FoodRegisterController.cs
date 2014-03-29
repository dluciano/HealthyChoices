using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Entity.Capture;
using Entity.Security;
using MainSite.Filters;
using MainSite.Models;
using Service;
using WebMatrix.WebData;

namespace MainSite.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class FoodRegisterController : Controller
    {
        private readonly IFoodRegisterService foodRegisterService;
        private readonly ITakenAtService takenAtService;
        private readonly IFoodTypeService foodTypeService;
        private readonly IUserService userService;

        private User loggedUser;

        private User LoggedUser
        {
            get
            {
                return loggedUser ?? (loggedUser = userService.GetById(WebSecurity.CurrentUserId));
            }
        }

        public FoodRegisterController(IFoodRegisterService foodRegisterService, ITakenAtService takenAtService, IFoodTypeService foodTypeService, IUserService userService)
        {
            this.foodRegisterService = foodRegisterService;
            this.takenAtService = takenAtService;
            this.foodTypeService = foodTypeService;
            this.userService = userService;
        }

        //
        // GET: /FoodRegister/
        [Authorize]
        public ActionResult Index()
        {
            InitializeDropDowns();
            return View();
        }

        [Authorize]
        public JsonResult GetById(int id)
        {
            InitializeDropDowns();
            var foodRegister = foodRegisterService.GetById(id);
            var aux = new FoodRegisterViewModel
            {
                Description = foodRegister.Description,
                CreationDate = String.Format("{0:dd/MM/yy hh:mm}", foodRegister.CreationDate),
                Id = foodRegister.Id,
                FoodType = ((List<FoodType>)(ViewBag.FoodTypes)).FirstOrDefault(f => f.Id == foodRegister.FoodTypeId).Description,
                TakenAt = ((List<TakenAt>)(ViewBag.TakenAts)).FirstOrDefault(t => t.Id == foodRegister.TakenAtId).Description
            };
            return Json(aux, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodRegisterViewModel entity)
        {
            InitializeDropDowns();
            if (ModelState.IsValid)
            {
                try
                {

                    var aux = new FoodRegister
                        {
                            Description = entity.Description,
                            //TakenAt = ((List<TakenAt>)ViewBag.TakenAts).FirstOrDefault(m => m.Id == entity.TakenAtId),
                            //FoodType = ((List<FoodType>)ViewBag.FoodTypes).FirstOrDefault(m => m.Id == entity.FoodTypeId),
                            //User = LoggedUser,
                            TakenAtId = entity.TakenAtId,
                            FoodTypeId = entity.FoodTypeId,
                            UserId = LoggedUser.Id
                        };

                    foodRegisterService.Insert(aux);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e);
                }
            }

            return PartialView("CreatePartial", entity);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodRegisterViewModel entity)
        {
            InitializeDropDowns();
            if (ModelState.IsValid)
            {
                var auxEntity = foodRegisterService.GetById(entity.Id);
                var message = auxEntity == null ? "This register don't exists!" :
                    !IsValidDateForModification(auxEntity) ? "You can't update the food register of yesterday or less" : "";
                if (!string.IsNullOrWhiteSpace(message))
                    throw new Exception(message);
                try
                {
                    var aux = new FoodRegister
                    {
                        Id = entity.Id,
                        Description = entity.Description,
                        FoodTypeId = entity.FoodTypeId
                    };

                    foodRegisterService.Update(aux);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("error", e);
                }
            }

            return PartialView("CreatePartial", entity);
        }

        [Authorize]
        public JsonResult Remove(int id)
        {
            string message = "";
            try
            {
                var entity = foodRegisterService.GetById(id);
                message = entity == null ? "This register don't exists!" :
                    !IsValidDateForModification(entity) ? "You can't eliminate the food register of yesterday or less" :
                    foodRegisterService.Remove(id) <= 0 ? "Error eliminating this data" :
                    "Registry eliminated successfully";
            }
            catch (Exception ex)
            {
                message = "Unexpected error while eliminating this register." + (HttpContext.IsDebuggingEnabled ? " Details: <br/>" + ex.Message : "");
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult GetFoodRegistersByLoggedUserAndDate(DateTime? date)
        {
            if (!date.HasValue)
                return Json(new { Message = "No valid datetime sent to the server" }, JsonRequestBehavior.AllowGet);
            InitializeDropDowns();
            var foodRegisters = foodRegisterService
                .GetByUserAndDate(LoggedUser, date.Value)
                .Select(m =>
                    {
                        var foodType = ((List<FoodType>)(ViewBag.FoodTypes)).FirstOrDefault(f => f.Id == m.FoodTypeId);
                        var takenAt = ((List<TakenAt>)(ViewBag.TakenAts)).FirstOrDefault(t => t.Id == m.TakenAtId);
                        return new FoodRegisterViewModel
                            {
                                Description = m.Description,
                                CreationDate = String.Format("{0:MM/dd/yyyy hh:mm}", m.CreationDate),
                                Id = m.Id,
                                FoodType = foodType.Description,
                                TakenAt = takenAt.Description,
                                TakenAtId = takenAt.Id,
                                FoodTypeId = foodType.Id,
                                UserId = m.UserId
                            };
                    })
                .ToList();
            return Json(foodRegisters, JsonRequestBehavior.AllowGet);
        }

        private bool IsValidDateForModification(FoodRegister entity)
        {
            var dateTime = DateTime.Now;
            return (dateTime.Day == entity.CreationDate.Day &&
                dateTime.Month == entity.CreationDate.Month &&
                   dateTime.Year == entity.CreationDate.Year);
        }

        private void InitializeDropDowns()
        {
            ViewBag.TakenAts = takenAtService.GetAll();
            ViewBag.FoodTypes = foodTypeService.GetAll();
        }
    }
}
