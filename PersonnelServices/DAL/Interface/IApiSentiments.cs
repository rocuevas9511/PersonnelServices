using PersonnelServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.DAL.Interface
{
    public interface IApiSentiments
    {
        Task<bool> InsertSentiment(ModSentiments sentiment);
    }
}
