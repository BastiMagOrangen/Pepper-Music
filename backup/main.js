let {app, BrowserWindow, dialog} = require('electron');

let mainWindow = null;

let createWindow = () => {
    mainWindow = new BrowserWindow({
        width: 1280,
        height: 780,
        webPreferences: {
            nodeIntegration: true,
            enableRemoteModule: true
        },
        autoHideMenuBar: true,
        
    });

    mainWindow.loadFile('./ui/index.html');
    mainWindow.webContents.openDevTools()
    
}

app.whenReady().then(() => {
    createWindow();
});

app.on('window-all-closed', () => {
    if(process.platform !== "darwin")
    {
        app.quit();
    }
});

