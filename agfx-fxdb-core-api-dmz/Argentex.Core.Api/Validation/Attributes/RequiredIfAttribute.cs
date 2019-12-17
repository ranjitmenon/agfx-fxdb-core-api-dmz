using System;
using System.ComponentModel.DataAnnotations;

namespace Argentex.Core.Api.Validation.Attributes
{
    public class RequiredIfAttribute: RequiredAttribute
    {
        private string PropertyName { get; } //This is the property we're going to be checking the desired value against
        private object DesiredValue { get; } //If the property we're checking matches this desired value, we then do the required validation on the property

        public RequiredIfAttribute(string propertyName, object desiredvalue)
        {
            PropertyName = propertyName;
            DesiredValue = desiredvalue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var property = context.ObjectInstance.GetType().GetProperty(PropertyName); //Get the specified property belonging to the object we're validating
            if (property == null) //simple null check for easy debugging
                throw new ArgumentException($"Unable to get Property {PropertyName} to check if required", nameof(PropertyName));
            var proprtyvalue = property.GetValue(context.ObjectInstance, null); //getting the value from the property on the object
            if (proprtyvalue.ToString() == DesiredValue.ToString()) return base.IsValid(value, context); //if the object matches the property we expect, use the default RequiredAttribute behaviour
            return ValidationResult.Success; //if the value doesn't match what we're expecting, we don't validate - so it's a success
        }
    }
}
