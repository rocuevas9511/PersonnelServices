using PersonnelServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelServices.DAL.Interface
{
    public interface IApiSurveys
    {
        Task<string> InsertSurvey(ModSurveys bom);

        Task<ModSurveys> GetSurvey(string lang);
    }
}
