using ElectronNET.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniExcel.Generate.Shared.Services
{
    public class ExcelOperate : IExcelOperate
    {
        public bool AllSheetToJsons(string path, string savePath, bool isTitle)
        {
            var sheetNames = MiniExcelLibs.MiniExcel.GetSheetNames(path).ToList();
            var mainWindow = Electron.WindowManager.BrowserWindows.First();
            int count = 0;
            foreach (var sheetName in sheetNames)
            {
                var rows = MiniExcelLibs.MiniExcel.Query(path, sheetName: sheetName, useHeaderRow: isTitle);
                var jsonStr = JsonConvert.SerializeObject(rows);

                if (!string.IsNullOrEmpty(savePath))
                {
                    try
                    {
                        using StreamWriter sw = new StreamWriter($"{savePath}/{sheetName}.json");
                        sw.Write(jsonStr);
                        sw.Close();
                        count++;
                        mainWindow.SetProgressBar((double)count / sheetNames.Count);
                    }
                    catch (Exception ex)
                    {
                        Electron.Dialog.ShowErrorBox("發生錯誤", ex.Message);
                        return false;
                    }

                }
                else
                {
                    Electron.Dialog.ShowErrorBox("發生錯誤", "路徑為空值！");
                    return false;
                }
            }

            return true;
        }

        public bool CsvToJson(string path, string savePath, bool isTitle)
        {
            var rows = MiniExcelLibs.MiniExcel.Query(path, useHeaderRow: isTitle);
            var jsonStr = JsonConvert.SerializeObject(rows);
            string fileName = Path.GetFileNameWithoutExtension(path);

            if (!string.IsNullOrEmpty(savePath))
            {
                try
                {
                    using StreamWriter sw = new StreamWriter($"{savePath}/{fileName}.json");
                    sw.Write(jsonStr);
                    sw.Close();
                    
                }
                catch (Exception ex)
                {
                    Electron.Dialog.ShowErrorBox("發生錯誤", ex.Message);
                    return false;
                }

            }
            else
            {
                Electron.Dialog.ShowErrorBox("發生錯誤", "路徑為空值！");
                return false;
            }

            return true;
        }

        public List<dynamic> ExcelTableToObject(string path)
        {
            throw new NotImplementedException();
        }

        public (Dictionary<Tuple<int, int>, object> dirData, int rowCount, int columnCount) ReadExcel(string path)
        {
            throw new NotImplementedException();
        }
    }
}
