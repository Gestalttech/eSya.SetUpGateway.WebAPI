using eSya.SetUpGateway.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.SetUpGateway.IF
{
    public interface IeSyaUserAccountRepository
    {
        Task<List<DO_MainMenu>> GeteSyaMenulist();
        Task<DO_UserFormRole> GetFormAction(string navigationURL);
        Task<DO_UserAccount> GetBusinessLocation();
        Task<List<DO_LocalizationResource>> GetLocalizationResourceString(string culture, string resourceName);
        Task<List<DO_ISDCodes>> GetISDCodes();
        Task<List<DO_ApplicationRules>> GetApplicationRuleListByProcesssID(int processID);
    }
}
