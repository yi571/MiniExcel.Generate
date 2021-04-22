function Guide(isRun, route) {
    switch (route) {
        case "":
            afet.ShowNoviceGuide('readFlagName', isRun, {
                '[name=gConnectStr]': {
                    offsetLeft: 160, offsetTop: 25,
                    text: '在這邊填入SQL資料庫的連線字串。'
                },
                '[name=gFileName]': {
                    offsetLeft: -160, offsetTop: 25,
                    text: '這裡輸入輸出用的檔名。'
                },
                '[name=gFuDang]': {
                    offsetLeft: 160, offsetTop: 25,
                    text: '選擇想輸出的檔案類型，目前提供xlsx和csv。'
                },
                '[name=gCommand]': {
                    offsetLeft: 160, offsetTop: 25,
                    text: '這邊填入資料查詢語法\n(注意：輸入INSERT、UPDATE、DELETE...等，也會執行)。'
                },
                '[name=gSubmit]': {
                    offsetLeft: 160, offsetTop: 25,
                    text: '所有欄位都確認無誤，按下按鍵後，選擇想要儲存的位置，即可輸出試算表。'
                }
            });
            break;
        case "exceltojson":
            afet.ShowNoviceGuide('readFlagName', isRun, {
                '[name=gExcelToJsonSwitch] .switch-core': {
                    offsetLeft: 160, offsetTop: 25,
                    text: '是：表示第一列是欄位名稱，會以第一列為Key。\n否：則會以A、B、C...為Key。'
                },
                '[name=gExceltojsonSubmit]': {
                    offsetLeft: 160, offsetTop: 25,
                    text: '按下按鍵後，先選擇要轉換的xlsx檔，再選擇儲存資料夾。\n程式會將所有sheet轉成json，並以sheet名稱作為檔名。'
                }
            });
            break;
        case "csvtojson":
            afet.ShowNoviceGuide('readFlagName', isRun, {
                '[name=gCsvToJsonSwtich] .switch-core': {
                    offsetLeft: 160, offsetTop: 25,
                    text: '是：表示第一列是欄位名稱，會以第一列為Key。\n否：則會以A、B、C...為Key。'
                },
                '[name=gCsvToJsonSubmit]': {
                    offsetLeft: 160, offsetTop: 25,
                    text: '按下按鍵後，先選擇要轉換的csv檔，再選擇儲存資料夾。\n程式會以csv檔案名稱作為檔名。'
                }
            });
            break;
        default:
            alert(route);
            break;
    }
	
}
