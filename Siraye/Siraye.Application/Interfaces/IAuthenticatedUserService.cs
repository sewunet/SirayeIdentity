﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Siraye.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
