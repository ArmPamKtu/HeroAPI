using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Feats
{
    public interface IFeatLogic
    {
        ICollection<FeatDto> GetAll();
        FeatDto GetById(string id);
        FeatDto Create(FeatDto userRoleDto);
        bool Delete(Guid entityId);
        bool Update(Guid entityId, FeatDto entity);

    }
}
