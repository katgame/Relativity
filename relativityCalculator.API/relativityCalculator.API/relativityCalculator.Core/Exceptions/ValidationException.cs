using System;
using System.Collections.Generic;
using System.Text;

namespace relativityCalculator.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public string FieldName { get; set; }
        public string ValidationError { get; set; }

        public ValidationException(string fieldName, string validationError)
        {

            this.FieldName = fieldName;
            this.ValidationError = validationError;
        }

    }
}
