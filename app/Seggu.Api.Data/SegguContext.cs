﻿using Microsoft.AspNet.Identity.EntityFramework;
using Seggu.Api.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seggu.Api.Data
{
    public partial class SegguContext : IdentityDbContext<ApplicationUser>
    {
        public SegguContext()
            : base("name=DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Locality> Localities { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Policy> Policies { get; set; }
        public virtual DbSet<Endorse> Endorses { get; set; }
        public virtual DbSet<Risk> Risks { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CasualtyType> CasualtyTypes { get; set; }
        public virtual DbSet<Casualty> Casualties { get; set; }
        public virtual DbSet<AccessoryType> AccessoryTypes { get; set; }
        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<CreditCard> CreditCards { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<ClientCreditCard> ClientCreditCards { get; set; }
        public virtual DbSet<Bodywork> Bodyworks { get; set; }
        public virtual DbSet<Use> Uses { get; set; }
        public virtual DbSet<ProducerCode> ProducerCodes { get; set; }
        public virtual DbSet<Cheque> Cheques { get; set; }
        public virtual DbSet<LedgerAccount> LedgerAccounts { get; set; }
        public virtual DbSet<CashAccount> CashAccounts { get; set; }
        public virtual DbSet<Fee> Fees { get; set; }
        public virtual DbSet<Coverage> Coverages { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Asset> Assets { get; set; }
        public virtual DbSet<Liquidation> Liquidations { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }
        public virtual DbSet<Integral> Integrals { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<VehicleModel> VehicleModels { get; set; }
        public virtual DbSet<FeeSelection> FeeSelections { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<AttachedFile> AttachedFiles { get; set; }
        public virtual DbSet<CoveragesPack> CoveragesPacks { get; set; }

        public virtual DbSet<SegguClient> SegguClients { get; set; }

    }
}
