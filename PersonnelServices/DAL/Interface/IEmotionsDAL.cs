using PersonnelServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.DAL.Interface
{
    public interface IEmotionsDAL
    {
        Task<bool> InsertEmotion(ModEmotion emotion);
    }
}
