# Roads-and-Gems

Windows Forms implementation of the board game Indigo, built with .NET 8.

## Current Features

- Local play for 2, 3, or 4 players
- Player color selection before match start
- Turn banner that shows whose turn it is
- Score tracking for all active players
- Game state snapshot and JSONL logging for future networking and debugging work
- Online multiplayer lobby screen with host mode and join-by-IP mode
- Host-side player limit enforcement up to 4 players
- Connected player list and session log
- Host-controlled online match start that opens the game for all connected players

## Tech Stack

- C#
- Windows Forms
- .NET 8 (`net8.0-windows`)

## Project Structure

- `Indigo1_Sol.sln` - solution file
- `WinFormsApp2/` - main game project
- `WinFormsApp2/GameForm.cs` - main board and game logic
- `WinFormsApp2/TitleScreenForm.cs` - title screen and entry flow
- `WinFormsApp2/OnlineMultiplayerForm.cs` - online lobby UI and TCP host/join flow
- `WinFormsApp2/GameStateSnapshot.cs` - serializable game state models

## Requirements

- Windows
- .NET SDK 8 or newer
- Windows Desktop runtime

## Build

From the repository root:

```powershell
dotnet build Indigo1_Sol.sln
```

If `Indigo.exe` is already running and locks the apphost file, use:

```powershell
dotnet build Indigo1_Sol.sln -p:UseAppHost=false
```

## Run

Standard run:

```powershell
dotnet run --project WinFormsApp2/Indigo.csproj
```

If you built with `UseAppHost=false`, you can also run:

```powershell
dotnet WinFormsApp2/bin/Debug/net8.0-windows/Indigo.dll
```

## Local Multiplayer

1. Start the game from the title screen.
2. Choose `2 Players`, `3 Players`, or `4 Players`.
3. Click `Start Game`.
4. Click `Choose players` inside the game and assign colors.

## Online Multiplayer Lobby

The project includes a network lobby and synchronized match launch, but not full network-synced gameplay yet.

### Host

1. Open `Online Multiplayer` from the title screen.
2. Enter your player name.
3. Select max players from `2` to `4`.
4. Choose a port.
5. Click `Start Hosting`.
6. Share your IP and port with the other players.
7. When enough players have joined, click `Start Online Game` to launch the match for everyone in the lobby.

### Join

1. Open `Online Multiplayer`.
2. Enter your player name.
3. Enter the host IP and port.
4. Click `Connect`.

### Current Online Scope

- Works as a lobby and session layer
- Host acts as the server
- Enforces max 4 players on the host side
- Lets the host launch a match for all connected players at the same time
- Does not yet synchronize live gameplay between machines

## Game State Logging

The game writes snapshot entries to:

```text
WinFormsApp2\bin\Debug\net8.0-windows\game_state_log.jsonl
```

Each line is a JSON object containing:

- reason or event
- current player
- scores
- placed tiles
- gems
- gateway ownership
- other runtime state

This exists to support future save/load, replay, and multiplayer synchronization work.

## Known Limitations

- Online lobby is implemented, but network gameplay sync is not finished yet
- The project currently targets Windows only
- Some UI layout values are still hand-tuned for the current board scale and form layout

## Planned Next Steps

- Host-authoritative multiplayer gameplay
- Sending moves and commands over the network
- Restoring game state from snapshots
