using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MiniExcel.Generate.Shared.Shared {
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout {
        [Inject]
        protected IJSRuntime js { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = false;

        private List<MenuItem> Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized() {
            base.OnInitialized();

            // TODO: 菜单获取可以通过数据库获取，此处为示例直接拼装的菜单集合
            Menus = GetIconSideMenuItems();
        }

        private static List<MenuItem> GetIconSideMenuItems() {
            var menus = new List<MenuItem>
            {
                //new MenuItem() { Text = "返回组件库", Icon = "fa fa-fw fa-home", Url = "https://www.blazor.zone/components" },
                new MenuItem() { Text = "生成試算表", Icon = "fa fa-table", Url = "" },
                new MenuItem() { Text = "Excel轉json", Icon = "fab fa-node-js", Url ="exceltojson" },
                new MenuItem() { Text = "Csv轉Excel", Icon = "far fa-file-excel", Url = "", IsDisabled = true },
                new MenuItem() { Text = "Excel轉Csv", Icon = "fas fa-file-csv", Url = "", IsDisabled = true }
            };

            return menus;
        }

        protected async System.Threading.Tasks.Task ShowGuidelineAsync()
        {
            object[] obj = new object[] {
                true, NavigationManager.ToBaseRelativePath(NavigationManager.Uri)
            };
            await js.InvokeVoidAsync("Guide", obj);
        }
    }
}
