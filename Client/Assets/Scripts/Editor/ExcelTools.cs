﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Excel;
using System.Data;

namespace EditorTool
{
    public class ExcelTools {

        /// <summary>
        /// 读取CardInfoArray信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static CardInfo[] CreateCardInfoArrayWithExcel(string filePath)
        {
            int col = 0, row = 0;
            DataRowCollection collect = ReadExcel(filePath, ref col, ref row);
            
            CardInfo[] array = new CardInfo[row - 2];
            for (int i = 2; i<row; i++)
            {
                CardInfo cardInfo = new CardInfo();
                cardInfo.cardID = int.Parse(collect[i][0].ToString());
                cardInfo.locType = LocType.Left;
                cardInfo.type = (CardType)int.Parse(collect[i][2].ToString());
                cardInfo.AlwaysShowCard = false;
                cardInfo.AlwaysShowType = false;
                cardInfo.context = collect[i][3].ToString();
                cardInfo.levelID = int.Parse(collect[i][4].ToString());
                array[i - 2] = cardInfo;
            }
            return array;
        } 
        
        /// <summary>
        /// 读取RoleInfoArray信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static RoleInfo[] CreateRoleInfoArrayWithExcel(string filePath)
        {
            int col = 0, row = 0;
            DataRowCollection collect = ReadExcel(filePath, ref col, ref row);

            RoleInfo[] array = new RoleInfo[row - 1];
            for (int i = 1; i<row; i++)
            {
                RoleInfo info = new RoleInfo();
                info.roleID = int.Parse(collect[i][0].ToString());
                info.roleName = collect[i][1].ToString();
                info.roleDesAddr = collect[i][2].ToString();   
                info.roleHeadPicAddr = collect[i][3].ToString();
                info.rolePicAddr = collect[i][4].ToString();
                array[i - 1] = info;
            }
            return array;
        }

        /// <summary>
        /// 读取CardManagerInfoArray信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static CardManagerInfo[] CreateCardManagerInfoArrayWithExcel(string filePath)
        {
            int col = 0, row = 0;
            DataRowCollection collect = ReadExcel(filePath, ref col, ref row);

            CardManagerInfo[] array = new CardManagerInfo[row - 2];
            for (int i = 2; i < row; i++)
            {
                CardManagerInfo info = new CardManagerInfo();
                info.levelID = int.Parse(collect[i][0].ToString());
                info.roleLeftID = int.Parse(collect[i][1].ToString());
                info.roleRightID = int.Parse(collect[i][2].ToString());
                info.CardsLeftID = getIntArrayFromString(collect[i][3].ToString(), ';', '-');
                info.CardsRightID = getIntArrayFromString(collect[i][4].ToString(), ';', '-');
                info.slotTypes = getIntArrayFromString(collect[i][5].ToString(), ';');
                info.levelName = collect[i][6].ToString();
                info.roleLeftName = collect[i][7].ToString();
                info.roleRightName = collect[i][8].ToString();
                info.roleLeftDesPic = collect[i][9].ToString();
                info.roleRightDesPic = collect[i][10].ToString();
                array[i - 2] = info;
            }
            return array;
        }

        /// <summary>
        /// 读取CardResultInfo信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static CardResultInfo[] CreateCardResultInfoArrayWithExcel(string filePath)
        {
            int col = 0, row = 0;
            DataRowCollection collect = ReadExcel(filePath, ref col, ref row);

            CardResultInfo[] array = new CardResultInfo[row - 2];
            for (int i = 2; i < row; i++)
            {
                CardResultInfo info = new CardResultInfo();
                info.levelID = int.Parse(collect[i][0].ToString());
                info.leftCardID = int.Parse(collect[i][1].ToString());
                info.rightCardID = int.Parse(collect[i][2].ToString());
                info.rightFirst = int.Parse(collect[i][3].ToString()) == 1 ? true : false;
                info.Score = int.Parse(collect[i][4].ToString());
                info.resultString = collect[i][5].ToString();
                info.SpecialEndID = int.Parse(collect[i][6].ToString());
                info.SpecialEndName = collect[i][7].ToString();
                array[i - 2] = info;
            }
            return array;
        }

