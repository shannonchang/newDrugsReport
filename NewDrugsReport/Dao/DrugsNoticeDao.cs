using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using NewDrugs.Base;
using NewDrugs.Common;
using NewDrugs.Models;
using CityinfoCommon;

namespace NewDrugs.Dao
{
    public class DrugsNoticeDao : BaseDao
    {
        public DrugsNoticeDao()
        {
            setXml("TbDrugsNoticeSqlProvider.xml");
        }
        /// <summary>
        /// Adds the drugs notice.
        /// </summary>
        /// <returns>The drugs notice.</returns>
        /// <param name="tbDrugsNotice">Tb drugs notice.</param>
        public int addDrugsNotice(TbDrugsNotice tbDrugsNotice){
            int sno = 0;
            sno = this.InsertTableByReturn<int>("TbDrugsNoticeSqlProvider", "insertTbDrugsNotice", mapping(tbDrugsNotice));
            return sno;
        }

        public List<TbDrugsNotice> qryDrugsNoticeListByIdn (int idn){
            List<TbDrugsNotice> noticeList = new List<TbDrugsNotice>();
            string sql = getSelectSql("TbDrugsNoticeSqlProvider", "selectTbDrugsNotice", "where IDN = @idn");
            var resultList = QueryTableListBySql(sql, new { idn = idn });
            foreach(var row in resultList){
                TbDrugsNotice bean = new TbDrugsNotice();
                bean.sno = Int32.Parse(row.SNO.ToString());
                bean.idn = Int32.Parse(row.IDN.ToString());
                bean.userId = row.USER_ID;
                bean.eventSchoolSystem = Int32.Parse(row.EVENT_SCHOOL_SYSTEM.ToString());
                bean.eventCategory = row.EVENT_CATEGORY;
                bean.eventCategoryName = row.EVENT_CATEGORY_NAME;
                bean.counselingStatus = row.COUNSELING_STATUS;
                bean.isAgain = row.IS_AGAIN;
                bean.eventReportTime = row.EVENT_REPORT_TIME;
                bean.noticeSchedule = row.NOTICE_SCHEDULE;
                bean.isWrithComplet = row.IS_WRITY_COMPLET;
                bean.isGivenPeople = row.IS_GIVEN_PEOPLE;
                bean.isMajorCase = row.IS_MAJOR_CASE;
                bean.flowStatus = row.FLOW_STATUS;
                bean.flowAccount = row.FLOW_ACCOUNT;
                if(row.FLOW_SNO != null){
                    bean.flowSno = Int32.Parse(row.FLOW_SNO.ToString());
                }
                bean.isSetupCh = row.IS_SETUP_CH;
                bean.noticeStatus = row.NOTICE_STATUS;
                bean.noticeStatusStr = row.NOTICE_STATUS_STR;
                bean.noticeReason = row.NOTICE_REASON;
                noticeList.Add(bean);
            }
            return noticeList;
        }

