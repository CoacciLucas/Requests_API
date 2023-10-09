using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICommandResult
    {
        bool Success { get; }

        string Message { get; }

        object Data { get; }
    }
}
