# General Refactor Plan: Extracting MainForm-embedded pages into standalone UserControls (Pages/XXXPage)

This document defines a repeatable, end-to-end process to migrate any MainForm-embedded "design page" (e.g., Grids, Headers, Panels) into a dedicated UserControl living under `Applications/Source/Palette Designer/Pages/` with the class and component name pattern `XXXPage` (example: `GridPage.cs` / `GridPage`).

The plan encapsulates all lessons learned from the ControlsPage migration and ensures clean architecture, consistent theming, and minimal back-and-forth.

---

## Goals

- Replace inline page implementations inside `MainForm` with a dedicated `Pages/XXXPage` UserControl.
- Encapsulate each page’s UI, event wiring, and style-update logic inside the new control.
- Keep palette application centralized: `MainForm.ApplyPalette(...)` calls `xxxPage1.ApplyPalette(_palette)`.
- Remove all legacy references in `MainForm` and `MainForm.Designer` to the old inline page components.
- Maintain parity with existing page patterns (e.g., left navigator + border panel + demo controls inside a host `kryptonPanel1`).

---

## Naming and Structure

- Directory: `Applications/Source/Palette Designer/Pages/`
- Files and Names:
  - `XXXPage.cs` (public partial class XXXPage : UserControl)
  - `XXXPage.Designer.cs` (designer-generated)
- Component/Class Name: `XXXPage` (e.g., `GridPage`)
- MainForm field name: `xxxPage1` (added to the appropriate design page container, e.g., `pageDesignGrid`)

---

## Step-by-step Procedure

1) Analyze the inline page in MainForm
   - Identify all controls, labels, borders, and navigators currently embedded for the target page.
   - Identify all event handlers that are scoped to that page (e.g., SelectedPageChanged, CheckSet clicks).
   - Identify any “fixed state” or “demo state” logic that sets styles on the inline controls.
   - Identify any palette-dependent usage (e.g., reads/writes to Palette styles).

2) Create the new UserControl: Pages/XXXPage
   - Create a new `UserControl` named `XXXPage` with:
     - A host `KryptonPanel` named `kryptonPanel1` docked `Fill`.
     - A left-side `KryptonNavigator` (if the page uses style-selection tabs) named `kryptonNavigatorDesignXXX` configured like other pages:
       - Dock: Left
       - `NavigatorMode = BarCheckButtonOnly`
       - `Panel.PanelBackStyle = PaletteBackStyle.PanelAlternate`
       - `Button.ButtonDisplayLogic = None`
     - A slim left border `KryptonPanel` named `borderDesignXXX` docked Left (1px), `PanelBackStyle = PanelAlternate`.
     - Any demo controls (e.g., “Disabled” and “Normal” variants) and their labels as was in the inline page.
   - Ensure component names match a consistent pattern:
     - Navigator: `kryptonNavigatorDesignXXX`
     - Border: `borderDesignXXX`
     - Demo groups/controls: semantically equivalent to old inline names, scoping them within the new UserControl.
   - Add an `ApplyPalette(KryptonCustomPaletteBase palette)` public method:
     - Set `kryptonPanel1.Palette = palette` (like other pages).
     - If any child controls require direct palette assignment, set as needed.

3) Move event logic into the UserControl
   - For events like `SelectedPageChanged` on the left navigator, move the handler into `XXXPage`:
     - Name: `KryptonNavigatorDesignXXX_SelectedPageChanged`
     - Within the handler, compute styles based on `kryptonNavigatorDesignXXX.SelectedIndex` (e.g., back/border style, header style).
     - Apply those styles to internal demo controls of the page.
   - Any UI toggles/fixed states set on the inline MainForm controls should be set in the `XXXPage` constructor or relevant handlers (e.g., `SetFixedState` calls).
   - Keep handler responsibilities inside `XXXPage`. Do NOT keep them in `MainForm`.

4) Integrate the XXXPage into MainForm
   - In `MainForm.Designer.cs`, add an instance field `private Pages.XXXPage xxxPage1;`.
   - On the corresponding `pageDesignXXX` container (e.g., `pageDesignGrid`), add and dock `xxxPage1` with `DockStyle.Fill`.
   - Ensure `xxxPage1` is constructed during `InitializeComponent()` and added to the page.