        /// <summary>
        /// Qries the drugs notice list.
        /// </summary>
        /// <returns>The drugs notice list.</returns>
        /// <param name="beginRow">Begin row.</param>
        /// <param name="endRow">End row.</param>
        /// <param name="tbDrugsNoticeUtils">Tb drugs notice utils.</param>
        public List<TbDrugsNoticeUtils> qryDrugsNoticeGrid(int beginRow, int endRow, string loginType, string loginUser, 
            string loginTitle, TbDrugsNoticeUtils tbDrugsNoticeUtils){
            List<TbDrugsNoticeUtils> gridList = new List<TbDrugsNoticeUtils>();
            string sql = getSelectSql("TbDrugsNoticeSqlProvider", "qryDrugsNotice", qryNotSetupChWhereString(loginType, tbDrugsNoticeUtils));
            if(loginType == "1" && !string.IsNullOrEmpty(loginTitle)){
                if(loginTitle != "7" && loginTitle != "8"){
                    sql += " and exists (select * from TB_CH_GROUP cg where cg.NOTICE_SNO = dn.SNO and cg.USER_ID = dn.USER_ID and cg.USER_ACCOUNT = @userAccount) ";
                }
            }
            if(loginType == "3"){
                sql += " and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ";
            }

            sql = this.getSelectSql("TbDrugsNoticeSqlProvider","relativeAuthUser") + " select * from (" + sql + ") new_table where ROW_ID >= " + beginRow + " and ROW_ID <= " + endRow;
            var resultList = QueryTableListBySql(sql, new{
                isMajorCase = tbDrugsNoticeUtils.isMajorCase,
                isSetupCH = tbDrugsNoticeUtils.isSetupCh,
                stuName = "%"+tbDrugsNoticeUtils.stuName+"%",
                idn = tbDrugsNoticeUtils.idn,
                stuIdNo = tbDrugsNoticeUtils.stuIdNo,
                school = "%" + tbDrugsNoticeUtils.school + "%",
                isAgain = tbDrugsNoticeUtils.isAgain,
                isGivenPeople = tbDrugsNoticeUtils.isGivenPeople,
                noticeStatus = tbDrugsNoticeUtils.noticeStatus,
                isWrityComplet = tbDrugsNoticeUtils.isWrityComplet,
                userId = tbDrugsNoticeUtils.userId,
                countyId = tbDrugsNoticeUtils.countyId,
                noticeSchedule = tbDrugsNoticeUtils.noticeSchedule,
                counselingStatus = tbDrugsNoticeUtils.counselingStatus,
                counselingTrack = tbDrugsNoticeUtils.counselingTrack,
                flowStatus = tbDrugsNoticeUtils.flowStatus,
                schoolSystemSno = tbDrugsNoticeUtils.schoolSystemSno,
                userAccount = loginUser
            });
            foreach (var row in resultList){
                TbDrugsNoticeUtils bean = new TbDrugsNoticeUtils();
                bean.rowId = Int32.Parse(row.ROW_ID.ToString());
                bean.sno = Int32.Parse(row.SNO.ToString());
                bean.idn = row.IDN.ToString();
                bean.stuName = row.STU_NAME;
                bean.sex = row.SEX;
                bean.sexStr = row.SEX_STR;
                bean.school = row.SCHOOL;
                bean.isAgain = row.IS_AGAIN;
                bean.isAgainStr = row.IS_AGAIN_STR;
                bean.isGivenPeople = row.IS_GIVEN_PEOPLE;
                bean.isGivenPeopleStr = row.IS_GIVEN_PEOPLE_STR;
                bean.isWrityComplet = row.IS_WRITY_COMPLET;
                bean.isWrityCompletStr = row.IS_WRITY_COMPLET_STR;
                bean.isMajorCase = row.IS_MAJOR_CASE;
                bean.counselingStatus = row.COUNSELING_STATUS;
                bean.counselingStatusStr = row.COUNSELING_STATUS_STR;
                bean.noticeSchedule = row.NOTICE_SCHEDULE;
                bean.noticeScheduleStr = row.NOTICE_SCHEDULE_STR;
                if(row.FLOW_SNO != null){
                    bean.flowSno = row.FLOW_SNO;
                }
                bean.flowStatus = row.FLOW_STATUS;
                bean.flowStatusStr = row.FLOW_STATUS_STR;
                bean.noticeStatus = row.NOTICE_STATUS;
                bean.noticeStatusStr = row.NOTICE_STATUS_STR;
                bean.noticeReason = row.NOTICE_REASON;
                bean.stuIdNo = row.STU_ID_NO;
                gridList.Add(bean);
            }
            return gridList;
        }

