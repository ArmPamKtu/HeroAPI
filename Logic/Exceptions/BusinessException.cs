using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Exceptions
{
    public class BusinessException : Exception
    {
        public ExceptionCode Code { get; set; }

        public BusinessException(ExceptionCode Code)
        {
            this.Code = Code;
        }

    }
}
