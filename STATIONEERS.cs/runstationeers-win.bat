@echo off

@REM create place to store save/ scripts/ setting.xml etc...
set DATA_DIR=%CD%/data

@REM set exe
set EXECUTABLE="C:\Server\servers\2\serverfiles\rocketstation_DedicatedServer.exe"

@REM # Define settings
set SERVER_NAME="Dowe Stationeers"
set SAVE_NAME="Dedi Test"
set WORLD_TYPE="Lunar"
set SERVER_PASSWORD=""
set SERVER_AUTH_SECRET="ICantTellYou"
set GAME_PORT=27000
set UPDATE_PORT=27001
set START_LOCAL_HOST=true
set SERVER_VISIBLE=true
set UPNP_ENABLED=false
set SERVER_MAX_PLAYERS=13
set AUTO_SAVE=true
set SAVE_INTERVAL=300
set SAVE_PATH="%DATA_DIR%"
set LOG_FILE="%DATA_DIR%/log.txt"
set SETTINGS_PATH="%DATA_DIR%/settings.xml"
set DIFFICULTY="easy"
set LOCAL_IP_ADDRESS="0.0.0.0"

@REM Run the server
%EXECUTABLE% ^
-loadlatest %SAVE_NAME% %WORLD_TYPE% ^
-settingspath %SETTINGS_PATH% ^
-difficulty %DIFFICULTY% ^
-settings StartLocalHost %START_LOCAL_HOST% ServerVisible %SERVER_VISIBLE% ^
    GamePort %GAME_PORT% UPNPEnabled %UPNP_ENABLED% ServerName %SERVER_NAME% ^
    ServerPassword %SERVER_PASSWORD% ServerMaxPlayers %SERVER_MAX_PLAYERS% ^
    AutoSave %AUTO_SAVE% SaveInterval %SAVE_INTERVAL% ^
    SavePath %SAVE_PATH% ^
    ServerAuthSecret %SERVER_AUTH_SECRET% ^
    LocalIpAddress %LOCAL_IP_ADDRESS%