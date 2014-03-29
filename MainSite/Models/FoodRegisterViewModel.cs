using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Foolproof;

namespace MainSite.Models
{
    public class FoodRegisterViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required", AllowEmptyStrings = false)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Food type")]
        public virtual int FoodTypeId { get; set; }


        public string CreationDate { get; set; }
        public int TakenAtId { get; set; }
        public string TakenAt { get; set; }
        public string FoodType { get; set; }
        public int UserId { get; set; }

    }
}