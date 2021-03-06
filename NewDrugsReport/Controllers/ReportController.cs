﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CityinfoCommon;
using NewDrugs.Base;
using NewDrugs.Models;
using NewDrugs.Service;
using Newtonsoft.Json;
using NLog;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NewDrugs.Helper;
using NPOI.SS.Util;

namespace NewDrugsReport.Controllers
{
    public class ReportController : BaseController
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private CommonService commService = new CommonService();
        private ReportService service = new ReportService();

        [HttpPost]
        public ActionResult Index(){
            ViewBag.Title = "藥物濫用學生個案輔導追蹤管理系統--報表管理";
            var loginUserInfo = this.getLoginUser();
            ViewBag.loginType = loginUserInfo.loginType.ToString();
            ViewBag.edulvList = commService.qryCommonByList("EDULV");
            ViewBag.schoolSystemList = commService.qryCommonByList("SSNO");
            ViewBag.counselingStatusList = commService.qryCommonByList("COLS");
            ViewBag.usrsList = commService.qryCommonByList("USRS");
            ViewBag.dgetList = commService.qryCommonByList("DGET");
            ViewBag.dgonList = commService.qryCommonByList("DGON");
            ViewBag.rptyList = commService.qryCommonByList("RPTY");
            return View();
        }
        [HttpPost]
        public JsonResult dynamicReportByGrid(int page, int pageSize, TbDrugsNoticeUtils tbDrugsNoticeUtils){
            var loginUserInfo = this.getLoginUser();
            string status = "success", msg = "執行成功";
            GridModel gridModel = new GridModel();
            gridModel = service.getDynamicReportByGrid(page, pageSize, tbDrugsNoticeUtils, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());
            if (gridModel.rowNum == 0){
                status = "error";
                msg = "查無資料";
            }
            return Json(new { status = status, msg = msg, data = gridModel, token = new JwtUtils().EnCodeJwt(loginUserInfo) });
        }

        [HttpPost]
        public FileResult expDynamicReport(TbDrugsNoticeUtils tbDrugsNoticeUtils, List<string> needColumn){
            var loginUserInfo = this.getLoginUser();
            MemoryStream ms = new MemoryStream();
            IWorkbook xlsx = new XSSFWorkbook();
            ISheet sheet = xlsx.CreateSheet("動態一覽表");
            IRow xlsxRow = sheet.CreateRow(0);

            List<TbDrugsNoticeUtils> dataList = service.getDynamicReportByList(tbDrugsNoticeUtils, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());
            List<string> titleList = new List<string>();
            List<string> columnList = new List<string>();
            foreach(string json in needColumn){
                Dictionary<string, string> map = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                columnList.Add(map["column"]);
                titleList.Add(map["columnValue"]);
            }
            int colI = 0, rowI = 1;
            foreach (string rowTitle in titleList){
                ICell cell = xlsxRow.CreateCell(colI);
                cell.SetCellType(CellType.String);
                cell.SetCellValue(rowTitle);
                sheet.AutoSizeColumn(colI);
                colI++;
            }

            foreach (TbDrugsNoticeUtils row in dataList){
                string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(row);
                Dictionary<string, string> map = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                xlsxRow = sheet.CreateRow(rowI);
                for (int idx = 0; idx < columnList.Count; idx++){
                    createCell(xlsxRow, idx, CellType.String, map[columnList[idx]]);
                    sheet.AutoSizeColumn(idx);
                }
                rowI++;
            }
            xlsx.Write(ms);
            return File(ms.ToArray(), "application/unknown", DateTime.Now.ToString("yyyyMMdd_") + "動態一覽表.xlsx");
        }

