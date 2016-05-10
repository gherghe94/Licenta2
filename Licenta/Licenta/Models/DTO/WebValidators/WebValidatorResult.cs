using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Licenta.Models.DTO.WebValidators
{
    public class WebValidatorResult
    {
        public bool IsOk { get; set; }

        public List<string> Errors { get; private set; }

        public int EntityId { get; set; }

        public WebValidatorResult()
        {
            IsOk = true;
            Errors = new List<string>();
        }

        public void Append(string error)
        {
            Errors.Add(error);
            IsOk = false;
        }
    }
}