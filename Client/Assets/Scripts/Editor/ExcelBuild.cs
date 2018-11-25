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
