﻿using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WindowsGSM.Functions;
using WindowsGSM.GameServer.Engine;
using WindowsGSM.GameServer.Query;

namespace WindowsGSM.Plugins
{
    public class STATIONEERS : SteamCMDAgent
    {
        // - Plugin Details
        public Plugin Plugin = new Plugin
        {
            name = "WindowsGSM.STATIONEERS", // WindowsGSM.XXXX
            author = "MrJohnDowe",
            description = "🧩 WindowsGSM plugin for supporting Staioneers Dedicated Server",
            version = "1.2.3",
            url = "https://github.com/mrjohndowe/WindowsGSM.STATIONEERS", // Github repository link (Best practice)
            color = "#07077dff" // Color Hex
        };


        // - Standard Constructor and properties
        public STATIONEERS(ServerConfig serverData) : base(serverData) => base.serverData = _serverData = serverData;
        private readonly ServerConfig _serverData;
        public string Error, Notice;

        // - Settings properties for SteamCMD installer
        public override bool loginAnonymous => true;
        public override string AppId => "600760 "; // Game server appId, STATIONEERS is 600760 


        // - Game server Fixed variables
        public override string StartPath => @"rocketstation_DedicatedServer.exe"; // Game server start path
        public string FullName = "Stationeers Dedicated Server"; // Game server FullName
        public bool AllowsEmbedConsole = false;  // Does this server support output redirect?
        public int PortIncrements = 1; // This tells WindowsGSM how many ports should skip after installation
        public object QueryMethod = null; // Query method should be use on current server type. Accepted value: null or new A2S() or new FIVEM() or new UT3()


        // - Game server default values
		public string ServerDescription = "Stationeers"; //Default Server Description
		public string Password = "AdminPass"; //Default Server Password [none]
		public string AdminPass = ""; // Default Adinpassword
		public string RConPass = "stationeers"; //Defuakt RconPassword
        public string Port = "27000"; // Default port
        public string QueryPort = "27001"; // Default query port
        public string Defaultmap = "Moon"; // Default map name
        public string Maxplayers = "0"; // Default maxplayers
		//public string ModPath = ".\mods\";
        public string Additional = "-batchmode -nographics -autostart -gameport=27000 -updateport=27001 -autosaveinterval=300 -servername=WGSM-StationeersServer -password=lol -loadworld=WGSM-StationeersServer -worldname=WGSM-StationeersServer";


        // - Create a default cfg for the game server after installation
        public async void CreateServerCFG() 
        {
            string configPath = ServerPath.GetServersServerFiles(_serverData.ServerID, @"");
            //may nod used in Stationeers
			if (!Directory.Exists(configPath))
            {
                try
                {
                    Directory.CreateDirectory(configPath);
                } catch
                {
                    Error = "Couldn't create config path!";
                    return;
                }

            }

            //string StationeerServerSettingsIni = "default.ini";
           //string EngineIni = "Engine.ini";

			/*
            if (await DownloadGameServerConfig(StationeerServerSettingsIni, Path.Combine(configPath, StationeerServerSettingsIni)))
            {
                string configText = File.ReadAllText(configPath + "\\" + StationeerServerSettingsIni);
				configText = configText.Replace("{{SERVERNAME}}", _serverData.FullName);
				configText = configText.Replace("{{GAMEPORT}}", _serverData.ServerPort);
                configText = configText.Replace("{{UPDATERPORT}}", _serverData.QueryPort);
				configText = configText.Replace("{{MODPATH}}", _serverData.ModPath);
				configText = configText.Replace("{{PASSWORD}}", _serverData.Password);
				configText = configText.Replace("{{ADMINPASSWORD}}", _serverData.AdminPass);
                configText = configText.Replace("{{MAPNAME}}", _serverData.map);
				configText = configText.Replace("{{DESCRIPTION}}", _serverData.ServerDescription);
				configText = configText.Replace("{{MAXPLAYER}}", _serverData.Maxplayers);
				configText = configText.Replace("{{RCONPASSWORD}}", _serverData.RConPass);			
				
                File.WriteAllText(configPath + "\\" + StationeerServerSettingsIni, configText);
            }*/
        }


        // - Start server function, return its Process to WindowsGSM
        public async Task<Process> Start()
        {
            QueryPort = Port + 1;
            string param = "-log";
            param += $" {_serverData.ServerParam}";

            string workingDir = Functions.ServerPath.GetServersServerFiles(_serverData.ServerID);
            string runPath = Path.Combine(workingDir, "rocketstation_DedicatedServer.exe");

            // Prepare Process
            var p = new Process
            {
                StartInfo =
                {
                    WindowStyle = ProcessWindowStyle.Minimized,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = ServerPath.GetServersServerFiles(_serverData.ServerID),
                    FileName = runPath,
                    Arguments = param.ToString()
                },
                EnableRaisingEvents = true
            };

            // Start Process
            try
            {
                p.Start();
                return p;
            } catch (Exception e)
            {
                base.Error = e.Message;
                return null; // return null if fail to start
            }
        }


        // - Stop server function
        public async Task Stop(Process p) => await Task.Run(() => { p.Kill(); });

        // Get ini files
        public static async Task<bool> DownloadGameServerConfig(string fileSource, string filePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //await webClient.DownloadFileTaskAsync($"https://raw.githubusercontent.com/1stian/WindowsGSM-Configs/master/Astroneer%20Dedicated%20Server/{fileSource}", filePath);
                }
            } catch (Exception e)
            {
                //System.Diagnostics.Debug.WriteLine($"Github.DownloadGameServerConfig {e}");
            }

            return File.Exists(filePath);
        }
    }
}
