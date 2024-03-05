using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bajaj.Events.Models
{
    public class ErrorResponse
    {
        public int Id { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string StackTrace { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}