        /// <summary>
        /// Qries the drugs notice count.
        /// </summary>
        /// <returns>The drugs notice count.</returns>
        /// <param name="tbDrugsNoticeUtils">Tb drugs notice utils.</param>
        /// <param name="isSetupCH">Is setup ch.</param>
        public int qryDrugsNoticeCount(string loginType, string loginUser,
            string loginTitle, TbDrugsNoticeUtils tbDrugsNoticeUtils){
            int count = 0;
            string sql = getSelectSql("TbDrugsNoticeSqlProvider", "qryDrugsNoticeCount", qryNotSetupChWhereString(loginType, tbDrugsNoticeUtils));
            if(loginType == "1" && !string.IsNullOrEmpty(loginTitle)){
                if(loginTitle != "7" && loginTitle != "8"){
                    sql += " and exists (select * from TB_CH_GROUP cg where cg.NOTICE_SNO = dn.SNO and cg.USER_ID = dn.USER_ID and cg.USER_ACCOUNT = @userAccount) ";
                }
            }
            if(loginType == "3"){
                sql += " and dn.USER_ID in (select RELATIVE_USER_ID from user_relative) ";
            }
            sql = this.getSelectSql("TbDrugsNoticeSqlProvider", "relativeAuthUser") + " " + sql;
            count = this.QueryTableFirstBySql<int>(sql, new{
                isMajorCase = tbDrugsNoticeUtils.isMajorCase,
                isSetupCH = tbDrugsNoticeUtils.isSetupCh,
                stuName = "%"+tbDrugsNoticeUtils.stuName+"%",
                idn = tbDrugsNoticeUtils.idn,
                stuIdNo = tbDrugsNoticeUtils.stuIdNo,
                school = "%" + tbDrugsNoticeUtils.school + "%",
                isAgain = tbDrugsNoticeUtils.isAgain,
                isGivenPeople = tbDrugsNoticeUtils.isGivenPeople,
                noticeStatus = tbDrugsNoticeUtils.noticeStatus,
                isWrityComplet = tbDrugsNoticeUtils.isWrityComplet,
                userId = tbDrugsNoticeUtils.userId,
                countyId = tbDrugsNoticeUtils.countyId,
                noticeSchedule = tbDrugsNoticeUtils.noticeSchedule,
                counselingStatus = tbDrugsNoticeUtils.counselingStatus,
                counselingTrack = tbDrugsNoticeUtils.counselingTrack,
                flowStatus = tbDrugsNoticeUtils.flowStatus,
                schoolSystemSno = tbDrugsNoticeUtils.schoolSystemSno,
                userAccount = loginUser
            });
            return count;
        }

        /// <summary>
        /// Qries the drugs notice by sno.
        /// </summary>
        /// <returns>The drugs notice by sno.</returns>
        /// <param name="sno">Sno.</param>
        public TbDrugsNotice qryDrugsNoticeBySno(int sno){
            TbDrugsNotice tbDrugsNotice = new TbDrugsNotice();
            string sql = getSelectSql("TbDrugsNoticeSqlProvider", "selectTbDrugsNotice", "where SNO = @sno");
            sql = "select tmp_table.*, (select ua.SCHOOL from TB_USER_DATA ua where ua.USER_ID = tmp_table.USER_ID) as NOTICE_SCHOOL from (" + sql + ") tmp_table";
            var resultList = QueryTableListBySql(sql, new {sno=sno});
            foreach(var row in resultList){
                tbDrugsNotice.sno = Int32.Parse(row.SNO.ToString());
                tbDrugsNotice.idn = Int32.Parse(row.IDN.ToString());
                tbDrugsNotice.userId = row.USER_ID;
                tbDrugsNotice.noticeSchool = row.NOTICE_SCHOOL;
                tbDrugsNotice.eventSchoolSystem = Int32.Parse(row.EVENT_SCHOOL_SYSTEM.ToString());
                tbDrugsNotice.eventCategory = row.EVENT_CATEGORY;
                tbDrugsNotice.eventCategoryName = row.EVENT_CATEGORY_NAME;
                tbDrugsNotice.counselingStatus = row.COUNSELING_STATUS;
                tbDrugsNotice.isAgain = row.IS_AGAIN;
                tbDrugsNotice.eventReportTime = row.EVENT_REPORT_TIME;
                tbDrugsNotice.noticeSchedule = row.NOTICE_SCHEDULE;
                tbDrugsNotice.isWrithComplet = row.IS_WRITY_COMPLET;
                tbDrugsNotice.isGivenPeople = row.IS_GIVEN_PEOPLE;
                tbDrugsNotice.flowStatus = row.FLOW_STATUS;
                tbDrugsNotice.flowAccount = row.FLOW_ACCOUNT;
                if(row.FLOW_SNO != null){
                    tbDrugsNotice.flowSno = row.FLOW_SNO;
                }
                tbDrugsNotice.isSetupCh = row.IS_SETUP_CH;
                tbDrugsNotice.isMajorCase = row.IS_MAJOR_CASE;
                tbDrugsNotice.noSetupRemark = row.NO_SETUP_REMARK;
                tbDrugsNotice.noSetupRemarkOther = row.NO_SETUP_REMARK_OTHER;
                if(row.NO_SETUP_REMARK_DATE != null){
                    tbDrugsNotice.noSetupRemarkDate = row.NO_SETUP_REMARK_DATE;
                }
                tbDrugsNotice.noticeStatus = row.NOTICE_STATUS;
                tbDrugsNotice.noticeStatusStr = row.NOTICE_STATUS_STR;
                tbDrugsNotice.noticeReason = row.NOTICE_REASON;
            }
            return tbDrugsNotice;
        }

