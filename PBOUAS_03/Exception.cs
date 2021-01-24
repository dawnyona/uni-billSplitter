using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBOUAS_03
{
    public class EmptyException : Exception // Error handler for empty inputs
    {
        public EmptyException(string message = "Please fill in all the given area") : base(message) { }
    }

    public class InvalidLogicException : Exception // Error handler for false input logic
    {
        public InvalidLogicException(string message = "Please enter the correct input") : base(message) { }
    }

    public class FailedConversionException : Exception // Error handler for incorrect input format
    {
        public FailedConversionException(string message = "Failed to convert input") : base(message) { }
    } 
}
