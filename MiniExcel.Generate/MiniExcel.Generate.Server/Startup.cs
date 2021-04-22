using BootstrapBlazor.Components;
using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MiniExcel.Generate.Shared.Data;
using MiniExcel.Generate.Shared.Services;
using System.Linq;
using MenuItem = ElectronNET.API.Entities.MenuItem;

namespace MiniExcel.Generate.Server {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddBootstrapBlazor(setupAction: options => {
                options.ResourceManagerStringLocalizerType = typeof(Program);
                options.AdditionalJsonAssemblies = new[] { GetType().Assembly };
                options.AdditionalJsonFiles = new string[]
                {
                    @"./Locales/zh-TW.json",
                    @"./zh-CN.json" 
                };
            });

            services.AddRequestLocalization<IOptions<BootstrapBlazorOptions>>((localizerOption, blazorOption) =>
            {
                var supportedCultures = blazorOption.Value.GetSupportedCultures();

                localizerOption.SupportedCultures = supportedCultures;
                localizerOption.SupportedUICultures = supportedCultures;
            });

            services.AddRequestLocalization<IOptions<BootstrapBlazorOptions>>((localizerOption, blazorOption) => {
                var supportedCultures = blazorOption.Value.GetSupportedCultures();

                localizerOption.SupportedCultures = supportedCultures;
                localizerOption.SupportedUICultures = supportedCultures;
            });

            services.AddSingleton<WeatherForecastService>();

            // 增加 Table 数据服务操作类
            services.AddTableDemoDataService();

            services.AddSingleton<IExcelOperate, ExcelOperate>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
            }

            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value);

            app.UseStaticFiles();

            app.UseRouting();

            app.UseRequestLocalization(app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>()!.Value);

            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            if (HybridSupport.IsElectronActive)
            {
                ElectronBootstrap();
            }
        }

        public async void ElectronBootstrap()
        {
            var menu = new MenuItem[] {
                new MenuItem { Label = "編輯", Type = MenuType.submenu, Submenu = new MenuItem[] {
                    new MenuItem { Label = "Undo", Accelerator = "CmdOrCtrl+Z", Role = MenuRole.undo },
                    new MenuItem { Label = "Redo", Accelerator = "Shift+CmdOrCtrl+Z", Role = MenuRole.redo },
                    new MenuItem { Type = MenuType.separator },
                    new MenuItem { Label = "Cut", Accelerator = "CmdOrCtrl+X", Role = MenuRole.cut },
                    new MenuItem { Label = "Copy", Accelerator = "CmdOrCtrl+C", Role = MenuRole.copy },
                    new MenuItem { Label = "Paste", Accelerator = "CmdOrCtrl+V", Role = MenuRole.paste },
                    new MenuItem { Label = "Select All", Accelerator = "CmdOrCtrl+A", Role = MenuRole.selectall }
                }
                },
                new MenuItem { Label = "檢視", Type = MenuType.submenu, Submenu = new MenuItem[] {
                    new MenuItem
                    {
                        Label = "Reload",
                        Accelerator = "CmdOrCtrl+R",
                        Click = () =>
                        {
                            // on reload, start fresh and close any old
                            // open secondary windows
                            var mainWindowId = Electron.WindowManager.BrowserWindows.ToList().First().Id;
                            Electron.WindowManager.BrowserWindows.ToList().ForEach(browserWindow => {
                                if(browserWindow.Id != mainWindowId)
                                {
                                    browserWindow.Close();
                                }
                                else
                                {
                                    browserWindow.Reload();
                                }
                            });
                        }
                    },
                    new MenuItem
                    {
                        Label = "Toggle Full Screen",
                        Accelerator = "CmdOrCtrl+F",
                        Click = async () =>
                        {
                            bool isFullScreen = await Electron.WindowManager.BrowserWindows.First().IsFullScreenAsync();
                            Electron.WindowManager.BrowserWindows.First().SetFullScreen(!isFullScreen);
                        }
                    },
                    new MenuItem
                    {
                        Label = "Open Developer Tools",
                        Accelerator = "CmdOrCtrl+I",
                        Click = () => Electron.WindowManager.BrowserWindows.First().WebContents.OpenDevTools()
                    },
                    new MenuItem
                    {
                        Type = MenuType.separator
                    }
                }
                },
                new MenuItem { Label = "視窗", Role = MenuRole.window, Type = MenuType.submenu, Submenu = new MenuItem[] {
                     new MenuItem { Label = "Minimize", Accelerator = "CmdOrCtrl+M", Role = MenuRole.minimize },
                     new MenuItem { Label = "Close", Accelerator = "CmdOrCtrl+W", Role = MenuRole.close }
                }
                },
                new MenuItem {
                    Label = "關於",
                    Click = async () => {
                        //var options = new MessageBoxOptions("練習用小程式");
                        //    options.Type = MessageBoxType.warning;
                        //    options.Title = "測試";
                        //    await Electron.Dialog.ShowMessageBoxAsync(options);
                        await Electron.Shell.OpenExternalAsync("https://github.com/yi571");
                    }

                }
            };

            Electron.Menu.SetApplicationMenu(menu);
            var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Width = 1152,
                Height = 800,
                Show = false,
                AutoHideMenuBar = true,
                Resizable = false,
                Title = "生成試算表App"
            });

            //browserWindow.RemoveMenu();

            await browserWindow.WebContents.Session.ClearCacheAsync();
            //await browserWindow.WebContents.Session.ClearStorageDataAsync();

            browserWindow.OnReadyToShow += () => browserWindow.Show();
            browserWindow.SetTitle("試算表生成器");
        }
    }
}
