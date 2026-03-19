# Project Structure Plan

This note documents the intended folder layout for `WinFormsApp2` before moving files around.

## Target Structure

```text
WinFormsApp2/
  Program.cs
  Indigo.csproj

  UI/
    TitleScreenForm.cs
    TitleScreenForm.Designer.cs
    TitleScreenForm.resx

    Game/
      GameForm.cs
      GameForm.Designer.cs
      GameForm.resx
      PlayerForm.cs
      PlayerForm.Designer.cs
      PlayerForm.resx
      GameEndForm.cs
      GameEndForm.Designer.cs
      GameEndForm.resx

    Online/
      OnlineMultiplayerForm.cs
      OnlineMultiplayerForm.Designer.cs
      OnlineMultiplayerForm.resx

  GameLogic/
    Objects.cs

  Online/
    OnlineSessionModels.cs

  Common/
    ImageUtils.cs

  Resources/
    *.png
    *.gif

  Properties/
    Resources.Designer.cs
    Resources.resx
```

## Move Map

- `WinFormsApp2/TitleScreenForm*` -> `WinFormsApp2/UI/`
- `WinFormsApp2/GameForm*` -> `WinFormsApp2/UI/Game/`
- `WinFormsApp2/PlayerForm*` -> `WinFormsApp2/UI/Game/`
- `WinFormsApp2/GameEndForm*` -> `WinFormsApp2/UI/Game/`
- `WinFormsApp2/OnlineMultiplayerForm*` -> `WinFormsApp2/UI/Online/`
- `WinFormsApp2/Objects.cs` -> `WinFormsApp2/GameLogic/`
- `WinFormsApp2/OnlineSessionModels.cs` -> `WinFormsApp2/Online/`
- `WinFormsApp2/ImageUtils.cs` -> `WinFormsApp2/Common/`
- `WinFormsApp2/Resources/*` stays in `WinFormsApp2/Resources/`
- `WinFormsApp2/Properties/Resources.*` stays in `WinFormsApp2/Properties/`

## Notes

- Form `.cs`, `.Designer.cs`, and `.resx` files should stay together.
- `Properties/Resources.*` should remain in place because WinForms resource generation depends on it.
- This step is documentation only. File moves and namespace cleanup happen later.
