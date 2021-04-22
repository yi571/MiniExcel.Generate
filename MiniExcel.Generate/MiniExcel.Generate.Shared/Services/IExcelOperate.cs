using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniExcel.Generate.Shared.Services
{
    public interface IExcelOperate
    {
        (Dictionary<Tuple<int, int>, object> dirData, int rowCount, int columnCount) ReadExcel(string path);

        List<dynamic> ExcelTableToObject(string path);

        bool AllSheetToJsons(string path, string savePath , bool isTitle);
    }
}
