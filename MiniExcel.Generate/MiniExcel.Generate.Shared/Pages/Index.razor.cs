using Dapper;
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
            ConnectStr = @"Data Source=.;Initial Catalog=YTest;Connection Timeout=300000;Integrated Security=true;"
            , SqlCommand = "select * from Person"
            , FileType = FileType.Excel };


        private async Task OnValidSubmit(EditContext context)
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
                string path = $@"D:\Tmp\{Model.FileName}{fuDangMing}";
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                MiniExcelLibs.MiniExcel.SaveAs(path, rows);
            }
        }

        private Task OnInvalidSubmit(EditContext context)
        {
            //Trace2.Log("OnInvalidSubmit 回调委托");
            return Task.CompletedTask;
        }
    }

}
