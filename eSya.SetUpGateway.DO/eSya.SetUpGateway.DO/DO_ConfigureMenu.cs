using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.SetUpGateway.DO
{
    public class DO_ConfigureMenu
    {
        public List<DO_MainMenu> l_MainMenu { get; set; }
        public List<DO_SubMenu> l_SubMenu { get; set; }
        public List<DO_FormMenu> l_FormMenu { get; set; }
    }
    public class DO_MainMenu
    {
        public int MainMenuId { get; set; }
        public string MainMenu { get; set; }
        public string ImageURL { get; set; }
        public int MenuIndex { get; set; }
        public bool ActiveStatus { get; set; }
        public int UserId { get; set; }
        public string TerminalId { get; set; }

        public List<DO_SubMenu> l_SubMenu { get; set; }

        public List<DO_FormMenu> l_FormMenu { get; set; }
    }

    public class DO_SubMenu
    {
        public int MenuItemId { get; set; }
        public int MainMenuId { get; set; }
        public string MenuItemName { get; set; }
        public string MenuSubGroupName { get; set; }
        public int ParentID { get; set; }
        public string ImageURL { get; set; }
        public int MenuIndex { get; set; }
        public bool ActiveStatus { get; set; }
        public int UserId { get; set; }
        public string TerminalId { get; set; }

        public List<DO_SubMenu> l_SubMenu { get; set; }
        public List<DO_FormMenu> l_FormMenu { get; set; }
    }

    public class DO_FormMenu
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }
        public int MainMenuId { get; set; }
        public int FormId { get; set; }
        public string FormNameClient { get; set; }
        public int FormIndex { get; set; }
        public bool ActiveStatus { get; set; }
        public int UserId { get; set; }
        public string TerminalId { get; set; }
        //property for UserGroup 
        public int MenuKey { get; set; }

        public int ParentId { get; set; }

        public string FormInternalID { get; set; }
        public string NavigateUrl { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string View { get; set; }

        public List<DO_FormMenu> l_FormMenu { get; set; }
    }
    public class DO_UserFormRole
    {
        public int FormID { get; set; }
        public string FormIntID { get; set; }
        public string FormName { get; set; }
        public bool IsView { get; set; }
        public bool IsInsert { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
        public bool IsPrint { get; set; }
        public bool IsRePrint { get; set; }
        public bool IsApprove { get; set; }
        public bool IsAuthenticate { get; set; }
        public bool IsGiveConcession { get; set; }
        public bool IsGiveDiscount { get; set; }
    }

    public class DO_UserAccount
    {
        public Dictionary<int, string> l_BusinessKey { get; set; }
        public List<int> l_FinancialYear { get; set; }
        public int SelectedBusinessKey { get; set; }
        public int SelectedFinancialYear { get; set; }
      
    }
    public class DO_ApplicationRules
    {
        public int ProcessID { get; set; }
        public int RuleID { get; set; }
        public bool RuleStatus { get; set; }
    }
    public class DO_LocalizationResource
    {
        public string ResourceName { get; set; }
        public string Culture { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public class DO_ISDCodes
    {
        public int Isdcode { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CountryFlag { get; set; }
        public string CurrencyCode { get; set; }
        public string MobileNumberPattern { get; set; }
        public string Uidlabel { get; set; }
        public string Uidpattern { get; set; }
        public string Nationality { get; set; }
        public bool IsPoboxApplicable { get; set; }
        public string PoboxPattern { get; set; }
        public bool IsPinapplicable { get; set; }
        public string PincodePattern { get; set; }
    }
}
