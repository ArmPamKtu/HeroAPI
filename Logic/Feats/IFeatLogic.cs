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
       /* bool Update(string id, FeatDto userRoleDto);
        bool Delete(string id);*/
    }
}
