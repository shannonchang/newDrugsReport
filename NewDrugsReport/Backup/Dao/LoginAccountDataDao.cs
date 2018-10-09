using System;
using System.Collections.Generic;
using Dapper;
using NewDrugs.Base;
using NewDrugs.Common;
using NewDrugs.Models;

namespace NewDrugs.Dao
{
    public class LoginAccountDataDao : BaseDao
    {
        public LoginAccountDataDao()
        {
            this.setXml("VwLoginInfoSqlProvider.xml");
        }
        public bool loginUserExists(string account){
            string sql = this.getSelectSql("VwLoginInfoSqlProvider", "chkLoginAccountExists");
            int count = 0;
            count = QueryTableFirstBySql<int>(sql, new { ACCOUNT = account });
            return count > 0;
        }

        public VwLoginInfo qryLoginInfo(string account){
            string sql = this.getSelectSql("VwLoginInfoSqlProvider", "selectLoginAccount");
            List<VwLoginInfo> userList = new List<VwLoginInfo>();
            var resultList = QueryTableListBySql(sql, new { ACCOUNT = account });
            foreach(var row in resultList){
                VwLoginInfo bean = new VwLoginInfo();
                bean.userId = row.USER_ID;
                bean.password = row.PASSWORD;
                bean.account =row.ACCOUNT;
                bean.school = row.SCHOOL;
                bean.schoolSystemSno = row.SCHOOL_SYSTEM_SNO;
                bean.schoolSystemName = row.SCHOOL_SYSTEM_NAME;
                bean.schoolPresident = row.SCHOOL_PRESIDENT;
                bean.name = row.NAME;
                bean.job = row.JOB;
                bean.title = row.TITLE;
                bean.titleName = row.TITLE_NAME;
                bean.phone = row.PHONE;
                bean.email = row.EMAIL;
                bean.countyId = row.COUNTY_ID;
                bean.city = row.CITY;
                bean.road = row.ROAD;
                bean.countyName = row.COUNTY_NAME;
                bean.cityName = row.CITY_NAME;
                bean.roadName = row.ROAD_NAME;
                bean.schoolAddress = row.SCHOOL_ADDRESS;
                bean.loginType = row.LOGIN_TYPE;
                bean.loginTypeName = row.LOGIN_TYPE_NAME;
                bean.status = row.STATUS;
                bean.showFlag = row.SHOW_FLAG;
                userList.Add(bean);
            }
            if(userList.Count == 0){
                return null;
            }
            return userList[0];
        }

        public VwLoginInfo qryLoginAuth(string account)
        {
            string sql = this.getSelectSql("VwLoginInfoSqlProvider", "chkLoginAccountAuth");
            VwLoginInfo bean = new VwLoginInfo();
            var resultList = QueryTableListBySql(sql, new { ACCOUNT = account });
            foreach (var row in resultList)
            {
                bean.loginType = row.LOGIN_TYPE;
            }
            return bean;
        }
    }
}
