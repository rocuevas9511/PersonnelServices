﻿
namespace PersonnelServices.DAL.Interface
{
    public interface IRepository
    {
        IApiTest ApiTest { get;  }

        IApiSurveys ApiSurveys { get;  }
    }
}