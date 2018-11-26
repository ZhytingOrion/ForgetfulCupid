using System.Collections;
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

            CardInfo[] array = new CardInfo[row - 1];
            for (int i = 1; i<row; i++)
            {
                CardInfo cardInfo = new CardInfo();
                cardInfo.cardID = int.Parse(collect[i][0].ToString());
                cardInfo.locType = LocType.Left;
                if (collect[i][1].ToString() == "R") cardInfo.locType = LocType.Right;
                cardInfo.type = (CardType)int.Parse(collect[i][2].ToString());
                cardInfo.AlwaysShowCard = collect[i][3].ToString() == "Y" ? true : false;
                cardInfo.AlwaysShowType = collect[i][4].ToString() == "Y" ? true : false; ;
                cardInfo.stayTime = float.Parse(collect[i][5].ToString());
                cardInfo.context = collect[i][6].ToString();
                array[i - 1] = cardInfo;
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

            CardManagerInfo[] array = new CardManagerInfo[row - 1];
            for (int i = 1; i < row; i++)
            {
                CardManagerInfo info = new CardManagerInfo();
                info.levelID = int.Parse(collect[i][0].ToString());
                info.roleLeftID = int.Parse(collect[i][1].ToString());
                info.roleRightID = int.Parse(collect[i][2].ToString());
                info.CardsLeftID = getIntArrayFromString(collect[i][3].ToString(), ';');
                info.CardsRightID = getIntArrayFromString(collect[i][4].ToString(), ';');
                info.CardsLeftLocs = getIntArrayFromString(collect[i][5].ToString(), ';');
                info.CardsRightLocs = getIntArrayFromString(collect[i][6].ToString(), ';');
                info.typeTexsAddrs = collect[i][7].ToString().Split(';');
                info.ContentTypeTexsAddrs = collect[i][8].ToString().Split(';');
                info.contentTexsAddrsLeft = collect[i][9].ToString();
                info.contentTexsAddrsRight = collect[i][10].ToString();
                info.backTexsAddrsLeft= collect[i][11].ToString();
                info.backTexsAddrsRight = collect[i][12].ToString();
                info.leftStep = int.Parse(collect[i][13].ToString());
                info.rightStep = int.Parse(collect[i][14].ToString());
                info.slotTypeTexsAddrs = collect[i][15].ToString().Split(';');
                info.slotTypes = getIntArrayFromString(collect[i][16].ToString(), ';');
                array[i - 1] = info;
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

            CardResultInfo[] array = new CardResultInfo[row - 1];
            for (int i = 1; i < row; i++)
            {
                CardResultInfo info = new CardResultInfo();
                info.levelID = int.Parse(collect[i][0].ToString());
                info.leftCardID = int.Parse(collect[i][1].ToString());
                info.rightCardID = int.Parse(collect[i][2].ToString());
                info.Score = int.Parse(collect[i][3].ToString());
                info.SpecialEndName = collect[i][4].ToString();
                info.SpecialLevel = int.Parse(collect[i][5].ToString());
                array[i - 1] = info;
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
