# Pill Identifier — Solution Documentation

## Solution File
`PillIdentifierAPI.sln` — Visual Studio 2013, .NET Framework 4.5, 4 projects.

---

## Projects

### 1. ClassChung — Class Library
- **Output:** `ClassChung.dll` (referenced by API and Form)
- **Namespace:** `ClassChung`
- **Target:** .NET 4.5

#### Purpose
Shared data access layer. All SQL Server interaction lives here via LINQ to SQL.

#### Key Files
| File | Role |
|---|---|
| `ClassLibrary.cs` | All database operation classes |
| `DataClasses.dbml` | LINQ-to-SQL schema designer (do not hand-edit) |
| `DataClasses.designer.cs` | Auto-generated ORM context (do not hand-edit) |
| `app.config` | Connection strings (local and remote) |

#### Architecture
`KetnoiDB` is the top-level class containing four nested operation classes:
- `KetnoiDB.InsertData` — INSERT operations
- `KetnoiDB.GetData` — SELECT/READ operations
- `KetnoiDB.UpdateData` — UPDATE operations
- `KetnoiDB.DeleteData` — DELETE operations

Domain model classes (e.g. `Thuoc`, `HoatChat`, `NhanDangThuoc`) are plain C# POCOs defined in `ClassLibrary.cs`, separate from the LINQ-to-SQL generated DB entity classes. Each POCO has a `.toXxxDB()` method to convert to the corresponding DB entity.

#### Connection Strings (in app.config)
| Name | Target |
|---|---|
| `PillIDConnectionString_Local` | `DESKTOP-SP0BRKD` (local dev machine) |
| `PillIDConnectionString_Server` | `103.140.249.152` (remote server) |
| `PillIDConnectionString1` | Same remote server (used by DataClasses.dbml) |

---

### 2. PillIdentifierAPI — ASP.NET Web API
- **Output:** Web application (IIS Express)
- **Port:** 6844 (`http://localhost:6844/`)
- **Namespace:** `PillIdentifierAPI`
- **References:** ClassChung, ASP.NET Web API 5.0.0, Newtonsoft.Json 5.0.6

#### Purpose
RESTful HTTP API. Wraps ClassChung and exposes all CRUD operations as HTTP endpoints.

#### Key Files
| File | Role |
|---|---|
| `Controllers/PillIdentifierController.cs` | All API endpoints (~1000 lines) |
| `App_Start/WebApiConfig.cs` | Enables attribute routing, adds JSON for text/html |
| `App_Start/RouteConfig.cs` | MVC route (default) |
| `Global.asax.cs` | Application startup |

#### Route Pattern
```
GET    api/v1/GetData/{OperationName}
POST   api/v1/InsertData/{OperationName}
PUT    api/v1/UpdateData/{OperationName}
DELETE api/v1/DeleteData/{OperationName}/{id}
```

#### All Endpoints

**GET (GetData)**
| Endpoint | Returns |
|---|---|
| `GetDSChidinh` | All indications |
| `GetDSDangThuoc` | All drug forms |
| `GetDSHinhDang` | All shapes |
| `GetDSHoatChat` | All active substances |
| `GetDSHoatChatGoc` | All generic/original active substances |
| `GetDSLoaiRanh` | All groove types |
| `GetDSLoaiViThuoc` | All blister/package types |
| `GetDSMauSac` | All colors |
| `GetDSThuoc` | All drugs |
| `SearchChiDinh?keyword=` | Search indications by keyword |
| `GetIDHoatChatbyChiDinh?idChiDinh=` | Active substance IDs for an indication |
| `GetDSHinhAnhbyThuoc?idThuoc=` | Images for a drug |
| `GetNhanDangThuoc` | **Main pill identification search** — see params below |

**GetNhanDangThuoc parameters (all optional)**
```
imprint      (string)  — text on pill
idMausac1    (int?)    — color 1
idMausac2    (int?)    — color 2
idHinhdang   (int?)    — shape
idDangthuoc  (int?)    — drug form
idLoaiVi     (int?)    — blister type
idLoaiRanh   (int?)    — groove type
```

**POST (InsertData)**
InsertChiDinh, InsertDangThuoc, InsertHinhDang, InsertHoatChat, InsertHoatChatGoc, InsertLoaiRanh, InsertLoaiViThuoc, InsertMauSac, InsertThuoc, InsertHoatChat_HoatChatGoc, InsertHoatChatGoc_ChiDinh, InsertThuoc_MauSac, InsertNhanDangThuoc

