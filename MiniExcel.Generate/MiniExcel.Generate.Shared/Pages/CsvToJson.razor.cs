using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Components;
using MiniExcel.Generate.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniExcel.Generate.Shared.Pages
{
    public sealed partial class CsvToJson
    {
        [Inject]
        IExcelOperate excelOperate { get; set; }

        public bool IsTitle { get; set; } = true;

        private void OnValueChanged(bool val)
        {
            IsTitle = val;
        }

        protected async Task SelectFile() {
            var mainWindow = Electron.WindowManager.BrowserWindows.First();
            var options = new OpenDialogOptions
            {
                Properties = new OpenDialogProperty[] {
                        OpenDialogProperty.openFile,
                    },
                Filters = new FileFilter[] {
                        new FileFilter {
                            Extensions = new string[] { "csv" },
                            Name = "csv"
                        }
                    }
            };

            string[] files = await Electron.Dialog.ShowOpenDialogAsync(mainWindow, options);
            if (files.Length > 0)
            {
                var saveOptions = new OpenDialogOptions
                {
                    Properties = new OpenDialogProperty[] {
                        OpenDialogProperty.openDirectory,
                    }
                };

                var result = await Electron.Dialog.ShowOpenDialogAsync(mainWindow, saveOptions);

                if (result.Length > 0)
                {
                    var isSucess = excelOperate.CsvToJson(files[0], result[0], IsTitle);



                    if (isSucess)
                    {


                        var NotifyOptions = new NotificationOptions("ConverData 訊息", $"檔案已儲存於：{result[0]}\n點擊氣球查看檔案。")
                        {
                            OnClick = async () => await Electron.Shell.ShowItemInFolderAsync(result[0])
                        };

                        Electron.Notification.Show(NotifyOptions);
                    }
                }


            }
        }
    }
}
