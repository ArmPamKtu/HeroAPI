using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Db.Entities;
using DbManager.Generic;
using Dto;
using Logic.Exceptions;
using Logic.Generic;
using Microsoft.Extensions.Logging;

namespace Logic.Feats
{
    public class FeatLogic : GenericLogic<IRepository<Feat>, FeatDto, Feat>, IFeatLogic
    {
        public FeatLogic(IRepository<Feat> repository, ILogger<FeatLogic> logger) : base(repository, logger)
        {

        }

        public override FeatDto Create(FeatDto entity)
        {
            using (var scope = Repository.DatabaseFacade.BeginTransaction())
            {
                if (entity.FromGuid == entity.ToUserGuid)
                    throw new BusinessException(ExceptionCode.SendingFeatsToYourself);

                var hasSentHugsToColleague = HasSentToColleagueThisMonth(entity.FromGuid, entity.ToUserGuid);
                if (hasSentHugsToColleague)
                    throw new BusinessException(ExceptionCode.FeatAlreadySentToThisUser);

                var item = Mapper.Map<Feat>(entity);
                Repository.Create(item);
                Repository.SaveChanges();
                scope.Commit();
            }

            return entity;
        }

        private bool HasSentToColleagueThisMonth(Guid fromUserGuid, Guid toUserGuid)
        {
            var feats = Repository.GetMany(x => x.FromGuid == fromUserGuid && x.ToUserGuid == toUserGuid && x.Created.Month.Equals(DateTime.Now.Month));
            var featsCount = feats.Sum(x => x.Value);
            if (featsCount >= 1)
                return true;
            return false;
        }
    }
}