        [HttpPost]
        public FileResult expOtherReport(string reportType, 
            string beginYear, string beginMonth, string endYear, string endMonth, string schoolSystemSno){
            var loginUserInfo = this.getLoginUser();
            MemoryStream ms = new MemoryStream();
            List<dynamic> dataList = new List<dynamic>();
            string reportName = commService.qryCommValue("RPTY", reportType);
            try{
                string filePath = Server.MapPath("~/Content/ExcelTemplate/"+ reportName+".xlsx");
                if (reportType == "1"){
                    using(Stream iStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                        IWorkbook xlsx = new XSSFWorkbook(iStream);
                        ISheet sheet = xlsx.GetSheetAt(0);
                        IRow xlsxRow = sheet.GetRow(0);
                        reportName = (Int32.Parse(beginYear) - 1911).ToString() + "年" + beginMonth + "月_"+ reportName;
                        xlsxRow.GetCell(0).SetCellValue(reportName);
                        ICellStyle sampleStyle = xlsx.CreateCellStyle();
                        sampleStyle.BorderTop = BorderStyle.Thin;
                        sampleStyle.BorderBottom = BorderStyle.Thin;
                        sampleStyle.BorderRight = BorderStyle.Thin;
                        sampleStyle.BorderLeft = BorderStyle.Thin;
                        generatorXlsx(sheet, sampleStyle, reportName, 4, dataList);

                        int colsTotal = service.getAllPeopleCount(loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString(), "12");
                        sheet.GetRow(31).GetCell(0).SetCellValue("年度累計濫用藥物學生人數：" + colsTotal.ToString());
                        int colsSucc = service.getAllPeopleCount(loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString(),"1");
                        sheet.GetRow(32).GetCell(0).SetCellValue("年度累計輔導成功學生人數：" + colsSucc.ToString());
                        int colsCont = service.getAllPeopleCount(loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString(), "13");
                        sheet.GetRow(33).GetCell(0).SetCellValue("目前繼續輔導學生人數：" + colsCont.ToString());
                        double p = Math.Round(((double)colsSucc / colsTotal) * 100);
                        sheet.GetRow(34).GetCell(0).SetCellValue("輔導成功率%："+ p.ToString() + "%");
                        sheet.GetRow(35).GetCell(0).SetCellValue("表內年度濫用藥物學生人數及輔導轉化學生人數，係民國"+ (Int32.Parse(beginYear) - 1911).ToString() + "年"+ beginMonth + "月之統計數據。(以excel表製作)");

                        xlsx.Write(ms);
                    }
                }else if(reportType == "2"){
                    using (Stream iStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                        IWorkbook xlsx = new XSSFWorkbook(iStream);
                        ISheet sheet = xlsx.GetSheetAt(0);
                        IRow xlsxRow = sheet.GetRow(0);
                        reportName = (Int32.Parse(beginYear) - 1911).ToString() + "年~" + (Int32.Parse(endYear) - 1911).ToString() + "年_"+ reportName;
                        xlsxRow.GetCell(0).SetCellValue(reportName);
                        ICellStyle sampleStyle = xlsx.CreateCellStyle();
                        sampleStyle.BorderDiagonalLineStyle = BorderStyle.Thin;
                        dataList = service.getPeopleAmountByDrugsLv(beginYear, endYear, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());
                        sampleStyle.BorderTop = BorderStyle.Thin;
                        sampleStyle.BorderBottom = BorderStyle.Thin;
                        sampleStyle.BorderRight = BorderStyle.Thin;
                        sampleStyle.BorderLeft = BorderStyle.Thin;
                        generatorXlsx(sheet, sampleStyle, reportName, 2, dataList);
                        xlsx.Write(ms);
                    }
                }
                else if(reportType == "3"){
                    IWorkbook xlsx = new XSSFWorkbook();
                    ISheet sheet = xlsx.CreateSheet();
                    sheet.CreateRow(0);
                    IRow xlsxRow = sheet.CreateRow(1);
                    ICellStyle sampleStyle = xlsx.CreateCellStyle();
                    sampleStyle.BorderTop = BorderStyle.Thin;
                    sampleStyle.BorderBottom = BorderStyle.Thin;
                    sampleStyle.BorderRight = BorderStyle.Thin;
                    sampleStyle.BorderLeft = BorderStyle.Thin;

                    reportName = (Int32.Parse(beginYear) - 1911).ToString() + "年~" + (Int32.Parse(endYear) - 1911).ToString() + "年_" + reportName;
                    dataList = service.getStuUseDrugs(beginYear, endYear, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());
                    Dictionary<string, object> map = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(dataList[0]));
                    int cellI = 0;
                    foreach (string key in map.Keys.ToList()){
                        createCell(xlsxRow, cellI, CellType.String, key);
                        cellI++;
                    }
                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, cellI - 1));
                    sheet.GetRow(0).CreateCell(0).CellStyle = sampleStyle;
                    sheet.GetRow(0).GetCell(0).SetCellValue(reportName);