**PUT (UpdateData)**
UpdateChiDinh, UpdateDangThuoc, UpdateHinhAnhThuocChiTiet, UpdateHinhDang, UpdateHoatChat, UpdateHoatChatGoc, UpdateLoaiRanh, UpdateLoaiViThuoc, UpdateMauSac, UpdateThuoc, UpdateNhanDangThuoc

**DELETE (DeleteData)**
DeleteChiDinh/{id}, DeleteDangThuoc/{id}, DeleteHinhAnhThuocChiTiet/{id}, DeleteHinhDang/{id}, DeleteHoatChat/{id}, DeleteHoatChatGoc/{id}, DeleteLoaiRanh/{id}, DeleteLoaiViThuoc/{id}, DeleteMauSac/{id}, DeleteThuoc/{id}, DeleteNhanDangThuoc/{id}

#### Response Format
All endpoints return:
```json
{ "Success": true/false, "Data": <T>, "Message": "..." }
```

---

### 3. PillIdentifierForm — WinForms Desktop App
- **Output:** `PillIdentifierForm.exe`
- **Namespace:** `PillIdentifierForm`
- **References:** ClassChung, System.Windows.Forms, System.Drawing

#### Purpose
Admin/data-entry desktop tool for pharmacists and database administrators. Manages reference data and pill identification records.

#### Key Files
| File | Role |
|---|---|
| `Program.cs` | Entry point — `Application.Run(new Giaodienchinh())` |
| `Giaodienchinh.cs` | Main MDI window |
| `Forms/Danhmuc/*.cs` | CRUD forms for all reference data tables |
| `Forms/Thietlap/*.cs` | Configuration forms for many-to-many relationships and bulk import |
| `Forms/Tracuu/Tracuu.cs` | Pill search/lookup form |

#### Form Groups
**Danhmuc (Reference Data Management)**
- `DanhmucThuoc` — Drugs
- `DanhmucHoatChat` — Active substances
- `DanhmucHoatChatGoc` — Generic active substances
- `DanhmucChiDinh` — Indications
- `DanhmucMauSac` — Colors
- `DanhmucHinhDang` — Shapes
- `DanhmucDangThuoc` — Drug forms
- `DanhmucLoaiRanh` — Groove types
- `DanhmucLoaiViThuoc` — Blister/package types

**Thietlap (Configuration & Import)**
- `FormNhanDangThuoc` — Pill identification record setup
- `Form_HoatChat_HoatChatGoc` — Link active substances to generic substances
- `Form_HoatChatGoc_ChiDinh` — Link generic substances to indications
- `Form_MauSacThuoc` — Link colors to drugs
- `ImportThuoc_MauSac` — Bulk import: drug-color mappings
- `Import_HoatChatGoc_ChiDinh` — Bulk import: generic substance-indication mappings
- `Import_HoatChat_HoatChatGoc` — Bulk import: active substance-generic substance mappings

**Tracuu (Lookup)**
- `Tracuu` — Search pills by visual attributes

---

### 4. PillIdentifierMVC — ASP.NET MVC 5 Web App
- **Output:** Web application (IIS Express)
- **Port:** 23762 (`http://localhost:23762/`)
- **Namespace:** `PillIdentifierMVC`
- **References:** ASP.NET MVC 5.0.0, Entity Framework 6.0.0, ASP.NET Identity (OWIN), Newtonsoft.Json

#### Purpose
User-facing web portal. Currently a blank Bootstrap scaffold. **Intended target for the user pill identification website.**

#### Key Files
| File | Role |
|---|---|
| `Controllers/HomeController.cs` | Index/About/Contact — currently empty boilerplate |
| `Controllers/AccountController.cs` | Login, Register, external auth |
| `Views/Shared/_Layout.cshtml` | Shared Bootstrap layout |
| `App_Start/RouteConfig.cs` | Default MVC routing |
| `App_Start/Startup.Auth.cs` | OWIN/Identity configuration |
| `Models/IdentityModels.cs` | Entity Framework Identity context |

#### Does NOT yet reference ClassChung
Adding a ClassChung project reference and the PillID connection string to Web.config is the first step to implement the pill identification feature.

---

## Database — SQL Server `PillID`

### Table Prefix Conventions
- `d_` — Reference/dictionary tables (static data)
- `w_` — Working/operational tables (main data)
- `r_` — Relation/junction tables (many-to-many)

### Tables

