using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IEmailServices
    {
        public abstract bool IsEmail(string email);
    }
}
