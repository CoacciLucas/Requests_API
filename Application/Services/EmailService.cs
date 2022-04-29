using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class EmailService
    {

        public static void IsEmail(string email)
        {

            int indexArr = email.IndexOf('@');
            if (indexArr > 0)
            {
                int indexDot = email.IndexOf('.', indexArr);
                if (indexDot - 1 > indexArr)
                {
                    if (indexDot + 1 < email.Length)
                    {
                        string indexDot2 = email.Substring(indexDot + 1, 1);
                        if (indexDot2 != ".")
                        {

                        }
                        else
                        {
                            throw new InvalidOperationException("Email Inválido");
                        }

                    }
                    else
                    {
                        throw new InvalidOperationException("Email Inválido");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Email Inválido");
                }
            }
            else
            {
                throw new InvalidOperationException("Email Inválido");
            }
        }
    }
}
