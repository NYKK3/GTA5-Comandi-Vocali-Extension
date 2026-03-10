# GTA5 Comandi Vocali Extension

Estensione per GTA 5 che permette di attivare comandi tramite la propria voce. La mod si integra con LSPD First Response e altre mod per gestire lo stato degli agenti, richiedere backup, e molto altro. Per funzionare è richiesta l'applicazione GTA5ComandiVocali

## 📋 Requisiti

- **GTA5ComandiVocali**: [Applicazione](https://github.com/NYKK3/GTA5-Comandi-Vocali)
- GTA 5
- RagePluginHook
- LSPD First Response
- GrammarPolice
- UltimateBackup
- StopThePed
- MechanicBackup

## 📦 Installazione

### Per Utenti Finali

1. Copia il file `GTA5ComandiVocaliExtension.dll` nella cartella `Grand Theft Auto V > plugins > LSPDFR` di GTA 5
2. Assicurati che tutte le DLL dipendenze siano presenti nella stessa cartella
3. Configura la porta del server (opzionale, vedi sezione Configurazione)

### Per Sviluppatori (Compilazione)

1. Clona il repository:
   ```bash
   git clone <repository-url>
   cd GTA5ComandiVocaliExtension
   ```

2. Scarica le DLL di riferimento necessarie e copiale nella cartella `references/`:
   - `GrammarPolice.dll`
   - `LSPD First Response.dll`
   - `MechanicBackup.dll`
   - `RagePluginHookSDK.dll`
   - `StopThePed.dll`
   - `UltimateBackup.dll`

3. Apri il progetto con Visual Studio 2022
4. Compila il progetto (F6 o Build > Build Solution)
5. Copia il file `.dll` generato in `bin\Debug\` o `bin\Release\` nella cartella `Grand Theft Auto V > plugins > LSPDFR` di GTA 5

## ⚙️ Configurazione

Per cambiare la porta del server listener:

1. Crea un file chiamato `GTA5ComandiVocaliExtension_config.txt` nella stessa cartella della mod
2. Aggiungi la seguente riga:
   ```
   ListenPort=51250
   ```
3. Riavvia il gioco

Se il file `GTA5ComandiVocaliExtension_config.txt` non esiste, la mod userà la porta di default (51250).

## 🎮 Comandi Disponibili

### Gestione Stato Agente
| Comando Vocale | Descrizione |
|----------------|-------------|
| `in_servizio` | Imposta stato "in servizio" |
| `fuori_servizio` | Imposta stato "fuori servizio" |
| `in_pattuglia` | Inizia pattuglia |
| `investigando` | Stato investigazione |
| `sulla_scena` | Stato sulla scena |
| `torno_in_centrale` | Ritorno in centrale |
| `in_pericolo` | Segnala pericolo |
| `agente_a_terra` | Segnala agente a terra |

### Chiamate e Backup
| Comando Vocale | Descrizione |
|----------------|-------------|
| `accetta_chiamata` | Accetta chiamata |
| `invia_backup` | Richiedi backup inseguimento |
| `invia_elicottero` | Richiedi supporto aereo |
| `invia_medico_legale` | Richiedi medico legale |
| `invia_ambulanza` | Richiedi ambulanza |
| `invia_vigili_del_fuoco` | Richiedi vigili del fuoco |
| `invia_carroattrezzi` | Richiedi carroattrezzi |
| `invia_trasporto` | Richiedi trasporto |
| `invia_meccanico` | Richiedi meccanico |
| `invia_assicurazione` | Richiedi assicurazione |
| `invia_gruppo_supporto` | Richiedi gruppo supporto |

### Codice e Status
| Comando Vocale | Descrizione |
|----------------|-------------|
| `codice2` | Codice 2 (senza allarmi) |
| `codice3` | Codice 3 (con allarmi) |
| `codice4` | Codice 4 (situazione sotto controllo) |

### Controlli
| Comando Vocale | Descrizione |
|----------------|-------------|
| `controllo_documenti_persona` | Richiedi controllo documenti |
| `controllo_targa_veicolo` | Richiedi controllo targa |

### Inseguimento
| Comando Vocale | Descrizione |
|----------------|-------------|
| `inseguimento` | Inizia inseguimento |
| `sospetto_scappato` | Segnala sospetto scappato |
| `sospetto_catturato` | Segnala sospetto catturato |
| `blocco_stradale` | Richiedi blocco stradale |
| `striscia_chiodata` | Richiedi striscia chiodata |
| `manovra_pit` | Richiedi manovra PIT |

### Traffico
| Comando Vocale | Descrizione |
|----------------|-------------|
| `fermato_un_veicolo` | Segnala fermata veicolo |
| `fermato_un_veicolo_backup` | Richiedi backup fermata veicolo |
| `nuovo_veicolo` | Richiedi nuovo veicolo |

### Altro
| Comando Vocale | Descrizione |
|----------------|-------------|
| `pausa_cibo` | Segnala pausa cibo |
| `dismissAllBackupUnits` | Tutte le unità di supporto se ne andranno via |

## 🔧 Gestione Errori

La mod include un sistema di gestione errori che:
- Controlla all'avvio se tutte le DLL dipendenze sono presenti
- Logga avvisi invece di crashare se una DLL manca
- Gestisce errori runtime senza interrompere l'esecuzione

## 📝 Log

Tutti i messaggi vengono loggati tramite `Game.LogTrivial()` con il prefisso `GTA5ComandiVocaliExtension:`.

## 📄 File del Progetto

- `Main.cs` - Codice principale del plugin
- `SimpleServer.cs` - Server HTTP per ricevere comandi
- `GlobalVariables.cs` - Variabili globali (host e porta)
- `config.txt` - File di configurazione (opzionale)

## ⚠️ Note

- Assicurati che tutte le DLL dipendenze siano presenti nella cartella `plugins`
- La mod non crasha se una DLL manca, ma logga un avviso
- Puoi cambiare la porta del server modificando `GTA5ComandiVocaliExtension_config.txt`
