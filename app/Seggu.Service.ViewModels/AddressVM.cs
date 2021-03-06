﻿using System;

namespace Seggu.Service.ViewModels
{
    public class AddressVM : ViewModel
    {
        public string Street { get; set; }
        public string Phone { get; set; }
        public string Number { get; set; }
        public string Floor { get; set; }
        public string Appartment { get; set; }
        public Nullable<Guid> LocalityId { get; set; }
        public string PostalCode { get; set; }
        public int AddressType { get; set; }
        public Nullable<Guid> ClientId { get; set; }
    }
}