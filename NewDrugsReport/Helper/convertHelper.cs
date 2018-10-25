using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace NewDrugs.Helper
{
    public static class convertHelper
    {
        public static string TitleHelper(string title)
        {
            string[] titleList = title.Split(',');
            string retString = "";
            foreach(string mbr in titleList)
            {
                if (retString.Length > 0)
                {
                    retString += ",";
                }
                retString += MbrHelper(mbr);
            }
            return retString;
        }

        public static string MbrHelper(string mbrType)
        {
            switch (mbrType)
            {
                case "1":
                    return "個案管理人";
                case "2":
                    return "生教(輔)組長";
                case "3":
                    return "輔導教師";
                case "4":
                    return "輔導教官";
                case "5":
                    return "班級導師";
                case "6":
                    return "其他";
                case "10":
                    return "輔導老師(校安)";
                default:
                    return "未填";
            }


        }

        /*
         * 審查表依照mbr type寫入對應的列數, 回的數字就是列數
         */
        public static int MbrRowNum(int mbrType)
        {
            switch (mbrType)
            {
                case 1:
                    return 1;
                case 4:
                    return 2;
                case 5:
                    return 3;
                case 3:
                    return 4;
                case 10:
                    return 5;
                default:
                    return 0;
            }
        }

        /*
         * 制造斜線空格
         */
        public static ICellStyle CellStyleHelper(ICellStyle sampleStyle, ICellStyle slashStyle, int mbrType, string key)
        {
            
            switch (mbrType)
            {
                case 1:
                    if (key.Equals("counselingCount")|| key.Equals("conselingRecord") || key.Equals("isInspect")  )
                        return slashStyle;
                    else
                        return sampleStyle;
                case 4:
                    if (key.Equals("actIsInvite")|| key.Equals("meeingRecord") || key.Equals("inspectReport") || key.Equals("endMeetingTime") || key.Equals("endIsInvite"))
                        return slashStyle;
                    else
                        return sampleStyle;
                case 5:
                    if (key.Equals("actIsInvite") || key.Equals("meeingRecord") || key.Equals("isInspect") || key.Equals("inspectReport") || key.Equals("endMeetingTime") || key.Equals("endIsInvite"))
                        return slashStyle;
                    else
                        return sampleStyle;
                case 3:
                    if (key.Equals("actIsInvite") || key.Equals("meeingRecord") || key.Equals("isInspect") || key.Equals("inspectReport") || key.Equals("endMeetingTime") || key.Equals("endIsInvite"))
                        return slashStyle;
                    else
                        return sampleStyle;
                case 10:
                    if (key.Equals("actIsInvite") || key.Equals("meeingRecord") || key.Equals("isInspect") || key.Equals("inspectReport") || key.Equals("endMeetingTime") || key.Equals("endIsInvite"))
                        return slashStyle;
                    else
                        return sampleStyle;
                default:
                    return sampleStyle;
            }
        }

        /*
         * 有斜線空格則不寫字
         */
        public static string CellValueHelper( int mbrType, string key, string value)
        {

            switch (mbrType)
            {
                case 1:
                    if (key.Equals("counselingCount") || key.Equals("conselingRecord") || key.Equals("isInspect"))
                        return "";
                    else if (key.Equals("title"))
                        return MbrHelper(value);
                    else
                        return value;
                case 4:
                    if (key.Equals("actIsInvite") || key.Equals("meeingRecord") || key.Equals("inspectReport") || key.Equals("endMeetingTime") || key.Equals("endIsInvite"))
                        return "";
                    else if (key.Equals("title"))
                        return MbrHelper(value);
                    else
                        return value;
                case 5:
                    if (key.Equals("actIsInvite") || key.Equals("meeingRecord") || key.Equals("isInspect") || key.Equals("inspectReport") || key.Equals("endMeetingTime") || key.Equals("endIsInvite"))
                        return "";
                    else if (key.Equals("title"))
                        return MbrHelper(value);
                    else
                        return value;
                case 3:
                    if (key.Equals("actIsInvite") || key.Equals("meeingRecord") || key.Equals("isInspect") || key.Equals("inspectReport") || key.Equals("endMeetingTime") || key.Equals("endIsInvite"))
                        return "";
                    else if (key.Equals("title"))
                        return MbrHelper(value);
                    else
                        return value;
                case 10:
                    if (key.Equals("actIsInvite") || key.Equals("meeingRecord") || key.Equals("isInspect") || key.Equals("inspectReport") || key.Equals("endMeetingTime") || key.Equals("endIsInvite"))
                        return "";
                    else if (key.Equals("title"))
                        return MbrHelper(value);
                    else
                        return value;
                default:
                    return value;
            }
        }

        public static string SetupReasonHelper(int reasonNum)
        {
            switch (reasonNum)
            {
                case 1:
                    return "經確認檢驗尿液檢體中含有濫用藥物或其他代謝物者";
                case 2:
                    return "自我坦承";
                case 3:
                    return "遭警查獲";
                case 4:
                    return "其他網絡通知涉及違反毒品危害防制條例";
                case 0:
                    return "未填";
                default:
                    return "未填";
            }
        }

        public static string IsInviteHelper(string isInvite)
        {
            if (isInvite.Equals("Y"))
            {
                return "V";
            }
            else return "";
        }

        public static string RecordHelper(string record)
        {
            if (record != null && record.Length > 0)
            {
                return "V";
            }
            else return "";
        }

        public static string InspectRecordHelper(string inspectRecord)
        {
            if (inspectRecord != null && inspectRecord.Length > 0)
            {
                return "否";
            }
            else return "";
        }

        public static string ContCounselingReasonHelper(int contReasonNum)
        {
            switch (contReasonNum)
            {
                case 1:
                    return "尿液檢體中仍含有濫用藥物或其他代謝物者";
                case 2:
                    return "因個案學生輔到週數未達12週";
                default:
                    return "未填";

            }
        }

        public static string timeHelper(DateTime tDate)
        {
            TaiwanCalendar twC = new TaiwanCalendar();
            return twC.GetYear(tDate) +
                            "." + twC.GetMonth(tDate) + "." + twC.GetDayOfMonth(tDate);
        }
    }
}