| Table | Primary Key | Purpose |
|---|---|---|
| `d_Thuoc` | IDThuoc | Drugs/medications |
| `d_HoatChat` | IDHoatChat | Active substances (ingredients) |
| `d_HoatChatGoc` | IDHoatChatGoc | Generic/original active substances |
| `d_ChiDinh` | IDChiDinh | Therapeutic indications |
| `d_HinhDang` | IDHinhDang | Pill shapes |
| `d_DangThuoc` | IDDangThuoc | Drug dosage forms |
| `d_MauSac` | IDMauSac | Colors |
| `d_LoaiViThuoc` | IDLoaiViThuoc | Blister/packaging types |
| `d_LoaiRanh` | IDLoaiRanh | Groove/score line types |
| `w_NhanDangThuoc` | IDNhanDang | Pill identification records — links a drug to its visual attributes |
| `d_HinhAnhThuocChiTiet` | IDHinhAnh | Detail images per identification record |
| `r_HoatChat_HoatChatGoc` | — | Many-to-many: active substance ↔ generic substance |
| `r_HoatChatGoc_ChiDinh` | — | Many-to-many: generic substance ↔ indication |
| `r_Thuoc_MauSac` | — | Many-to-many: drug ↔ color |

### w_NhanDangThuoc — Key Columns
| Column | Type | Notes |
|---|---|---|
| IDNhanDang | int | PK, identity |
| IDThuoc | int | FK → d_Thuoc |
| CoKhacDau | bit | Has imprint |
| KhacDauMatTruoc | nvarchar(100) | Front imprint text |
| KhacDauMatSau | nvarchar(100) | Back imprint text |
| IDHinhDang | int | FK → d_HinhDang |
| IDDangThuoc | int | FK → d_DangThuoc |
| IDLoaiViThuoc | int? | FK → d_LoaiViThuoc |
| IDLoaiRanh | int? | FK → d_LoaiRanh |
| MaHinh | nvarchar(100) | Image code |
| KichThuoc | float? | Size/diameter |

---

## Domain Terminology (Vietnamese → English)

| Vietnamese | English |
|---|---|
| Thuốc | Drug / Medication |
| Hoạt chất | Active substance / Ingredient |
| Hoạt chất gốc | Generic / original active substance |
| Chỉ định | Therapeutic indication |
| Hình dạng | Shape |
| Dạng thuốc / Dạng bào chế | Dosage form |
| Màu sắc | Color |
| Loại vỉ thuốc | Blister/packaging type |
| Loại rãnh | Groove/score line type |
| Nhận dạng thuốc | Pill identification record |
| Hình ảnh | Image |
| Khắc dầu | Imprint (engraving on pill surface) |
| Hàm lượng | Dosage / concentration |
| Nhà sản xuất | Manufacturer |
| Ghi chú | Notes |
| Tìm kiếm | Search |
| Tra cứu | Lookup |
| Danh mục | Catalog / reference list |
| Thiết lập | Configuration / setup |

---

## Architecture Diagram

```
SQL Server (PillID)
        |
        | LINQ to SQL
        v
  ┌─────────────┐
  │  ClassChung │  (Class Library — shared by all consumers)
  └──────┬──────┘
         │ referenced by
    ┌────┴─────────────────────┐
    │                          │
    v                          v
┌──────────────────┐   ┌──────────────────┐
│ PillIdentifierAPI│   │PillIdentifierForm│
│  Web API :6844   │   │  WinForms .exe   │
│  (REST backend)  │   │  (Admin desktop) │
└──────────────────┘   └──────────────────┘

┌──────────────────────┐
│  PillIdentifierMVC   │  ← Target for user website
│  MVC 5 Web :23762    │    (currently blank scaffold)
│  (does NOT yet ref   │    Needs ClassChung reference
│   ClassChung)        │
└──────────────────────┘
```

---

## User Website Implementation Plan

Target project: **PillIdentifierMVC**

### Steps
1. Add ClassChung project reference to `PillIdentifierMVC.csproj`
2. Add PillID connection string to `PillIdentifierMVC\Web.config`
3. Create `Models\PillSearchViewModel.cs` — filter input model + results wrapper
4. Extend `Controllers\HomeController.cs`:
   - `Index()` — loads all dropdown data (shapes, colors, forms, indications, etc.)
   - `Search(PillSearchFilter)` POST — calls `KetnoiDB.GetData.GetNhanDangThuoc(...)`, returns results view
5. Create `Views\Home\Index.cshtml` — search form (matches `Design/mainpage.png`)
6. Create `Views\Home\Results.cshtml` — result cards (matches `Design/resultpage.png`)

### Design Reference
- `Design/mainpage.png` — search form with all filter fields
- `Design/resultpage.png` — result list with drug info cards and XEM CHI TIẾT button

### Data Flow
```
User fills form → POST HomeController.Search()
  → KetnoiDB.GetData.GetNhanDangThuoc(imprint, colorIds, shapeId, formId, blisterType, grooveType)
  → List<Thuoc> → Results.cshtml
```
