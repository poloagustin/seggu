﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.ModelBinding;
using Seggu.Api.Data;
using Seggu.Api.Domain;
using System.Web.OData;
using Seggu.Service.ViewModels;
using AutoMapper;

namespace Seggu.Api.Controllers
{
    public class BanksController : BaseApiController<Bank, BankVM>
    {
    }
}
