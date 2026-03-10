# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build Commands

Build the entire solution via MSBuild (requires Visual Studio or Build Tools installed):
```
msbuild PillIdentifierAPI.sln /p:Configuration=Debug
msbuild PillIdentifierAPI.sln /p:Configuration=Release
```

Build a single project:
```
msbuild ClassChung\ClassChung.csproj
msbuild PillIdentifierAPI\PillIdentifierAPI.csproj
```

There are no automated tests in this solution.

## Architecture Overview

This is a .NET Framework 4.5 Visual Studio 2013 solution with four projects:

### `ClassChung` (Class Library — shared data layer)
The central shared library referenced by both `PillIdentifierAPI` and `PillIdentifierForm`. All database logic lives here.

- **`ClassLibrary.cs`** — Contains the `KetnoiDB` class with four nested inner classes: `InsertData`, `UpdateData`, `DeleteData`, `GetData`. Each method maps directly to a DB operation.
- **`DataClasses.dbml`** — LINQ to SQL designer file. `DataClassesDataContext` is the auto-generated data context. **Do not edit `DataClasses.designer.cs` manually.**
- **`app.config`** — Holds multiple named connection strings for local (`DESKTOP-SP0BRKD`) and remote (`103.140.249.152`) SQL Server instances, all targeting database `PillID`.

The domain models in `ClassLibrary.cs` (e.g. `Thuoc`, `HoatChat`, `NhanDangThuoc`) are plain C# classes separate from the LINQ-to-SQL generated DB entities. Each domain model has a `.toXxxDB()` conversion method that maps to the DB entity.

### `PillIdentifierAPI` (ASP.NET Web API)
REST API hosted via IIS Express on port 6844. References `ClassChung` for all data access.

- Single controller: `Controllers/PillIdentifierController.cs` — attribute-routed, wraps all responses in `ApiResponse<T> { Success, Data, Message }`.
- Route pattern: `api/v1/{GetData|InsertData|UpdateData|DeleteData}/{OperationName}`
- Key endpoint: `GET api/v1/GetData/GetNhanDangThuoc` accepts optional filters (imprint, color IDs, shape ID, etc.) for pill identification search.
- JSON responses are enabled for `text/html` content type (configured in `WebApiConfig.cs`).

### `PillIdentifierForm` (WinForms desktop app)
Admin/data-entry desktop application. Also references `ClassChung`.

- **`Giaodienchinh.cs`** — Main window / entry point.
- **`Forms/Danhmuc/`** — CRUD forms for reference data (drugs, active substances, colors, shapes, groove types, blister types, indications).
- **`Forms/Thietlap/`** — Configuration forms for many-to-many relationships and bulk import (e.g. `ImportThuoc_MauSac` for importing drug-color mappings).
- **`Forms/Tracuu/`** — Pill lookup/search form.

### `PillIdentifierMVC` (ASP.NET MVC 5)
Separate MVC web application on port 23762. Uses Entity Framework 6 and ASP.NET Identity (OWIN). Does not reference `ClassChung`. Currently contains only the default scaffolded Account and Home controllers.

## Database Schema (SQL Server — `PillID`)

Tables prefixed `d_` are reference/dictionary tables; `w_` are working/operational tables; `r_` are relation/junction tables.

| Table | Purpose |
|---|---|
| `d_Thuoc` | Drugs/medications |
| `d_HoatChat` | Active substances |
| `d_HoatChatGoc` | Generic/original active substances |
| `d_ChiDinh` | Therapeutic indications |
| `d_HinhDang` | Pill shapes |
| `d_DangThuoc` | Drug dosage forms |
| `d_MauSac` | Colors |
| `d_LoaiViThuoc` | Blister/packaging types |
| `d_LoaiRanh` | Groove/score types |
| `w_NhanDangThuoc` | Pill identification records (links drug to visual attributes) |
| `d_HinhAnhThuocChiTiet` | Detail images per identification record |
| `r_HoatChat_HoatChatGoc` | Many-to-many: active substance ↔ generic substance |
| `r_HoatChatGoc_ChiDinh` | Many-to-many: generic substance ↔ indication |
| `r_Thuoc_MauSac` | Many-to-many: drug ↔ color |

## Domain Terminology (Vietnamese → English)

- Thuốc = Drug/Medication
- HoạtChất = Active substance
- HoạtChatGoc = Original/generic active substance
- ChỉĐịnh = Indication
- HìnhDạng = Shape
- DạngThuốc = Dosage form
- MàuSắc = Color
- LoạiVỉThuốc = Blister/packaging type
- LoạiRãnh = Groove/score line type
- NhậnDạngThuốc = Pill identification record
- HìnhẢnh = Image
- KhắcDầu = Imprint (engraved marking on pill)
