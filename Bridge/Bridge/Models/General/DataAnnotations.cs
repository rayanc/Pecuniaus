using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Collections;
using System.Configuration;


///THIS FILE SHOULD INCLUDE ONLY DATAANOTATIONS THAT CAN BE REUSED, IF YOU ARE GOING TO IMPLEMENT CUSTOM ATTRIBUTES 
///THAT WILL BE USED JUST IN A CONTEXT OF A SINGLE MODEL, PLEASE ADD THOSE IN THE SAME MODEL FILE
namespace Bridge.Models
{


    public class RequiredListAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            return ((IList)value).Count > 0;
        }
    }

    public class BelongToAttribute : ValidationAttribute
    {
        private Type enumType;
        public BelongToAttribute(Type enumType) { this.enumType = enumType; }
        public override bool IsValid(object value)
        {
            if (value == null) return true; //the required attribute should validate that not this, so we assume null as correct.
            if (value.GetType().IsArray)
            {
                string[] r = (string[])value;
                return Enum.GetNames(enumType).Intersect(r).Count() == r.Length && r.Length > 0;
            }
            return Enum.GetNames(enumType).Contains(value);
        }

    }

    

    
}