5) Palette application pipeline
   - In `MainForm.ApplyPalette(...)`, add:
     - `xxxPage1.ApplyPalette(_palette);`
   - Remove any legacy palette application for old inline components related to this page (e.g., removing from `_applyPalettesToBases` collections if they referenced the old inline controls).

6) Remove old references from MainForm code
   - In `MainForm.cs`:
     - Remove event handlers for the inline page (e.g., `KryptonNavigatorDesignXXX_SelectedPageChanged`) that were tied to old inline fields.
     - Remove any direct references to old inline controls (e.g., changing `GroupBackStyle` on inline groups).
   - In `MainForm.Designer.cs`:
     - Remove or comment out any legacy fields for the inline page (old navigator, border panel, labels, demo groups).
     - Remove or comment out related `InitializeComponent` sections that initialize those old fields.
   - Ensure `MainForm` reflects page title/description by reading `xxxPage1` navigator state only if needed. Prefer letting `XXXPage` manage its own internal labels and only update MainForm page headers if required.

7) Consistency with other pages
   - Follow the pattern established by existing refactored pages:
     - `ApplyPalette` method.
     - Left navigator and alternate-panel styling for the left column.
     - Internal event handling for its own UI changes.
   - Keep naming and layout consistent to minimize maintenance friction.

8) Verification checklist
   - Build the solution after adding the new UserControl and removing old references.
   - Verify:
     - The `pageDesignXXX` displays the new `XXXPage` correctly.
     - The left side navigator in `XXXPage` changes and updates the internal demo controls properly.
     - Palette changes trigger `xxxPage1.ApplyPalette(_palette)` and the host `kryptonPanel1` uses the palette (dark/alternate style applied on left column).
     - No dangling references remain in `MainForm` to removed inline controls or events.
   - Run a search to ensure no leftover inline identifiers remain:
     - Search terms:
       - `kryptonNavigatorDesignXXX`
       - `borderDesignXXX`
       - `labelXXX*` that were part of the inline page
       - Inline demo control names (e.g., `group1Disabled`, `group1Normal`, etc.)
     - Remove any stragglers found outside `XXXPage`.

9) Coding conventions and guardrails
   - Keep `XXXPage` fully self-contained; do not expose internal demo controls to `MainForm`.
   - If `MainForm` needs state for headers/descriptions, expose only what’s necessary (e.g., a read-only `Navigator` property).
   - Always move the “old inline commands” (event logic) into the new UserControl handler rather than proxying from `MainForm`.
   - Maintain consistent `PaletteBackStyle`, `PaletteBorderStyle`, `HeaderStyle`, etc., mappings as per the original design.

---

## Example mapping for common style-switching

When the `kryptonNavigatorDesignXXX.SelectedIndex` changes:

- For “Controls-like” pages:
  - 0: `PaletteBackStyle.ControlClient`, `PaletteBorderStyle.ControlClient`
  - 1: `ControlAlternate`, `ControlAlternate`
  - 2: `ControlGroupBox`, `ControlGroupBox`
  - 3: `ControlToolTip`, `ControlToolTip`
  - 4: `ControlRibbon`, `ControlRibbon`
  - 5: `ControlCustom1`, `ControlCustom1`

- For “Headers-like” pages:
  - Map indices to `HeaderStyle` enums (Primary, Secondary, DockActive, DockInactive, Calendar, Form, Custom1, Custom2).

- For “Panels-like” pages:
  - Map indices to `PaletteBackStyle.PanelClient`, `PanelAlternate`, `PanelRibbonInactive`, `PanelCustom1`.

Replicate the corresponding original mappings for each page category to preserve parity.

---

## Common pitfalls to avoid

- Leaving the old `MainForm` event handler in place after moving logic into the `XXXPage`.
- Keeping duplicate fields for inline controls in `MainForm.Designer` after embedding `XXXPage`.
- Forgetting to add `xxxPage1.ApplyPalette(_palette)` in `MainForm.ApplyPalette`.
- Not setting the left-side navigator’s `Panel.PanelBackStyle = PanelAlternate`, causing inconsistent left-column theming.
- Exposing internal demo controls instead of keeping them encapsulated in `XXXPage`.

---

## Final acceptance criteria

- All page-specific behavior is inside `Pages/XXXPage`.
- `MainForm` only embeds `XXXPage` and applies the palette.
- No compile-time references remain to the old inline components or their events in `MainForm`.
- Visual parity with the original inline page is preserved.
- Theming is consistent, with the left column using the alternate treatment like other refactored pages.
