using Dapper;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Components.Forms;
using MiniExcel.Generate.Shared.Models;
using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniExcel.Generate.Shared.Pages
{
    public sealed partial class Index
    {
        private InputData Model { get; set; } = new() {
            ConnectStr = ""
            , SqlCommand = ""
            , FileType = FileType.Excel };


        private async Task OnValidSubmit(EditContext context)
        {
            var mainWindow = Electron.WindowManager.BrowserWindows.First();
            var options = new OpenDialogOptions
            {
                Properties = new OpenDialogProperty[] {
                 OpenDialogProperty.openDirectory,
                }
            };

            string[] dirs = await Electron.Dialog.ShowOpenDialogAsync(mainWindow, options);

            if (dirs.Length > 0)
            {
                using (var connection = new SqlConnection(Model.ConnectStr))
                {
                    var rows = connection.Query(Model.SqlCommand);
                    //iniExcelLibs.MiniExcel.SaveAs(path, rows);
                    string fuDangMing = "";
                    switch (Model.FileType)
                    {
                        case FileType.Excel:
                            fuDangMing = ".xlsx";
                            break;
                        case FileType.Csv:
                            fuDangMing = ".csv";
                            break;
                        default:
                            break;
                    }
                    string path = $"{dirs[0]}\\{Model.FileName}{fuDangMing}";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    MiniExcelLibs.MiniExcel.SaveAs(path, rows);

                    var NotifyOptions = new NotificationOptions("您提出的作業已完成 😀", $"請點擊氣球查看檔案。")
                    {
                        OnClick = async () => await Electron.Shell.ShowItemInFolderAsync(path)
                    };

                    Electron.Notification.Show(NotifyOptions);
                }
            }
            
        }

        private Task OnInvalidSubmit(EditContext context)
        {
            //Trace2.Log("OnInvalidSubmit 回调委托");
            return Task.CompletedTask;
        }
    }

}