        /// <summary>
        /// 读取LevelResultInfo信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static LevelResultInfo[] CreateLevelResultInfoArrayWithExcel(string filePath)
        {
            int col = 0, row = 0;
            DataRowCollection collect = ReadExcel(filePath, ref col, ref row);

            LevelResultInfo[] array = new LevelResultInfo[row - 2];
            for (int i = 2; i < row; i++)
            {
                LevelResultInfo info = new LevelResultInfo();
                info.levelID = int.Parse(collect[i][0].ToString());
                info.passScore = int.Parse(collect[i][1].ToString());
                info.maxScore = int.Parse(collect[i][2].ToString());
                info.successEndID = int.Parse(collect[i][3].ToString());
                info.successEndName = collect[i][4].ToString();
                info.failEndID = int.Parse(collect[i][5].ToString());
                info.failEndName = collect[i][6].ToString();
                array[i - 2] = info;
            }
            return array;
        }
        
        /// <summary>
        /// 读取SelectInfo信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static SelectInfo[] CreateSelectInfoArrayWithExcel(string filePath)
        {
            int col = 0, row = 0;
            DataRowCollection collect = ReadExcel(filePath, ref col, ref row);

            SelectInfo[] array = new SelectInfo[row - 2];
            for (int i = 2; i < row; i++)
            {
                SelectInfo info = new SelectInfo();
                info.messageID = int.Parse(collect[i][0].ToString());
                info.levelID = int.Parse(collect[i][1].ToString());
                info.timeAttr = int.Parse(collect[i][2].ToString());
                info.leftRoleID = int.Parse(collect[i][3].ToString());
                info.rightRoleID = int.Parse(collect[i][4].ToString());
                info.message = collect[i][5].ToString();
                array[i - 2] = info;
            }
            return array;
        }
        
        /// <summary>
        /// 读取EndInfo信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static EndInfo[] CreateEndInfoArrayWithExcel(string filePath)
        {
            int col = 0, row = 0;
            DataRowCollection collect = ReadExcel(filePath, ref col, ref row);

            EndInfo[] array = new EndInfo[row - 2];
            for (int i = 2; i < row; i++)
            {
                EndInfo info = new EndInfo();
                info.endID = int.Parse(collect[i][0].ToString());
                info.endName = collect[i][1].ToString();
                info.endPic = collect[i][2].ToString();
                array[i - 2] = info;
            }
            return array;
        }

        /// <summary>
        /// 读取excel文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="columnNum">行数</param>
        /// <param name="rowNum">列数</param>
        /// <returns></returns>
        private static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            DataSet result = excelReader.AsDataSet();
            //Tables[0] 下标0表示excel文件中第一张表的数据
            columnNum = result.Tables[0].Columns.Count;
            rowNum = result.Tables[0].Rows.Count;
            return result.Tables[0].Rows;
        }

        private static int[] getIntArrayFromString(string s, char split, char from_to)
        {
            List<int> array = new List<int>();
            if (s.Contains(split.ToString()))
            {
                string[] stringArray = s.Split(split);
                for (int i = 0; i < stringArray.Length; ++i)
                {
                    List<int> from_to_array = getListIntFromString(stringArray[i], from_to);
                    foreach (int tmp in from_to_array)
                        array.Add(tmp);
                }
            }
            else
            {
                List<int> from_to_array = getListIntFromString(s, from_to);
                foreach (int tmp in from_to_array)
                    array.Add(tmp);
            }
            return array.ToArray();
        }

        private static List<int> getListIntFromString(string s, char from_to)
        {
            List<int> array = new List<int>();
            if (s.Contains(from_to.ToString()))
            {
                string[] fromtoArray = s.Split(from_to);
                int start = int.Parse(fromtoArray[0]);
                int end = int.Parse(fromtoArray[1]);
                for (int i = start; i <= end; i++)
                {
                    array.Add(i);
                }
            }
            else array.Add(int.Parse(s));
            return array;
        }

        private static int[] getIntArrayFromString(string s, char split)
        {
            List<int> array = new List<int>();
            string[] stringArray = s.Split(split);
            for(int i = 0;i < stringArray.Length; ++i)
            {
                array.Add(int.Parse(stringArray[i]));
            }
            return array.ToArray();
        }

//--------------------- 
//作者：王王王渣渣
//来源：CSDN
//原文：https://blog.csdn.net/wangjiangrong/article/details/79980447 
//版权声明：本文为博主原创文章，转载请附上博文链接！
    }
}
