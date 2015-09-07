﻿using Seggu.Daos.Interfaces;
using Seggu.Data;
using Seggu.Domain;

namespace Seggu.Daos
{
    public sealed class AssetDao : IdEntityDao<Asset> , IAssetDao
    {
        public AssetDao(SegguDataModelContext context)
            : base(context)
        {
        }

    }
}
