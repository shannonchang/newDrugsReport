using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NewDrugs.Base;
using NewDrugs.Models;

namespace NewDrugs.Dao
{
    public class ReportDao : BaseDao{
        public ReportDao(){
            setXml("ReportViewSqlProvider.xml");
        }

        public List<TbDrugsNoticeUtils> qryDynamicReportByExp(TbDrugsNoticeUtils tbDrugsNoticeUtils, string loginType, string loginUser) => qryDynamicReportByGrid(0, 0, tbDrugsNoticeUtils, loginType, loginUser);
        public List<TbDrugsNoticeUtils> qryDynamicReportByGrid(int beginRow, int endRow, TbDrugsNoticeUtils tbDrugsNoticeUtils, string loginType, string loginUser){
            List<TbDrugsNoticeUtils> dataList = new List<TbDrugsNoticeUtils>();
            string whereString = dynamicWhereString(tbDrugsNoticeUtils);
            string reative = "";
            if(loginType == "2"){
                whereString += " and dn.USER_ID = @USER_ID ";
            }else if(loginType == "3"){
                whereString += " and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ";
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = getSelectSql("ReportViewSqlProvider", "qryDynamicReport", whereString);
            if (beginRow > 0 && endRow > 0){
                sql = reative +  "select * from (" + sql + ") new_table where ROW_ID >= " + beginRow + " and ROW_ID <= " + endRow;
            }

            var resultList = QueryTableListBySql(sql, new{
                isSetupCH = tbDrugsNoticeUtils.isSetupCh,
                stuName = "%"+tbDrugsNoticeUtils.stuName+"%",
                school = "%" + tbDrugsNoticeUtils.school + "%",
                isAgain = tbDrugsNoticeUtils.isAgain,
                countyId = tbDrugsNoticeUtils.countyId,
                counselingStatus = tbDrugsNoticeUtils.counselingStatus,
                flowStatus = tbDrugsNoticeUtils.flowStatus,
                schoolSystemSno = tbDrugsNoticeUtils.schoolSystemSno,
                sex = tbDrugsNoticeUtils.sex,
                isPoliceSeized = tbDrugsNoticeUtils.isPoliceSeized,
                useReason = tbDrugsNoticeUtils.useReason,
                drugsEventType = tbDrugsNoticeUtils.drugsEventType,
                drugsOrigin = tbDrugsNoticeUtils.drugsOrigin,
                counselingBeginDate = tbDrugsNoticeUtils.counselingBeginDateStr,
                counselingEndDate = tbDrugsNoticeUtils.counselingEndDateStr,
                birthDayStart = tbDrugsNoticeUtils.birthDayStart,
                birthDayEnd = tbDrugsNoticeUtils.birthDayEnd,
                USER_ID = loginUser
            });
            foreach (var row in resultList){
                TbDrugsNoticeUtils bean = new TbDrugsNoticeUtils();
                bean.rowId = Int32.Parse(row.ROW_ID.ToString());
                bean.idn = row.IDN.ToString();
                bean.countyStr = row.COUNTY_STR;
                bean.school = row.SCHOOL;
                bean.schoolSystemStr = row.SCHOOL_SYSTEM_STR;
                bean.stuName = row.STU_NAME;
                bean.sexStr = row.SEX_STR;
                bean.stuBirth = row.STU_BIRTH;
                bean.eduInfo = row.EDU_INFO;
                bean.isPoliceSeizedStr = row.IS_POLICE_SEIZED_STR;
                bean.drugsOriginStr = row.DRUGS_ORIGIN_STR;
                bean.drugsEventTypeStr = row.DRUGS_EVENT_TYPE_STR;
                bean.useReasonStr = row.USE_REASON_STR;
                if(row.COUNSELING_BEGINTIME != null){
                    bean.counselingBeginDate = row.COUNSELING_BEGINTIME;
                }
                if(row.COUNSELING_ENDTIME != null){
                    bean.counselingEndDate = row.COUNSELING_ENDTIME;
                }
                bean.isAgainStr = row.IS_AGAIN_STR;
                bean.counselingStatusStr = row.COUNSELING_STATUS_STR;
                bean.flowStatusStr = row.FLOW_STATUS_STR;
                dataList.Add(bean);
            }
            return dataList;
        }
        public int qryDynamicReportCount(TbDrugsNoticeUtils tbDrugsNoticeUtils, string loginType, string loginUser){
            int count = 0;
            string whereString = dynamicWhereString(tbDrugsNoticeUtils);
            string reative = "";
            if(loginType == "2"){
                whereString += " and dn.USER_ID = @USER_ID ";
            }else if(loginType == "3"){
                whereString += " and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ";
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }

            string sql = getSelectSql("ReportViewSqlProvider", "qryDynamicReport", whereString);
            sql = reative +  "select count(*) from ( " + sql + " ) new_table ";
            count = QueryTableFirstBySql<int>(sql, new{
                isSetupCH = tbDrugsNoticeUtils.isSetupCh,
                stuName = "%" + tbDrugsNoticeUtils.stuName + "%",
                school = "%" + tbDrugsNoticeUtils.school + "%",
                isAgain = tbDrugsNoticeUtils.isAgain,
                countyId = tbDrugsNoticeUtils.countyId,
                counselingStatus = tbDrugsNoticeUtils.counselingStatus,
                flowStatus = tbDrugsNoticeUtils.flowStatus,
                schoolSystemSno = tbDrugsNoticeUtils.schoolSystemSno,
                sex = tbDrugsNoticeUtils.sex,
                isPoliceSeized = tbDrugsNoticeUtils.isPoliceSeized,
                useReason = tbDrugsNoticeUtils.useReason,
                drugsEventType = tbDrugsNoticeUtils.drugsEventType,
                drugsOrigin = tbDrugsNoticeUtils.drugsOrigin,
                counselingBeginDate = tbDrugsNoticeUtils.counselingBeginDateStr,
                counselingEndDate = tbDrugsNoticeUtils.counselingEndDateStr,
                birthDayStart = tbDrugsNoticeUtils.birthDayStart,
                birthDayEnd = tbDrugsNoticeUtils.birthDayEnd,
                USER_ID = loginUser
            });
            return count;
        }
        private string dynamicWhereString(TbDrugsNoticeUtils tbDrugsNoticeUtils){
            StringBuilder whereString = new StringBuilder();
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.stuName)){
                whereString.AppendLine(" and STU_NAME like @stuName ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.school)){
                whereString.AppendLine(" and SCHOOL like @school ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.countyId)){
                whereString.AppendLine(" and COUNTY_ID in @countyId ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.sex)){
                whereString.AppendLine(" and SEX = @sex ");
            }
            if(tbDrugsNoticeUtils.schoolSystemSno > 0){
                whereString.AppendLine(" and SCHOOL_SYSTEM_SNO = @schoolSystemSno ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.counselingStatus)){
                whereString.AppendLine(" and COUNSELING_STATUS = @counselingStatus ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.isPoliceSeized)){
                whereString.AppendLine(" and IS_POLICE_SEIZED = @isPoliceSeized ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.useReason)){
                whereString.AppendLine(" and USE_REASON = @useReason ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.drugsEventType)){
                whereString.AppendLine(" and DRUGS_EVENT_TYPE = @drugsEventType ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.drugsOrigin)){
                whereString.AppendLine(" and DRUGS_ORIGIN = @drugsOrigin ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.isAgain)){
                whereString.AppendLine(" and IS_AGAIN = @isAgain ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.isSetupCh)){
                whereString.AppendLine(" and IS_SETUP_CH = @isSetupCh ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.counselingBeginDateStr)){
                whereString.AppendLine(" and COUNSELING_BEGINTIME >= convert(datetime,@counselingBeginDate + ' 00:00:00',120) ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.counselingEndDateStr)){
                whereString.AppendLine(" and COUNSELING_ENDTIME <= convert(datetime,@counselingEndDate + ' 23:59:59',120) ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.birthDayStart)){
                whereString.AppendLine(" and convert(date,STU_BIRTH, 120) >= convert(datetime,@birthDayStart + ' 00:00:00',120) ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.birthDayEnd)){
                whereString.AppendLine(" and convert(date,STU_BIRTH, 120) <= convert(datetime,@birthDayEnd + ' 23:59:59',120) ");
            }
            return whereString.ToString();
        }

        public List<dynamic> qryPeopleAmountOnMonthReportList(string beginYear, string beginMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "peopleAmountOnMonthReport", whereString.ToString());

            var resultList = QueryTableListBySql(sql, new { qryYYYY = beginYear, qryMM = beginMonth, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> qryPeopleAmountByDrugsLvList(string beginYear, string endYear, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "peopleAmountByDrugsLv", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, endYear = endYear });
            return resultList;
        }

        public List<dynamic> qryUndeclaredCount(string beginYear, string beginMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and ud.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "undeclaredCount", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth = beginMonth, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> qryUndeclaredList(string beginYear, string beginMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and ud.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "undeclaredList", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth = beginMonth, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> qrySpcfCategoryCount(string beginYear, string beginMonth, string loginUser){
            string sql = getSelectSql("ReportViewSqlProvider", "spcfCategoryCount");
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth = beginMonth, USER_ID = loginUser});
            return resultList;
        }
        public List<dynamic> qrySpcfCategoryCountAdmin(string beginYear, string beginMonth, string countyId){
            string sql = getSelectSql("ReportViewSqlProvider", "spcfCategoryCountAdmin");
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth = beginMonth, countyId = countyId });
            return resultList;
        }

        public List<dynamic> qrySpcfSchoolList(string beginYear, string beginMonth, string loginType, string loginUser, string schoolSystemSno){
            StringBuilder whereString = new StringBuilder();
            if(loginType == "3"){
                whereString.AppendLine(" join user_relative ur on ur.RELATIVE_USER_ID = ud.USER_ID ");
            }
            string sql = getSelectSql("ReportViewSqlProvider", "spcfCategoryListBySchool", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth = beginMonth, USER_ID = loginUser, schoolSystemSno= schoolSystemSno });
            return resultList;
        }
        public List<dynamic> qrySpcfCategoryList(string beginYear, string beginMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and mas.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "spcfPersonList", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth = beginMonth, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> qryStuUseDrugs(string beginYear, string endYear, string loginType, string loginUser, string beginMonth = "", string endMonth = "")
        {
            StringBuilder whereString = new StringBuilder();
            string beginDate = beginYear + "/" + (!string.IsNullOrEmpty(beginMonth) ? beginMonth : "01") + "/01";
            string endDate = endYear + "/" + (!string.IsNullOrEmpty(endMonth) ? endMonth : "12") + "/01";
            if (loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
            }
            string sql = getSelectSql("ReportViewSqlProvider", "stuUseDrugs", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginDate, endYear = endDate, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> qryStuInfoToWeiFu(string beginYear, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "stuInfoToWeiFu", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> qryStuAllInfoExp(string beginYear, string endYear, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "stuAllInfoExp", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, endYear= endYear, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> qryUseDrugsStuCount(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "useDrugsStuCount", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth= beginMonth, endYear = endYear, endMonth= endMonth, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> qryUseDrugsLvCount(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "useDrugsLvCount", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth= beginMonth, endYear = endYear, endMonth= endMonth, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> qryCounselingStatusCount(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "counselingStatusCount", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth= beginMonth, endYear = endYear, endMonth= endMonth, USER_ID = loginUser });
            return resultList;
        }

        public List<string> qryTtouchReasnoDynamic(){
            var resultList = QueryTableListBySql<string>(getSelectSql("ReportViewSqlProvider", "touchReasnoDynamic"));
            return resultList;
        }
        public List<dynamic> qryCsrcCaseAnsi(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            if (loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
            }
            string sql = getSelectSql("ReportViewSqlProvider", "csrcCaseAnsi", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth = beginMonth, endYear = endYear, endMonth = endMonth, USER_ID = loginUser });
            return resultList;
        }
        public int qryDrugsStuTotalCount(string beginYear, string beginMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "drugsStuTotalCount", whereString.ToString());
            var resultList = QueryTableFirstBySql<int>(sql, new { beginYear = beginYear, beginMonth = beginMonth, USER_ID = loginUser });
            return resultList;
        }
        public List<dynamic> qryCsrcBulletinUseDrugs(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            if (loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
            }
            string sql = getSelectSql("ReportViewSqlProvider", "csrcBulletinUseDrugs", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth = beginMonth, endYear = endYear, endMonth = endMonth, USER_ID = loginUser });
            return resultList;
        }
        public List<dynamic> qryCltkCount(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if(loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "cltkCount", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, beginMonth = beginMonth, endYear = endYear, endMonth = endMonth, USER_ID = loginUser });
            return resultList;
        }
        public List<dynamic> qryFindCounselingQuid(string beginYear, string endYear, string schoolSystemSno, string loginType, string loginUser){
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            whereString.AppendLine(" and dn.SCHOOL_SYSTEM_SNO = @schoolSystemSno ");
            if (loginType == "3"){
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("ReportViewSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "findCounselingQuid", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, endYear = endYear, schoolSystemSno = schoolSystemSno, USER_ID = loginUser });
            return resultList;
        }

        //外包 立偉
        public List<dynamic> qryStuUseDrugsAmountByEduLv(string beginYear, string endYear, string loginType, string loginUser)
        {
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if (loginType == "3")
            {
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("TbDrugsNoticeSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "studentDrugAbuseAmountByEduLv", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, endYear = endYear, USER_ID = loginUser });
            return resultList;
        }
        public List<dynamic> qyrDrugsClassificationBySexAndYear(string beginYear, string endYear, string loginType, string loginUser)
        {
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if (loginType == "3")
            {
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("TbDrugsNoticeSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "drugsClassficationBySexAndYear", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, endYear = endYear, USER_ID = loginUser });
            return resultList;
        }
        public List<dynamic> getDrugsAbusePortionByEduDvsAndEduLv(string beginYear, string endYear, string loginType, string loginUser)
        {
            StringBuilder whereString = new StringBuilder();
            string reative = "";
            if (loginType == "3")
            {
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
                reative = this.getSelectSql("TbDrugsNoticeSqlProvider", "relativeAuthUser") + " ";
            }
            string sql = reative + getSelectSql("ReportViewSqlProvider", "drugAbusePortionByEduDvsAndEduLv", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = beginYear, endYear = endYear, USER_ID = loginUser });
            return resultList;
        }

        public List<dynamic> GetTbCHGroupsList()
        {
            string sql = "select * from TB_CH_GROUP";
            List<dynamic> list = new List<dynamic>();
            list = QueryTableListBySql(sql);
            return list;
        }

        /*
         * 建立mbr type對應表
         */
        public List<dynamic> GetMbrTypeCommonValue()
        {
            string sql = "select COMM_VALUE,COMM_CODE from TB_COMMON_CODE where COMM_TYPE = 'MBRTP'";
            List<dynamic> list = new List<dynamic>();
            list = QueryTableListBySql(sql);
            return list;
        }

        /*20181011 Frank
         * 縣市薦報表
         */
        public List<dynamic> QryCityRewardsList(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string loginUser)
        {
            StringBuilder whereString = new StringBuilder();
            //string beginDate = beginYear + "/" + (!string.IsNullOrEmpty(beginMonth) ? beginMonth : "01") + "/01";
            //string endDate = endYear + "/" + (!string.IsNullOrEmpty(endMonth) ? endMonth : "12") + "/01";
            if (loginType == "3")
            {
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
            }
            string sql = getSelectSql("ReportViewSqlProvider", "spcCityRewardList", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new
            {
                beginYear = Int32.Parse(beginYear),
                beginMonth = Int32.Parse(beginMonth)
                ,
                endYear = Int32.Parse(endYear),
                endMonth = Int32.Parse(endMonth),
                USER_ID = loginUser
            });
            return resultList;
        }

        /*20181011 Frank
         * 推薦報表
         */
        public List<dynamic> QryRewardsList(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string loginUser)
        {
            StringBuilder whereString = new StringBuilder();
            //string beginDate = beginYear + "/" + (!string.IsNullOrEmpty(beginMonth) ? beginMonth : "01") + "/01";
            //string endDate = endYear + "/" + (!string.IsNullOrEmpty(endMonth) ? endMonth : "12") + "/01";
            if (loginType == "3")
            {
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
            }
            string sql = getSelectSql("ReportViewSqlProvider", "spcRewardList", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new { beginYear = Int32.Parse(beginYear), beginMonth = Int32.Parse(beginMonth)
                ,endYear = Int32.Parse(endYear), endMonth= Int32.Parse(endMonth), USER_ID = loginUser });
            return resultList;
        }

        /*20181011 Frank
         * 統計表
         */
        public List<dynamic> QrySpcItemList(string beginYear, string beginMonth, string endYear, string endMonth, string loginType, string loginUser)
        {
            StringBuilder whereString = new StringBuilder();
            //string beginDate = beginYear + "/" + (!string.IsNullOrEmpty(beginMonth) ? beginMonth : "01") + "/01 00:00:00";
            //string endDate = endYear + "/" + (!string.IsNullOrEmpty(endMonth) ? endMonth : "12") + "/01";
            if (loginType == "3")
            {
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
            }
            string sql = getSelectSql("ReportViewSqlProvider", "spcItemList", whereString.ToString());
            var resultList = QueryTableListBySql(sql, new
            {
                beginYear = Int32.Parse(beginYear),
                beginMonth = Int32.Parse(beginMonth)
                ,
                endYear = Int32.Parse(endYear),
                endMonth = Int32.Parse(endMonth),
                USER_ID = loginUser
            });
            return resultList;
        }

        /*20181021 Frank
         * 統計表人員
         */
        public List<dynamic> QrySpcPeopleList(string snoList, string loginType, string loginUser)
        {
            StringBuilder whereString = new StringBuilder();
            
            if (loginType == "3")
            {
                whereString.AppendLine(" and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ");
            }
            string sql = getSelectSql("ReportViewSqlProvider", "spcPeopleList", whereString.ToString());
            sql = sql + "in (" + snoList + ");";
            var resultList = QueryTableListBySql(sql, new { USER_ID = loginUser });
            return resultList;
        }
    }
}
