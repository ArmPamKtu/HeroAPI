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
                if (entity.FromUser.Equals(entity.ToUser))
                    throw new BusinessException(ExceptionCode.SendingFeatsToYourself);     


                var item = Mapper.Map<Feat>(entity);
                Repository.Create(item);
                Repository.SaveChanges();
                scope.Commit();
            }

            return entity;
        }

        public FeatDto GetById(string id)
        {

            var feat = Repository.GetByID(id);

            var mappedData = Mapper.Map<FeatDto>(feat);

            return mappedData;
        }

        public bool Delete(string entityId)
        {
            var success = false;
            if (entityId != null)
                using (var scope = Repository.DatabaseFacade.BeginTransaction())
                {
                    var item = Repository.GetByID(entityId);
                    if (item != null)
                    {
                        Repository.Delete(item);
                        Repository.SaveChanges();
                        scope.Commit();
                        success = true;
                    }
                }

            return success;
        }

        public bool Update(Guid entityId, FeatDto entity)
        {
            var success = false;
            if (entity != null)
                using (var scope = Repository.DatabaseFacade.BeginTransaction())
                {
                    var product = Repository.GetByID(entityId);
                    if (product != null)
                    {
                        var item = Mapper.Map(entity, product);
                        Repository.Update(item);
                        Repository.SaveChanges();
                        scope.Commit();
                        success = true;
                    }
                }

            return success;
        }

    }
}
