using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.DAL
{
    /// <summary>
    /// ManageUser SPs
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class SP_ManageUser
    {
        public const string SP_GETALLUSERS = "usp_GetUserList";
        public const string SP_GETADDIDCUSER = "[dbusermgt].[usp_ins_AddIDCUser]";
        public const string SP_GETEDITIDCUSER = "[dbusermgt].[usp_upd_EditIDCUser]";
        public const string SP_GETROLES = "[dbusermgt].[GetIDCGroups]";
    }
    public static class SP_SearchSoW
    {
        public const string SP_GETALLBUSINESSUNITS = "sow_Get_Business_Unit_Names";
        public const string SP_GETALLCIOS = "sow_Get_CIO_Names";
        public const string SP_GETALLCHUBBMANAGERS = "sow_Get_Chubb_Manager_Names";
        public const string SP_GETFILTERSOWRECORDS = "sow_Get_Filter_Sow_Data";
    }
    public static class SP_SoWTracker
    {
        public const string SP_ADDNEWSOWTRACKER = "sow_New_Add_SOW_Tracker";
        public const string SP_ADDSOWTRACKERFILES = "sow_Add_Tracker_Files";
        public const string SP_SOWTRACKERSUMMARY = "sow_Get_SoW_Tracker_Summary";
        public const string SP_MAXORIGINALID = "sow_Get_Max_Original_SoW";
        public const string SP_MAXSOWCRID = "sow_Get_Max_SoW_CR";
        public const string SP_OFFSHOREDMS = "sow_Get_Off_Shore_DMS";
        public const string SP_ONSHOREDMS = "sow_Get_On_Shore_DMS";
        public const string SP_YEARS = "sow_Get_Years";
        public const string SP_ORIGINALSOWS = "sow_Get_Original_SoWs";



    }
}
