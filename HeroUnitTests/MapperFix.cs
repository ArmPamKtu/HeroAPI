using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroUnitTests
{
    public class MapperFix : IDisposable
    {
        public MapperFix()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(
                    new AutoMapping());
            });
        }

        public void Dispose()
        {
            Mapper.Reset();
        }
    }
}