                    generatorXlsx(sheet, sampleStyle, reportName, 2, dataList);
                    xlsx.Write(ms);

                }else if(reportType == "B"){
                    using (Stream iStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                        IWorkbook xlsx = new XSSFWorkbook(iStream);
                        ISheet sheet = xlsx.GetSheetAt(0);
                        reportName = (Int32.Parse(beginYear) - 1911).ToString() + "年" + beginMonth + "月_" + reportName;
                        dataList = service.getSpcfCategoryCount(beginYear, beginMonth, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());
                        ICellStyle sampleStyle = xlsx.CreateCellStyle();
                        sampleStyle.BorderTop = BorderStyle.Thin;
                        sampleStyle.BorderBottom = BorderStyle.Thin;
                        sampleStyle.BorderRight = BorderStyle.Thin;
                        sampleStyle.BorderLeft = BorderStyle.Thin;
                        generatorXlsx(sheet, sampleStyle, reportName, 4, dataList);
                        xlsx.Write(ms);
                    }
                }else if (reportType == "C") {
                    using (Stream iStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                        IWorkbook xlsx = new XSSFWorkbook(iStream);
                        ISheet sheet = xlsx.GetSheetAt(0);
                        ICellStyle sampleStyle = sheet.GetRow(7).GetCell(0).CellStyle;
                        reportName = (Int32.Parse(beginYear) - 1911).ToString() + "年" + beginMonth + "月_" + reportName;
                        dataList = service.getSpcfCategoryList(beginYear, beginMonth, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());
                        generatorXlsx(sheet, sampleStyle, reportName, 8, dataList);
                        xlsx.Write(ms);
                    }
                }else if(reportType == "D"){
                    using (Stream iStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read)){
                        IWorkbook xlsx = new XSSFWorkbook(iStream);
                        ISheet sheet = xlsx.GetSheetAt(0);
                        ICellStyle sampleStyle = xlsx.CreateCellStyle();
                        sampleStyle.BorderTop = BorderStyle.Thin;
                        sampleStyle.BorderBottom = BorderStyle.Thin;
                        sampleStyle.BorderRight = BorderStyle.Thin;
                        sampleStyle.BorderLeft = BorderStyle.Thin;
                        
                        reportName = (Int32.Parse(beginYear) - 1911).ToString() + "年" + beginMonth + "月_" + reportName;
                        //未填校數統計
                        dataList = service.getUndeclaredCount(beginYear, beginMonth, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());
                        generatorXlsx(sheet, sampleStyle, "", 1, dataList);
                        //未填校數清單
                        sheet = xlsx.GetSheetAt(1);
                        dataList = service.getUndeclaredList(beginYear, beginMonth, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());
                        generatorXlsx(sheet, sampleStyle, "", 1, dataList);
                        xlsx.Write(ms);
                    }
                }
                else if (reportType == "K")//案件統計暨審查表
                {
                    using (Stream iStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        IWorkbook xlsx = new XSSFWorkbook(iStream);
                        ISheet sheet = xlsx.GetSheetAt(0);
                        ICellStyle sampleStyle = xlsx.CreateCellStyle();
                        sampleStyle.BorderTop = BorderStyle.Thin;
                        sampleStyle.BorderBottom = BorderStyle.Thin;
                        sampleStyle.BorderRight = BorderStyle.Thin;
                        sampleStyle.BorderLeft = BorderStyle.Thin;
                        sampleStyle.VerticalAlignment = VerticalAlignment.Center;
                        sampleStyle.Alignment = HorizontalAlignment.Center;
                        sampleStyle.BorderDiagonal = BorderDiagonal.None;
                        sampleStyle.BorderDiagonalLineStyle = BorderStyle.None;
                        sampleStyle.WrapText = true;
                        // 設定字體
                        IFont Font = xlsx.CreateFont();   // 產生字體樣式設定
                        Font.FontName = "標楷體";
                        Font.FontHeightInPoints = 12;

                        sampleStyle.SetFont(Font);

                        ICellStyle slashStyle = xlsx.CreateCellStyle();
                        slashStyle.BorderTop = BorderStyle.Thin;
                        slashStyle.BorderBottom = BorderStyle.Thin;
                        slashStyle.BorderRight = BorderStyle.Thin;
                        slashStyle.BorderLeft = BorderStyle.Thin;
                        slashStyle.VerticalAlignment = VerticalAlignment.Center;
                        slashStyle.Alignment = HorizontalAlignment.Center;
                        slashStyle.BorderDiagonal = BorderDiagonal.Backward;
                        slashStyle.BorderDiagonalLineStyle = BorderStyle.Thin;
                        slashStyle.SetFont(Font);

                        reportName = (Int32.Parse(beginYear) - 1911).ToString() + "年" + beginMonth + "月_" + (Int32.Parse(endYear) - 1911).ToString() + "年" + endMonth +"月_"+ reportName;
                        //統計表
                        List<SpcItem> spcItemList = new List<SpcItem>();
                        spcItemList = service.GetSpcItem(beginYear, beginMonth, endYear, endMonth, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());
                        generatorSpcItemXlsx(sheet, sampleStyle, slashStyle, reportName, 3, spcItemList);
                        //推薦表
                        sheet = xlsx.GetSheetAt(1);
                        dataList = service.GetSpcReward(beginYear, beginMonth, endYear, endMonth, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());//test by Frank
                        generatorXlsx(sheet, sampleStyle, "", 3, dataList);
                        xlsx.Write(ms);
                    }
                }
                else if (reportType == "L")//各縣市薦報表
                {
                    using (Stream iStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        IWorkbook xlsx = new XSSFWorkbook(iStream);
                        ISheet sheet = xlsx.GetSheetAt(0);
                        IRow xlsxRow = sheet.GetRow(0);
                        //reportName = "各縣市薦報表";
                        reportName = (Int32.Parse(beginYear) - 1911).ToString() + "年" + beginMonth + "月_" + (Int32.Parse(endYear) - 1911).ToString() + "年" + endMonth + "月_" + reportName;
                        xlsxRow.GetCell(0).SetCellValue(reportName);
                        ICellStyle sampleStyle = xlsx.CreateCellStyle();
                        sampleStyle.BorderDiagonalLineStyle = BorderStyle.Thin;
                        dataList = service.GetCitySpcReward(beginYear, beginMonth, endYear, endMonth, loginUserInfo.loginType.ToString(), loginUserInfo.userId.ToString());//test by Frank
                        sampleStyle.BorderTop = BorderStyle.Thin;
                        sampleStyle.BorderBottom = BorderStyle.Thin;
                        sampleStyle.BorderRight = BorderStyle.Thin;
                        sampleStyle.BorderLeft = BorderStyle.Thin;
                        sampleStyle.VerticalAlignment = VerticalAlignment.Center;
                        sampleStyle.Alignment = HorizontalAlignment.Center;
                        // 設定字體
                        IFont Font = xlsx.CreateFont();   // 產生字體樣式設定
                        Font.FontName = "標楷體";
                        Font.FontHeightInPoints = 12;

                        sampleStyle.SetFont(Font);
                        generatorXlsx(sheet, sampleStyle, reportName, 3, dataList);
                        xlsx.Write(ms);
                    }//add by frank
                }
            }
            catch(Exception e){
                logger.Error(e, e.Message);
                throw e;
            }


            return File(ms.ToArray(), "application/unknown", reportName+".xlsx");
        }

        private void generatorXlsx(ISheet sheet, ICellStyle sampleStyle, string reportName, int rowI, List<dynamic> dataList){
            MemoryStream ms = new MemoryStream();
            try{
                IRow xlsxRow = sheet.GetRow(0);
                xlsxRow.GetCell(0).SetCellValue(reportName);
                foreach (var row in dataList){
                    xlsxRow = sheet.GetRow(rowI);
                    if(xlsxRow == null){
                        xlsxRow = sheet.CreateRow(rowI);
                    }
                    Dictionary<string, object> map = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(row));
                    int cellI = 0;
                    foreach (string key in map.Keys.ToList()){
                        if(map[key] is null)//20181011 Frank避免null exception
                        {
                            map[key] = "";
                        }
                        if (this.IsNumber(map[key])){
                            createCell(xlsxRow, cellI, CellType.Numeric, Int32.Parse(map[key].ToString()), sampleStyle);
                        }else{
                            createCell(xlsxRow, cellI, CellType.String, map[key].ToString(), sampleStyle);
                        }
                        cellI++;
                    }
                    rowI++;
                }
            }catch(Exception e){
                logger.Error(e, e.Message);
                throw e;
            }
        }

        private void generatorSpcItemXlsx(ISheet sheet, ICellStyle sampleStyle, ICellStyle slashStyle, string reportName, int rowI, List<SpcItem> dataList)
        {
            MemoryStream ms = new MemoryStream();
            int noticeSnoRows = 5;//同一NOTICE_SNO的一組, 共5行
            int totalRow = dataList!=null&&dataList.Count>0?dataList[dataList.Count() - 1].rowNum:0;
            
            try
            {
                for (int i = rowI; i < totalRow * noticeSnoRows + rowI; i++)
                {
                    InsertRow(sheet, i);
                    

                    IRow newRow = sheet.GetRow(i);
                    if (newRow == null)
                    {
                        newRow = sheet.CreateRow(i);
                    }
                }

                IRow xlsxRow = sheet.GetRow(0);
                xlsxRow.GetCell(0).SetCellValue(reportName);
                foreach (var row in dataList)
                {
                    
                    
                    Dictionary<string, object> map = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(row));
                    int cellI = 0;
                    if (map["rowNum"] != null&&map["title"]!=null&&convertHelper.MbrRowNum(Int32.Parse(map["title"].ToString()))!=0)
                    {
                        xlsxRow = sheet.GetRow((Int32.Parse(map["rowNum"].ToString()) - 1) * noticeSnoRows + convertHelper.MbrRowNum(Int32.Parse(map["title"].ToString()))+rowI-1);//rowNum*4+mbrType列數+起始列數-1(由零起算)
                        foreach (string key in map.Keys.ToList())
                        {
                            if (map[key] is null)//20181011 Frank避免null exception
                            {
                                map[key] = "";
                            }

                            ICellStyle currentStyle = convertHelper.CellStyleHelper(sampleStyle, slashStyle , Int32.Parse(map["title"].ToString()), key);
                            
                            createCell(xlsxRow, cellI, CellType.String, convertHelper.CellValueHelper(Int32.Parse(map["title"].ToString()),key, map[key].ToString()), currentStyle);
                            
                            cellI++;
                        }
                    }
                }
                //處理row_num, notice_sno, meeting time合併儲存格
                for(int i =0; i<totalRow; i++)
                {
                    int begin = i * noticeSnoRows + rowI;
                    int end = i * noticeSnoRows + rowI + (noticeSnoRows -1);//從begin起算noticeSnoRows-1行=end
                    sheet.AddMergedRegion(new CellRangeAddress(begin, end, 0, 0));//row num
                    sheet.AddMergedRegion(new CellRangeAddress(begin, end, 1, 1));//school
                    sheet.AddMergedRegion(new CellRangeAddress(begin, end, 2, 2));//notice sno
                    sheet.AddMergedRegion(new CellRangeAddress(begin, end, 6, 6));//metting type
                    sheet.AddMergedRegion(new CellRangeAddress(begin, end, 7, 7));//meeting time
                }
                
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                throw e;
            }
        }

        private void createCell(IRow xlsxRow, int cellIdx, CellType cellType, dynamic cellValue, ICellStyle style = null){
            ICell cell = xlsxRow.GetCell(cellIdx);
            if(cell == null ){
                cell = xlsxRow.CreateCell(cellIdx);
                if(style != null){
                    cell.CellStyle = style;
                }
            }
            cell.SetCellType(cellType);
            cell.SetCellValue(cellValue == null ? "" : cellValue);
        }

        /*
         * 插入列
         */
        private void InsertRow(ISheet sheet, int insertRow)
        {
            try
            {
                sheet.ShiftRows(insertRow, sheet.LastRowNum, 1);
                // 如果要插入的列數是0，複制下方列，反之，複制上方列
                //sheet.CopyRow((insertRow == 0) ? insertRow + 1 : insertRow - 1, insertRow);
                // 清空插入列的值
                /*var row = sheet.GetRow(insertRow);
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    var cell = row.GetCell(i);
                    if (cell != null)
                        cell.SetCellValue("");
                }*/
            }
            catch (Exception e)
            {
                logger.Error(e, e.Message);
                throw e;
            }
            
        }
    }
}
