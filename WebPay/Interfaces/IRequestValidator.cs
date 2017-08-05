using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPay.Interfaces
{
   

    public interface IRequestValidator<T>//: IRequestValidator
    {
           List<ValidationError> DoValidation(T instance);

         bool IsValid(T instance);
    }
    //public interface IRequestValidator
    //{
    //    List<ValidationError> DoValidation(object instance);

    //    bool IsValid(object instance);

    //}
}
