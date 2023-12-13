using eSya.SetUpGateway.DL.Entities;
using eSya.SetUpGateway.DO;
using eSya.SetUpGateway.IF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSya.SetUpGateway.DL.Repository
{
    public class eSyaUserAccountRepository: IeSyaUserAccountRepository
    {
        public async Task<List<DO_MainMenu>> GeteSyaMenulist()
        {
            try
            {
                using (eSyaEnterprise db = new eSyaEnterprise())
                {
                    List<DO_MainMenu> l_MenuList = new List<DO_MainMenu>();
                    var mainMenus = db.GtEcmamns.Where(w => w.ActiveStatus == true).OrderBy(o => o.MenuIndex);
                    foreach (var m in mainMenus)
                    {
                        DO_MainMenu do_MainMenu = new DO_MainMenu();
                        do_MainMenu.MainMenuId = m.MainMenuId;
                        do_MainMenu.MainMenu = m.MainMenu;
                        do_MainMenu.MenuIndex = m.MenuIndex;
                        do_MainMenu.l_FormMenu = GetSubMenuFormsItem(m.MainMenuId, 0);
                        l_MenuList.Add(do_MainMenu);
                    }
                    return l_MenuList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DO_FormMenu> GetSubMenuFormsItem(int mainMenuID, int menuItemID)
        {
            using (eSyaEnterprise db = new eSyaEnterprise())
            {
                List<DO_FormMenu> l_menuForm = new List<DO_FormMenu>();

                var sm = db.GtEcsbmns
                        .Where(w => w.MainMenuId == mainMenuID
                            && w.ParentId == menuItemID
                            && w.ActiveStatus == true)
                        .OrderBy(o => o.MenuIndex)
                        .Select(f => new DO_FormMenu()
                        {
                            MenuItemId = f.MenuItemId,
                            MenuItemName = f.MenuItemName,
                            FormIndex = f.MenuIndex,
                            ParentId = menuItemID,
                        }).OrderBy(o => o.FormIndex).ToList();

                var fm = db.GtEcmnfls
                    .Join(db.GtEcfmnms,
                        f => f.FormId,
                        i => i.FormId,
                        (f, i) => new { f, i })
                    .Where(w => w.f.MainMenuId == mainMenuID
                            && w.f.MenuItemId == menuItemID
                            && w.f.FormId > 0
                            && w.f.ActiveStatus
                            && w.i.ActiveStatus)
                    .OrderBy(o => o.f.FormIndex)
                    .AsQueryable()
                    .Select(f => new DO_FormMenu()
                    {
                        // MenuItemId = f.f.MenuItemId,
                        FormId = f.f.FormId,
                        FormInternalID = f.i.FormIntId,
                        FormNameClient = f.f.FormNameClient + (f.i.FormDescription != "Standard" ? (" - " + f.i.FormDescription) : ""),
                        NavigateUrl = f.i.NavigateUrl,
                        Area = f.i.NavigateUrl.Split('/', StringSplitOptions.None)[0],
                        Controller = f.i.NavigateUrl.Split('/', StringSplitOptions.None)[1],
                        View = f.i.NavigateUrl.Split('/', StringSplitOptions.None)[2],
                        FormIndex = f.f.FormIndex,
                        MenuKey = f.f.MenuKey,
                        ParentId = menuItemID,
                    }).OrderBy(o => o.FormIndex).ToList();

                var mf = sm.Union(fm);

                foreach (var s in sm)
                {
                    var l_s = GetSubMenuFormsItem(mainMenuID, s.MenuItemId);
                    s.l_FormMenu = l_s;
                }

                l_menuForm.AddRange(mf.OrderBy(o => o.FormIndex));

                return l_menuForm;
            }
        }

        public async Task<DO_UserFormRole> GetFormAction(string navigationURL)
        {
            using (var db = new eSyaEnterprise())
            {
                var lr = db.GtEcfmnms
                    .Where(w => w.NavigateUrl == navigationURL && w.ActiveStatus == true)
                    .AsNoTracking()
                    .Select(x => new DO_UserFormRole
                    {
                        FormID = x.FormId,
                        FormIntID = x.FormIntId,
                        FormName = db.GtEcmnfls.Where(w => w.FormId == x.FormId).FirstOrDefault().FormNameClient,
                        IsView = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 1).Count() > 0,
                        IsInsert = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 2).Count() > 0,
                        IsEdit = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 3).Count() > 0,
                        IsDelete = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 4).Count() > 0,
                        IsPrint = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 5).Count() > 0,
                        IsRePrint = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 6).Count() > 0,
                        IsApprove = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 7).Count() > 0,
                        IsAuthenticate = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 8).Count() > 0,
                        IsGiveConcession = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 9).Count() > 0,
                        IsGiveDiscount = db.GtEcfmals.Where(w => w.FormId == x.FormId && w.ActionId == 10).Count() > 0,
                    }).FirstOrDefaultAsync();

                return await lr;
            }
        }

        public async Task<DO_UserAccount> GetBusinessLocation()
        {
            using (var db = new eSyaEnterprise())
            {
                DO_UserAccount us = new DO_UserAccount();

                var ub = await db.GtEcbslns
                            .Where(w => w.ActiveStatus).ToListAsync();

                us.l_BusinessKey = ub.Select(x => new KeyValuePair<int, string>(x.BusinessKey, x.BusinessName + "-" + x.LocationDescription))
                   .ToDictionary(x => x.Key, x => x.Value);

                return us;
            }
        }

        public async Task<List<DO_LocalizationResource>> GetLocalizationResourceString(string culture, string resourceName)
        {
            using (var db = new eSyaEnterprise())
            {
                var lr = db.GtEcltfcs.Where(x=>x.ResourceName==resourceName && x.ActiveStatus)
                    .GroupJoin(db.GtEcltcds.Where(w => w.Culture == culture),
                        l => l.ResourceId,
                        c => c.ResourceId,
                        (l, c) => new { l, c })
                   .SelectMany(z => z.c.DefaultIfEmpty(),
                   (a, b) => new DO_LocalizationResource
                    {
                        ResourceName =a.l.ResourceName, 
                        Key = a.l.Key,
                        Value = b == null ? a.l.Value : b.Value,
                       //Value = x.c != null ? x.c.Value : x.l.Value
                    }).ToList();
                var DistinctKeys = lr.GroupBy(x => x.Key).Select(y => y.First());
                return DistinctKeys.ToList();

               
            }
        }
       
        public async Task<List<DO_ISDCodes>> GetISDCodes()
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEccncds
                        .Where(w => w.ActiveStatus)
                        .Select(r => new DO_ISDCodes
                        {
                            Isdcode = r.Isdcode,
                            CountryCode = r.CountryCode,
                            CountryFlag = r.CountryFlag,
                            CountryName = r.CountryName,
                            MobileNumberPattern = r.MobileNumberPattern
                        }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<DO_ApplicationRules>> GetApplicationRuleListByProcesssID(int processID)
        {
            try
            {
                using (var db = new eSyaEnterprise())
                {
                    var ds = db.GtEcprrls
                        .Join(db.GtEcaprls,
                            p => p.ProcessId,
                            r => r.ProcessId,
                            (p, r) => new { p, r })
                        .Where(w => w.p.ProcessId == processID
                            && w.p.ActiveStatus)
                       .Select(s => new DO_ApplicationRules
                       {
                           ProcessID = s.p.ProcessId,
                           RuleID = s.r.RuleId,
                           RuleStatus = s.r.ActiveStatus
                       }).ToListAsync();

                    return await ds;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
