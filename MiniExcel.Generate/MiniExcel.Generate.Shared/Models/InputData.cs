using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniExcel.Generate.Shared.Models
{
    public class InputData
    {
        [Display(Name = "連線字串")]
        [Required(ErrorMessage = "{0}為必填")]
        public string ConnectStr { get; set; }

        [Display(Name = "檔案名稱")]
        [Required(ErrorMessage = "{0}為必填")]
        public string FileName { get; set; }

        [Display(Name = "查詢指令")]
        [Required(ErrorMessage = "{0}為必填")]
        public string SqlCommand { get; set; }
        [Display(Name = "檔案類型")]
        public FileType FileType { get; set; }
    }

    public enum FileType
    {
        Excel,
        Csv
    }
}
