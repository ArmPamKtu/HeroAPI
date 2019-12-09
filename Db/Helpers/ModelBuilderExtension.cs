using Db.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Db.Helpers
{
    public static class ModelBuilderExtension
    {
        public static void AddEnum<TEntity, TEnum>(this ModelBuilder modelBuilder)
            where TEntity : BaseEnumEntity, new()
        {
            foreach (var model in (TEnum[])Enum.GetValues(typeof(TEnum)))
                modelBuilder.Entity<TEntity>().HasData(new TEntity { Id = model.GetHashCode(), Name = model.ToString() });
        }
    }
}
