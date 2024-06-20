using eSya.SetUpGateway.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.SetUpGateway.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class eSyaUserAccountController : ControllerBase
    {
        private readonly IeSyaUserAccountRepository _userAccountRepository;

        public eSyaUserAccountController(IeSyaUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        #region Used functionality as per new Gateway of Gestalt User
        [HttpGet]
        public async Task<IActionResult> GeteSyaMenulist()
        {
            var ds = await _userAccountRepository.GeteSyaMenulist();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetFormAction(string navigationURL)
        {
            var ds = await _userAccountRepository.GetFormAction(navigationURL);
            return Ok(ds);
        }
        #endregion

        #region Not Used later delete after freezed
        [HttpGet]
        public async Task<IActionResult> GetBusinessLocation()
        {
            var ds = await _userAccountRepository.GetBusinessLocation();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetLocalizationResourceString(string culture, string resourceName)
        {
            var ds = await _userAccountRepository.GetLocalizationResourceString(culture, resourceName);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetISDCodes()
        {
            var ds = await _userAccountRepository.GetISDCodes();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetApplicationRuleListByProcesssID(int processID)
        {
            var ds = await _userAccountRepository.GetApplicationRuleListByProcesssID(processID);
            return Ok(ds);
        }
        #endregion
    }
}
