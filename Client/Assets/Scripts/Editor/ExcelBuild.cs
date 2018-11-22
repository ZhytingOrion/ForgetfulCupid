using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace EditorTool
{
    public class ExcelBuild : Editor
    {
        [MenuItem("CustomTools/Excel/CreateCardInfoAsset")]
        public static void CreateCardInfoAsset()
        {
            CardInfoArray cardAsset = ScriptableObject.CreateInstance<CardInfoArray>();
            cardAsset.dataArray = ExcelTools.CreateCardInfoArrayWithExcel(ExcelConfig.excelsPath + "CardInfo.xlsx");
            BuildAssets(cardAsset, "CardInfo.asset");
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
