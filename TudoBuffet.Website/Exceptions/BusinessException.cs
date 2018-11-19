﻿using System;

namespace TudoBuffet.Website.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base()
        {

        }

        public BusinessException(string message) : base(message)
        {

        }

        public BusinessException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