        public TbDrugsNotice qryDrugsNoticeByFlowSno(int flowSno){
            TbDrugsNotice tbDrugsNotice = new TbDrugsNotice();
            string sql = getSelectSql("TbDrugsNoticeSqlProvider", "selectTbDrugsNotice", "where FLOW_SNO = @flowSno");
            sql = "select tmp_table.*, (select ua.SCHOOL from TB_USER_DATA ua where ua.USER_ID = tmp_table.USER_ID) as NOTICE_SCHOOL from (" + sql + ") tmp_table";
            var resultList = QueryTableListBySql(sql, new {flowSno=flowSno});
            foreach(var row in resultList){
                tbDrugsNotice.sno = Int32.Parse(row.SNO.ToString());
                tbDrugsNotice.idn = Int32.Parse(row.IDN.ToString());
                tbDrugsNotice.userId = row.USER_ID;
                tbDrugsNotice.noticeSchool = row.NOTICE_SCHOOL;
                tbDrugsNotice.eventSchoolSystem = Int32.Parse(row.EVENT_SCHOOL_SYSTEM.ToString());
                tbDrugsNotice.eventCategory = row.EVENT_CATEGORY;
                tbDrugsNotice.eventCategoryName = row.EVENT_CATEGORY_NAME;
                tbDrugsNotice.counselingStatus = row.COUNSELING_STATUS;
                tbDrugsNotice.isAgain = row.IS_AGAIN;
                tbDrugsNotice.eventReportTime = row.EVENT_REPORT_TIME;
                tbDrugsNotice.noticeSchedule = row.NOTICE_SCHEDULE;
                tbDrugsNotice.isWrithComplet = row.IS_WRITY_COMPLET;
                tbDrugsNotice.isGivenPeople = row.IS_GIVEN_PEOPLE;
                tbDrugsNotice.flowStatus = row.FLOW_STATUS;
                tbDrugsNotice.flowAccount = row.FLOW_ACCOUNT;
                if(row.FLOW_SNO != null){
                    tbDrugsNotice.flowSno = row.FLOW_SNO;
                }
                tbDrugsNotice.isSetupCh = row.IS_SETUP_CH;
                tbDrugsNotice.isMajorCase = row.IS_MAJOR_CASE;
                tbDrugsNotice.noSetupRemark = row.NO_SETUP_REMARK;
                tbDrugsNotice.noSetupRemarkOther = row.NO_SETUP_REMARK_OTHER;
                if(row.NO_SETUP_REMARK_DATE != null){
                    tbDrugsNotice.noSetupRemarkDate = DateTime.Parse(row.NO_SETUP_REMARK_DATE);
                }
                tbDrugsNotice.noticeStatus = row.NOTICE_STATUS;
                tbDrugsNotice.noticeStatusStr = row.NOTICE_STATUS_STR;
                tbDrugsNotice.noticeReason = row.NOTICE_REASON;
            }
            return tbDrugsNotice;
        }

