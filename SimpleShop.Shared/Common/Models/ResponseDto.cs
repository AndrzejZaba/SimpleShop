using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleShop.Shared.Common.Models
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }
    }
}
