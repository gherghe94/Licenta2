using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licenta.Models.DTO.WebValidators
{
    public interface IWebValidator<T>
    {
        WebValidatorResult Validate(T entity);
    }
}