        public int updDrugsNotice(TbDrugsNotice tbDrugsNotice, bool isNoSetup){
            int count = 0;
            StringBuilder updateColumn = new StringBuilder();
            if(isNoSetup){
                updateColumn.Append(" NO_SETUP_REMARK = @NO_SETUP_REMARK, ");
                updateColumn.Append(" NO_SETUP_REMARK_OTHER = @NO_SETUP_REMARK_OTHER, ");
                updateColumn.Append(" NO_SETUP_REMARK_DATE = @NO_SETUP_REMARK_DATE, ");
            }   
            string sql = this.getUpdateSql("TbDrugsNoticeSqlProvider", "updateTbDrugsNotice", updateColumn.ToString());
            count = this.ExecuteTableBySql(sql, mapping(tbDrugsNotice));
            return count;
        }

        private object mapping(TbDrugsNotice tbDrugsNotice) => new
        {
            SNO = tbDrugsNotice.sno,
            IDN = tbDrugsNotice.idn,
            USER_ID = tbDrugsNotice.userId,
            EVENT_SCHOOL_SYSTEM = tbDrugsNotice.eventSchoolSystem,
            EVENT_CATEGORY = tbDrugsNotice.eventCategory,
            EVENT_CATEGORY_NAME = tbDrugsNotice.eventCategoryName,
            COUNSELING_STATUS = tbDrugsNotice.counselingStatus,
            IS_AGAIN = tbDrugsNotice.isAgain,
            EVENT_REPORT_TIME = tbDrugsNotice.eventReportTime,
            NOTICE_SCHEDULE = tbDrugsNotice.noticeSchedule,
            IS_WRITY_COMPLET = tbDrugsNotice.isWrithComplet,
            IS_GIVEN_PEOPLE = tbDrugsNotice.isGivenPeople,
            FLOW_STATUS = tbDrugsNotice.flowStatus,
            FLOW_ACCOUNT = tbDrugsNotice.flowAccount,
            FLOW_SNO = tbDrugsNotice.flowSno,
            IS_SETUP_CH = tbDrugsNotice.isSetupCh,
            IS_MAJOR_CASE =tbDrugsNotice.isMajorCase,
            NO_SETUP_REMARK = tbDrugsNotice.noSetupRemark,
            NO_SETUP_REMARK_OTHER = tbDrugsNotice.noSetupRemarkOther,
            NO_SETUP_REMARK_DATE = tbDrugsNotice.noSetupRemarkDateStr,
            NOTICE_STATUS = tbDrugsNotice.noticeStatus,
            NOTICE_STATUS_STR = tbDrugsNotice.noticeStatusStr,
            NOTICE_REASON = tbDrugsNotice.noticeReason,
            CR_DATE = tbDrugsNotice.crDate,
            UP_IP = tbDrugsNotice.upIp,
            UP_USER = tbDrugsNotice.upUser
        };

