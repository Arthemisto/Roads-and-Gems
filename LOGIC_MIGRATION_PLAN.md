# Logic Migration Plan

This note tracks the next cleanup step after the folder migration and initial test setup.

## Goal

Move non-UI logic out of WinForms forms into testable classes.

## Current Direction

- UI stays in `WinFormsApp2/UI/`
- game rules and board logic move into `WinFormsApp2/GameLogic/`
- online/session logic moves into `WinFormsApp2/Online/`
- shared helpers stay in `WinFormsApp2/Common/`

## Already Moved

- `CreateGatewayOwners` logic -> `WinFormsApp2/GameLogic/GameBoardLogic.cs`
- `CreateHexGrid` logic -> `WinFormsApp2/GameLogic/GameBoardLogic.cs`
- online color mapping -> `WinFormsApp2/Online/OnlineLobbyLogic.cs`
- endpoint parsing -> `WinFormsApp2/Online/OnlineLobbyLogic.cs`

## Next Moves From GameForm

Source:
- `WinFormsApp2/UI/Game/GameForm.cs`

Move candidates:
- tile placement validation
  - `GetClosestIndex`
  - `BorderApproved`
  - `FindNeighbors`
  - `Snap` rules split into pure validation + UI side effects
  - target folder: `WinFormsApp2/GameLogic/`

- turn flow
  - `AdvanceTurn`
  - current player progression
  - online/local turn checks
  - target folder: `WinFormsApp2/GameLogic/`

- scoring and endgame
  - `ScoreUpdate`
  - endgame winner calculation
  - gateway scoring decisions
  - target folder: `WinFormsApp2/GameLogic/`

- board state setup
  - parts of `SetUpApp`
  - tile/gem setup rules
  - target folder: `WinFormsApp2/GameLogic/`

## Next Moves From OnlineMultiplayerForm

Source:
- `WinFormsApp2/UI/Online/OnlineMultiplayerForm.cs`

Move candidates:
- session message serialization/deserialization
  - `DeserializeEnvelope`
  - `SendEnvelopeAsync`
  - target folder: `WinFormsApp2/Online/`

- host/client session coordination
  - connection acceptance
  - join validation
  - broadcast helpers
  - target folder: `WinFormsApp2/Online/`

- player list/session state shaping
  - lobby state mapping
  - player color state generation
  - target folder: `WinFormsApp2/Online/`

## Suggested Order

1. Extract more pure game logic from `GameForm.cs`
2. Add tests for placement, turn flow, and scoring
3. Extract online session helpers from `OnlineMultiplayerForm.cs`
4. Add tests for envelope parsing and lobby state transitions
5. Split `Objects.cs` into separate model files

## Rule Of Thumb

If code needs WinForms controls to run, keep it in `UI/`.

If code can run without a form, move it into `GameLogic/`, `Online/`, or `Common/` and cover it with tests.
