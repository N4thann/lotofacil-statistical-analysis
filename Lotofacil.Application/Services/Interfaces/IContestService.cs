﻿using Lotofacil.Application.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotofacil.Application.Services.Interfaces
{
    public interface IContestService
    {
        Task CreateAsync(CreateContestViewModel contestVM);
    }
}