        private string qryNotSetupChWhereString(string loginType, TbDrugsNoticeUtils tbDrugsNoticeUtils){
            StringBuilder whereString = new StringBuilder();
            whereString.Append(" where 1 = 1 ");
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.isMajorCase)){
                whereString.Append(" and dn.IS_MAJOR_CASE = @isMajorCase ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.isSetupCh)){
                whereString.Append(" and dn.IS_SETUP_CH = @isSetupCH ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.stuName)){
                whereString.Append(" and dn.STU_NAME like @stuName ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.idn)){
                whereString.Append(" and dn.IDN = @idn ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.stuIdNo)){
                whereString.Append(" and dn.STU_ID_NO = @stuIdNo ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.eventReportBeginDateStr)){
                string beginDate = tbDrugsNoticeUtils.eventReportBeginDateStr + " 00:00:00";
                whereString.Append(" and dn.EVENT_REPORT_TIME >= convert(DATETIME, '" + beginDate + "', 120) ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.eventReportEndDateStr)){
                string endDate = tbDrugsNoticeUtils.eventReportEndDateStr + " 23:59:59";
                whereString.Append(" and dn.EVENT_REPORT_TIME <= convert(DATETIME, '" + endDate + "', 120) ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.counselingBeginDateStr)){
                string beginDate = tbDrugsNoticeUtils.counselingBeginDateStr + " 00:00:00";
                whereString.Append(" and dn.EVENT_REPORT_TIME >= convert(DATETIME, '" + beginDate + "', 120) ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.counselingEndDateStr)){
                string endDate = tbDrugsNoticeUtils.counselingEndDateStr + " 23:59:59";
                whereString.Append(" and dn.EVENT_REPORT_TIME <= convert(DATETIME, '" + endDate + "', 120) ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.closeMeetBeginDateStr)){
                string beginDate = tbDrugsNoticeUtils.closeMeetBeginDateStr + " 00:00:00";
                whereString.Append(" and dn.EVENT_REPORT_TIME >= convert(DATETIME, '" + beginDate + "', 120) ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.closeMeetEndDateStr)){
                string endDate = tbDrugsNoticeUtils.closeMeetEndDateStr + " 23:59:59";
                whereString.Append(" and dn.EVENT_REPORT_TIME <= convert(DATETIME, '" + endDate + "', 120) ");
            }

            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.isAgain)){
                whereString.Append(" and dn.IS_AGAIN = @isAgain ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.isGivenPeople)){
                whereString.Append(" and dn.IS_GIVEN_PEOPLE = @isGivenPeople ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.noticeStatus)){
                whereString.Append(" and dn.NOTICE_STATUS = @noticeStatus ");
            }
            if (!string.IsNullOrEmpty(tbDrugsNoticeUtils.isWrityComplet)){
                whereString.Append(" and dn.IS_WRITY_COMPLET = @isWrityComplet ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.countyId)){
                whereString.Append(" and dn.COUNTY_ID = @countyId ");
            }
            if(loginType != "3"){
               if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.userId)){
                    whereString.Append(" and dn.USER_ID = @userId ");
                } 
            }
            if (tbDrugsNoticeUtils.schoolSystemSno > 0){
                whereString.Append(" and dn.SCHOOL_SYSTEM_SNO = @schoolSystemSno ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.school)){
                whereString.Append(" and dn.SCHOOL like @school ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.noticeSchedule)){
                whereString.Append(" and dn.NOTICE_SCHEDULE = @noticeSchedule ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.flowStatus)){
                whereString.Append(" and dn.FLOW_STATUS = @flowStatus ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.counselingStatus)){
                whereString.Append(" and dn.COUNSELING_STATUS = @counselingStatus ");
            }
            if(!string.IsNullOrEmpty(tbDrugsNoticeUtils.counselingTrack)){
                whereString.Append(" and dn.COUNSELING_TRACK = @counselingTrack ");
            }
            return whereString.ToString();
        }

        public List<TbDrugsNotice> qryDrugsNoticeByStatusE(int beginRow, int endRow, string idn, string noticeSchool){
            List<TbDrugsNotice> errBullList = new List<TbDrugsNotice>();
            StringBuilder whereString = new StringBuilder();
            if(!string.IsNullOrEmpty(idn)){
                whereString.Append("where IDN = @IDN");
            }
            if(!string.IsNullOrEmpty(noticeSchool)){
                if(whereString.Length > 0){
                    whereString.Append("and ");
                }else{
                    whereString.Append("where ");
                }
                whereString.Append(" NOTICE_SCHOOL like @NOTICE_SCHOOL");
            }
            string sql = this.getSelectSql("TbDrugsNoticeSqlProvider","qryDrugsNoticeByStatusE",whereString.ToString());
            sql = "select * from ("+sql+") GRID_TABLE where ROW_ID >= " + beginRow + " and ROW_ID <= " + endRow;
            var resultList = QueryTableListBySql(sql, new { IDN = idn, NOTICE_SCHOOL = noticeSchool });
            foreach(var row in resultList){
                TbDrugsNotice notice = new TbDrugsNotice();
                notice.sno = row.SNO;
                notice.idn = row.IDN;
                notice.userId = row.USER_ID;
                notice.noticeSchool = row.NOTICE_SCHOOL;
                notice.eventSchoolSystem = row.EVENT_SCHOOL_SYSTEM;
                notice.eventSchoolSystemStr = row.EVENT_SCHOOL_SYSTEM_STR;
                notice.eventCategory = row.EVENT_CATEGORY;
                notice.eventCategoryName = row.EVENT_CATEGORY_NAME;
                notice.noticeStatus = row.NOTICE_STATUS;
                notice.noticeReason = row.NOTICE_REASON;
                notice.stuName = row.STU_NAME;
                notice.sexStr = row.SEX_STR;
                errBullList.Add(notice);
            }
            return errBullList;
        }

        public int qryDrugsNoticeByStatusECount(string idn, string noticeSchool){
            int count = 0;
            StringBuilder whereString = new StringBuilder();
            if(!string.IsNullOrEmpty(idn)){
                whereString.Append("where IDN = @IDN");
            }
            if(!string.IsNullOrEmpty(noticeSchool)){
                if(whereString.Length > 0){
                    whereString.Append("and ");
                }else{
                    whereString.Append("where ");
                }
                whereString.Append(" NOTICE_SCHOOL like @NOTICE_SCHOOL");
            }
            string sql = this.getSelectSql("TbDrugsNoticeSqlProvider","qryDrugsNoticeByStatusE",whereString.ToString());
            sql = "select count(*) from ("+sql+") GRID_TABLE ";
            count = this.QueryTableFirstBySql<int>(sql, new { IDN = idn, NOTICE_SCHOOL = noticeSchool });
            return count;
        }

        public List<TbDrugsNoticeUtils> qryStuAgainRecord(int noticeSno, string stuIdNo){
            List<TbDrugsNoticeUtils> result = new List<TbDrugsNoticeUtils>();
            string sql = this.getSelectSql("TbDrugsNoticeSqlProvider", "qryStuAgainRecord", "where SNO <> @noticeSno and STU_ID_NO = @stuIdNo");
            var resultList = QueryTableListBySql(sql, new { noticeSno=noticeSno, stuIdNo = stuIdNo });
            foreach(var row in resultList){
                TbDrugsNoticeUtils tbDrugsNoticeUtils = new TbDrugsNoticeUtils();
                tbDrugsNoticeUtils.idn = row.IDN;
                tbDrugsNoticeUtils.stuName = row.STU_NAME;
                tbDrugsNoticeUtils.school = row.SCHOOL;
                result.Add(tbDrugsNoticeUtils);
            }
            return result;
        }

        public int qryStuIsAgain(int noticeSno, string stuIdNo){
            int count = 0;
            string sql = this.getSelectSql("TbDrugsNoticeSqlProvider", "qryStuAgainRecordCount", "where SNO <> @noticeSno and STU_ID_NO = @stuIdNo");
            count = this.QueryTableFirstBySql<int>(sql, new { noticeSno = noticeSno, stuIdNo = stuIdNo });
            return count;
        }

        public string qryLoginMsgByAdmin(string loginType, string loginUser){
            string result = "";
            string whereString = "";
            if(loginType == "3"){
                whereString += " and USER_ID in (select RELATIVE_USER_ID from user_relative) ";
            }
            string sql = this.getSelectSql("TbDrugsNoticeSqlProvider", "relativeAuthUser") + " " + this.getSelectSql("TbDrugsNoticeSqlProvider", "loginMsgByAdmin", whereString);
            result = this.QueryTableFirstBySql<string>(sql, new { userId = loginUser });
            return result;
        }

        public string qryLoginMsg(string loginUser){
            string result = "";
            result = this.QueryTableFirst<string>("TbDrugsNoticeSqlProvider", "loginMsg", new { userId = loginUser});
            return result;
        }

        public int updDrugsNoticeFromSpcf(string userId, string stuIdCord, string upIp, string upUser, string isGivenPeople){
            int count = 0;
            count = this.UpdateTable("TbDrugsNoticeSqlProvider", "updDrugsNoticeFromSpcf", new { userId = userId, stuIdCord = stuIdCord, upIp = upIp, upUser = upUser, isGivenPeople= isGivenPeople });
            return count;
        }
    }
}