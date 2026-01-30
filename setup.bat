@echo off
setlocal

:: Define lock file path
set "LOCK_FILE=%TEMP%\setup.lock"

:: Check if lock file exists, if so, exit to prevent concurrent setup
if exist "%LOCK_FILE%" (
    echo Setup is already in progress. Please wait for the previous setup to complete.
    exit /b 1
)

:: Create the lock file
echo Lock acquired > "%LOCK_FILE%"

echo ====== Running Windows Environment Setup ======

:: ---------- Check PowerShell 7+ ----------
where pwsh >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo PowerShell 7+ not found. Installing...

    set "PWSH_URL=https://github.com/PowerShell/PowerShell/releases/download/v7.4.2/PowerShell-7.4.2-win-x64.msi"
    set "PWSH_INSTALLER=%TEMP%\powershell-installer.msi"

    powershell -Command "Invoke-WebRequest -Uri '!PWSH_URL!' -OutFile '!PWSH_INSTALLER!'"
    msiexec /i "!PWSH_INSTALLER!" /quiet /norestart

    echo PowerShell installation initiated. Please restart this script after installation completes.
    pause
    del "%LOCK_FILE%"
    exit /b 1
) else (
    echo PowerShell 7+ is already installed.
)

echo ====== Running Browser Installation Check ======

:: === Google Chrome ===
set "chrome_path=%ProgramFiles%\Google\Chrome\Application\chrome.exe"
set "chrome_installer=chrome_installer.exe"

echo Checking Google Chrome...
if exist "%chrome_path%" (
    echo Google Chrome is already installed.
) else (
    echo Google Chrome not found. Downloading and installing...
    powershell -Command "Invoke-WebRequest -Uri 'https://dl.google.com/chrome/install/latest/chrome_installer.exe' -OutFile '%chrome_installer%'"
    start /wait "" "%cd%\%chrome_installer%" /silent /install
    del "%chrome_installer%"
)

:: === Microsoft Edge ===
set "edge_path=%ProgramFiles(x86)%\Microsoft\Edge\Application\msedge.exe"
set "edge_installer=msedge_installer.exe"

echo Checking Microsoft Edge...
if exist "%edge_path%" (
    echo Microsoft Edge is already installed.
) else (
    echo Microsoft Edge not found. Downloading and installing...
    powershell -Command "Invoke-WebRequest -Uri 'https://go.microsoft.com/fwlink/?linkid=2108834' -OutFile '%edge_installer%'"
    start /wait "" "%cd%\%edge_installer%" /silent /install
    del "%edge_installer%"
)

:: === Mozilla Firefox ===
set "firefox_path=%ProgramFiles%\Mozilla Firefox\firefox.exe"
set "firefox_installer=firefox_installer.exe"

echo Checking Mozilla Firefox...
if exist "%firefox_path%" (
    echo Mozilla Firefox is already installed.
) else (
    echo Mozilla Firefox not found. Downloading and installing...
    powershell -Command "Invoke-WebRequest -Uri 'https://download.mozilla.org/?product=firefox-latest-ssl&os=win64&lang=en-US' -OutFile '%firefox_installer%'"
    start /wait "" "%cd%\%firefox_installer%" -ms
    del "%firefox_installer%"
)

echo ====== Browser Check and Installation Completed ======

:: Remove lock file after setup is complete
del "%LOCK_FILE%"

pause >nul
