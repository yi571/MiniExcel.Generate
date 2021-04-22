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
        default:
            alert(route);
            break;
    }
	
}
