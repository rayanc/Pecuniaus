﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pecuniaus.Web.Models
{
    public class ProcessorModel
    {
        public ProcessorModel()
        {
            ProcessorTypes = new List<SelectListItem>();
        }

        public int ID { get; set; }
        [Required]
        [Display(Name = "Processor ID")]
        public int processorId { get; set; }
        
        public string processorNumber { get; set; }
        
        
        public string processorRNC { get; set; }
        
        
        [Display(Name = "Processor")]
        public string processorName { get; set; }
        //public string companyName { get; set; }
        //public string processorRNC { get; set; }
        //public string bankAccountNumber { get; set; }
        //public string bankAccountName { get; set; }
        //public string authorisedOwner { get; set; }

        [Required]
        [Display(Name = "Processor Compar")]
        public int ProcessorTypeId { get; set; }

        [Display(Name = "Processor Compar")]
        public IEnumerable<SelectListItem> ProcessorTypes { get; set; }

        [Required]
        [Display(Name = "First Processed Date")]
        [DataType(DataType.Date)]
        public DateTime FirstProcessedDate { get; set; }
    }
}