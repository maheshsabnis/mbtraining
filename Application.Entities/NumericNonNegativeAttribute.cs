using System;
using System.ComponentModel.DataAnnotations;
namespace Application.Entities
{
    public class NumericNonNegativeAttribute : ValidationAttribute
    {
        /// <summary>
        /// value: This is value accepted from the View
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            if (Convert.ToInt32(value) < 0) return false;
            else
                return true;     
        }
    }
}

