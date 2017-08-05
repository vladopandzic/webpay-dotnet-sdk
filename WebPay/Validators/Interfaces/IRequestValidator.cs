using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Validators.Interfaces
{
   

    public interface IRequestValidator<T>
    {
           List<ValidationError> DoValidation(T instance);

         bool IsValid(T instance);
    }
   
}
