using System;
using System.Collections.Generic;
using System.Text;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Generic;

namespace Logic.Feats
{
    public class FeatLogic : GenericLogic<IRepository<Feat>, FeatDto, Feat>, IFeatLogic
    {
        public FeatLogic(IRepository<Feat> repository) : base(repository)
        {

        }
    }
}
