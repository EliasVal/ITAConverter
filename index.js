const url = require("url")
const path = require('path');
const ita = require("./src/js/imageToAscii");
const { existsSync, lstatSync } = require("fs");

const { app, BrowserWindow, Menu, ipcMain, dialog, shell } = require("electron")

let mainWindow;

app.on('ready', () => {
    mainWindow = new BrowserWindow({
        webPreferences: {
            preload: path.join(__dirname, '/src/js/preload.js'),
            contextIsolation: true,
            enableRemoteModule: false
        },
        width: 725,
        height: 550,
        resizable: false,
        icon: path.join(__dirname, `/icons/icon.ic${process.platform == "darwin" ? "ns" : "o"}`)
    });

    //load HTML
    mainWindow.loadURL(url.format({
        pathname: path.join(__dirname, '/src/html/mainWindow.html'),
        protocol: 'file:',
        slashes: true
    }))

    mainWindow.on('close', () => {
        app.quit();
    })


    
    //const navBar = Menu.buildFromTemplate(Template)
    Menu.setApplicationMenu(null)
})

const Template = null/*[{
    label: 'ToggleDevTools',
    click(item, focusedWindow) {
        focusedWindow.toggleDevTools();
    },
    accelerator: 'F12'
},
{
    role: 'reload'
}]*/


ipcMain.on("toMain", (event, args) => {
    switch (args.action) {
        case "ascii":
            if (existsSync(args.outputPath) && lstatSync(args.outputPath).isDirectory()) {
                for (image of args.imagePath)
                {
                    ita.imageToAscii(image, args.outputPath)
                }
            }
            else {
                mainWindow.webContents.send("fromMain", { status: "err", message: "invalid_path"})
            }
            break;
        case "alert":
            dialog.showMessageBox({
                title: args.title,
                message: args.message
            })
            break;
        case "path":
            let dir = dialog.showOpenDialogSync({properties: ['openDirectory']});
            if (!dir) return
            let data = {
                path: dir[0],
                status: "path"
            }
            mainWindow.webContents.send("fromMain", data)
            break;
        case "openPromo":
            shell.openExternal("https://github.com/EliasVal/")
            break;
    }
    
});