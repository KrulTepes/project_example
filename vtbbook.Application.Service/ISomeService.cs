using System;
using vtbbook.Application.Service.Models;

namespace vtbbook.Application.Service
{
    public interface ISomeService
    {
        Guid Some(SomeModel user);
    }
}