﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seggu.Domain;
namespace Seggu.Daos.Interfaces
{
    public interface IIntegralDao : IGenericDao<Integral>
    {
        void SaveIntegral(Integral newIntegral);
    }
}
