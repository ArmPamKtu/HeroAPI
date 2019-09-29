using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Feats
{
    public interface IFeatLogic
    {
        ICollection<FeatDto> GetAll();
        FeatDto GetById(Guid id);
        void Create(FeatDto userRoleDto);
        bool Update(Guid id, FeatDto userRoleDto);
        bool Delete(Guid id);
    }
}
