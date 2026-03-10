using GrammarPolice.Actions;
using LSPD_First_Response.Mod.API;
using Rage;
using System.Net;
using System.Text;
using System.Reflection;
using System;
using System.IO;

namespace GTA5ComandiVocaliExtension
{
    public class Main : Plugin
    {
        private static Main _instance; // Singleton reference

        public override void Initialize()
        {
            _instance = this; // Imposta l'istanza singleton
            LoadConfig(); // Carica configurazione dal file esterno
            CheckDependencies(); // Controlla le dipendenze
            StartServer(); // Avvia il listener
        }

        private static void LoadConfig()
        {
            string configPath = "plugins/LSPDFR/GTA5ComandiVocaliExtension_config.txt";
            
            try
            {
                if (File.Exists(configPath))
                {
                    Game.LogTrivial("GTA5ComandiVocaliExtension: Caricamento configurazione da GTA5ComandiVocaliExtension_config.txt...");
                    string[] lines = File.ReadAllLines(configPath);
                    
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim().ToLower();
                            string value = parts[1].Trim();
                            
                            if (key == "listenport")
                            {
                                GlobalVariables.ListenPort = value;
                                Game.LogTrivial($"GTA5ComandiVocaliExtension: Porta configurata su {value}");
                            }
                        }
                    }
                }
                else
                {
                    Game.LogTrivial("GTA5ComandiVocaliExtension: GTA5ComandiVocaliExtension_config.txt non trovato, uso porta di default (51250)");
                }
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"GTA5ComandiVocaliExtension: Errore nel caricamento della configurazione - {ex.Message}");
            }
        }

        public override void Finally()
        {
            Game.LogTrivial(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + " has been cleaned up.");
        }

        private static void CheckDependencies()
        {
            Game.LogTrivial("GTA5ComandiVocaliExtension: Controllo dipendenze in corso...");

            // Controlla GrammarPolice
            try
            {
                var assembly = Assembly.Load("GrammarPolice");
                Game.LogTrivial("GTA5ComandiVocaliExtension: GrammarPolice.dll trovata");
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"GTA5ComandiVocaliExtension: GrammarPolice.dll non trovata - {ex.Message}");
            }

            // Controlla LSPD First Response
            try
            {
                var assembly = Assembly.Load("LSPD First Response");
                Game.LogTrivial("GTA5ComandiVocaliExtension: LSPD First Response.dll trovata");
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"GTA5ComandiVocaliExtension: LSPD First Response.dll non trovata - {ex.Message}");
            }

            // Controlla UltimateBackup
            try
            {
                var assembly = Assembly.Load("UltimateBackup");
                Game.LogTrivial("GTA5ComandiVocaliExtension: UltimateBackup.dll trovata");
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"GTA5ComandiVocaliExtension: UltimateBackup.dll non trovata - {ex.Message}");
            }

            // Controlla StopThePed
            try
            {
                var assembly = Assembly.Load("StopThePed");
                Game.LogTrivial("GTA5ComandiVocaliExtension: StopThePed.dll trovata");
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"GTA5ComandiVocaliExtension: StopThePed.dll non trovata - {ex.Message}");
            }

            // Controlla MechanicBackup
            try
            {
                var assembly = Assembly.Load("MechanicBackup");
                Game.LogTrivial("GTA5ComandiVocaliExtension: MechanicBackup.dll trovata");
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"GTA5ComandiVocaliExtension: MechanicBackup.dll non trovata - {ex.Message}");
            }
        }

        public static void StartServer()
        {
            var url = $"http://{GlobalVariables.Host}:{GlobalVariables.ListenPort}/";

            Game.LogTrivial($"GTA5ComandiVocaliExtension: Server Listener Avviato su {url}");
            var httpListener = new HttpListener();
            var simpleServer = new SimpleServer(httpListener, url, ProcessYourResponse);
            simpleServer.Start();
        }
        // Qui arrivano le richieste dal client
        public static byte[] ProcessYourResponse(string data)
        {

            Game.LogTrivial("GTA5ComandiVocaliExtension: Usato il comando " + data);
            if (data == "in_servizio") { SafeCall(() => GrammarPolice.API.Functions.Available(true, true)); }
            if (data == "fuori_servizio") { SafeCall(() => GrammarPolice.API.Functions.Busy(true, true)); }
            if (data == "accetta_chiamata") { SafeCall(() => Callout.Accept()); }

            // Poco per volta cambiare i nomi dei comandi
            if (data == "in_pattuglia") { SafeCall(() => GrammarPolice.API.Functions.StartPatrol(true, true)); }
            if (data == "investigando") { SafeCall(() => GrammarPolice.API.Functions.Investigating(true, true)); }
            if (data == "sulla_scena") { SafeCall(() => GrammarPolice.API.Functions.Scene(true, true)); }
            if (data == "torno_in_centrale") { SafeCall(() => GrammarPolice.API.Functions.ReturnToStation(true, true)); }
            if (data == "in_pericolo") { SafeCall(() => GrammarPolice.API.Functions.Panic()); }
            if (data == "agente_a_terra") { SafeCall(() => Backup.OfficerDown()); }

            if (data == "fermato_un_veicolo") { SafeCall(() => GrammarPolice.API.Functions.TrafficStop(true, true)); }
            if (data == "fermato_un_veicolo_backup") { SafeCall(() => UltimateBackup.API.Functions.callTrafficStopBackup()); }
            if (data == "codice2") { SafeCall(() => Backup.Code2()); }
            if (data == "codice3") { SafeCall(() => Backup.Code3()); }
            if (data == "codice4") { SafeCall(() => Status.Code4()); }

            if (data == "invia_backup") { SafeCall(() => UltimateBackup.API.Functions.callPursuitBackup()); }
            if (data == "invia_elicottero") { SafeCall(() => Backup.Air()); }
            if (data == "invia_medico_legale") { SafeCall(() => Backup.Coroner()); }
            if (data == "invia_ambulanza") { SafeCall(() => Backup.EMS()); }
            if (data == "invia_vigili_del_fuoco") { SafeCall(() => Backup.Fire()); }

            if (data == "controllo_documenti_persona")
            {
                SafeCall(() => StopThePed.API.Functions.requestDispatchPedCheck(true));
            }
            if (data == "controllo_targa_veicolo")
            {
                SafeCall(() => StopThePed.API.Functions.requestDispatchVehiclePlateCheck(true));
            }

            if (data == "inseguimento") { SafeCall(() => Callout.InitiatePursuit()); }
            if (data == "sospetto_scappato") { SafeCall(() => Advisory.SuspectLost()); }
            if (data == "sospetto_catturato") { SafeCall(() => Advisory.SuspectCustody()); }
            if (data == "blocco_stradale") { SafeCall(() => Backup.Roadblock()); }
            if (data == "striscia_chiodata") { SafeCall(() => Backup.Spikestrips()); }
            if (data == "manovra_pit") { SafeCall(() => Backup.Pit()); }
            if (data == "invia_carroattrezzi") { SafeCall(() => Backup.Tow()); }
            if (data == "invia_trasporto") { SafeCall(() => Backup.Transport()); }

            if (data == "nuovo_veicolo") { SafeCall(() => Backup.Vehicle()); }

            if (data == "pausa_cibo") { SafeCall(() => Status.MealBreak()); }

            if (data == "invia_meccanico") { SafeCall(() => MechanicBackup.API.spawnMechanicUnit()); }
            if (data == "invia_assicurazione") { SafeCall(() => Backup.Insurance()); }
            if (data == "invia_gruppo_supporto") { SafeCall(() => Backup.GroupBackup()); }
            if (data == "invia_controllo_animali") { SafeCall(() => StopThePed.API.Functions.callAnimalControl()); }

            // Comandi per i ped
            if (data == "dismissAllBackupUnits") { SafeCall(() => UltimateBackup.API.Functions.dismissAllBackupUnits()); }

            
            return Encoding.UTF8.GetBytes("OK");
        }

        private static void SafeCall(System.Action action)
        {
            try
            {
                GameFiber.StartNew(() =>
                {
                    try
                    {
                        action();
                    }
                    catch (Exception ex)
                    {
                        Game.LogTrivial($"GTA5ComandiVocaliExtension: Errore nell'esecuzione del comando - {ex.Message}");
                    }
                });
            }
            catch (Exception ex)
            {
                Game.LogTrivial($"GTA5ComandiVocaliExtension: Errore nell'avvio del comando - {ex.Message}");
            }
        }
    }
}
