using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewDrugs.Helper
{
    public static class convertHelper
    {
        public static string MbrHelper(int mbrType)
        {
            return "個案管理人";
        }

        /*
         * 審查表依照mbr type寫入不同列
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
                default:
                    return 0;
            }
        }

        /*
         * 
         */
        public static ICellStyle cellStyleHelper(ICellStyle sampleStyle, ICellStyle slashStyle, int mbrType, string key)
        {
            
            switch (mbrType)
            {
                case 1:
                    if (key.Equals("counselingCount"))
                        return slashStyle;
                    else
                        return sampleStyle;
                case 4:
                    if (key.Equals("actIsInvite"))
                        return slashStyle;
                    else
                        return sampleStyle;
                case 5:
                    if (key.Equals("actIsInvite"))
                        return slashStyle;
                    else
                        return sampleStyle;
                case 3:
                    if (key.Equals("actIsInvite"))
                        return slashStyle;
                    else
                        return sampleStyle;
                default:
                    return sampleStyle;
            }
        }
    }
}