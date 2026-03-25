# Copilot Instructions

## General Guidelines
- Use clsEventsManager for context menu actions in ucEntityManager (keep handler structure consistent with other actions).
- Ensure that the DetainedLicenses table uses the column name `ReleaseDate` (not `ReleasedDate`).
- When creating a detain, include `CreatedByUserID` from `Session.CurrentUser.UserID`.