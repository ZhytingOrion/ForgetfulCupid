using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace EditorTool
{
    public class ExcelBuild : Editor
    {
        /// <summary>
        /// 读取CardInfo表格
        /// </summary>
        [MenuItem("CustomTools/Excel/CreateCardInfoAsset")]
        public static void CreateCardInfoAsset()
        {
            CardInfoArray cardAsset = ScriptableObject.CreateInstance<CardInfoArray>();
            cardAsset.dataArray = ExcelTools.CreateCardInfoArrayWithExcel(ExcelConfig.excelsPath + "CardInfo.xlsx");
            BuildAssets(cardAsset, "CardInfo.asset");
        }
        
        /// <summary>
        /// 读取RoleInfo表格
        /// </summary>
        [MenuItem("CustomTools/Excel/CreateRoleInfoAsset")]
        public static void CreateRoleInfoAsset()
        {
            RoleInfoArray asset = ScriptableObject.CreateInstance<RoleInfoArray>();
            asset.dataArray = ExcelTools.CreateRoleInfoArrayWithExcel(ExcelConfig.excelsPath + "RoleInfo.xlsx");
            BuildAssets(asset, "RoleInfo.asset");
        }

        /// <summary>
        /// 读取CardManagerInfo表格
        /// </summary>
        [MenuItem("CustomTools/Excel/CreateLevelInfoAsset")]
        public static void CreateCardManagerInfoAsset()
        {
            CardManagerInfoArray asset = ScriptableObject.CreateInstance<CardManagerInfoArray>();
            asset.dataArray = ExcelTools.CreateCardManagerInfoArrayWithExcel(ExcelConfig.excelsPath + "LevelInfo.xlsx");
            BuildAssets(asset, "LevelInfo.asset");
        }

        /// <summary>
        /// 读取CardResultInfo表格
        /// </summary>
        [MenuItem("CustomTools/Excel/CreateCardResultInfoAsset")]
        public static void CreateCardResultInfoAsset()
        {
            CardResultInfoArray asset = ScriptableObject.CreateInstance<CardResultInfoArray>();
            asset.dataArray = ExcelTools.CreateCardResultInfoArrayWithExcel(ExcelConfig.excelsPath + "CardResultInfo.xlsx");
            BuildAssets(asset, "CardResultInfo.asset");
        }

        /// <summary>
        /// 读取CardResultInfo表格
        /// </summary>
        [MenuItem("CustomTools/Excel/CreateLevelResultInfoAsset")]
        public static void CreateLevelResultInfoAsset()
        {
            LevelResultInfoArray asset = ScriptableObject.CreateInstance<LevelResultInfoArray>();
            asset.dataArray = ExcelTools.CreateLevelResultInfoArrayWithExcel(ExcelConfig.excelsPath + "LevelResultInfo.xlsx");
            BuildAssets(asset, "LevelResultInfo.asset");
        }

        /// <summary>
        /// 读取SelectInfo表格
        /// </summary>
        [MenuItem("CustomTools/Excel/CreateSelectInfoAsset")]
        public static void CreateSelectInfoAsset()
        {
            SelectInfoArray asset = ScriptableObject.CreateInstance<SelectInfoArray>();
            asset.dataArray = ExcelTools.CreateSelectInfoArrayWithExcel(ExcelConfig.excelsPath + "SelectInfo.xlsx");
            BuildAssets(asset, "SelectInfo.asset");
        }
        private static void BuildAssets(Object assets, string assetName)
        {
            if (!Directory.Exists(ExcelConfig.assetPath))
            {
                Directory.CreateDirectory(ExcelConfig.assetPath);
            }

            string assetPath = ExcelConfig.assetPath + assetName;
            AssetDatabase.CreateAsset(assets, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
