# FirstApi
 
> API REST développée en ASP.NET Core — gestion de tâches avec authentification JWT et système multi-utilisateurs.
 
![.NET](https://img.shields.io/badge/.NET_Core-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![JWT](https://img.shields.io/badge/JWT-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)
![EF Core](https://img.shields.io/badge/EF_Core-68217A?style=flat-square&logo=nuget&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)
![AutoMapper](https://img.shields.io/badge/AutoMapper-BE4B48?style=flat-square)
![BCrypt](https://img.shields.io/badge/BCrypt-3D3D3D?style=flat-square)
 
---
 
## Architecture
 
```
Client
  │
  ▼
JWT Middleware ──── valide le token, extrait les claims (UserId)
  │
  ▼
Controllers (AuthController / TaskController)
  │   └── [Authorize] · AutoMapper DTOs
  │
  ├── BCrypt ──── hash des mots de passe
  │
  ▼
EF Core DbContext
  │
  ▼
SQL Server (LocalDB)
```
 
---
 
## Stack technique
 
| Technologie | Rôle |
|---|---|
| ASP.NET Core Web API | Framework principal |
| Entity Framework Core | ORM + migrations |
| SQL Server (LocalDB) | Base de données |
| JWT Authentication | Sécurité des routes |
| AutoMapper | Mapping entités ↔ DTOs |
| BCrypt | Hash des mots de passe |
 
---
 
## Fonctionnalités
 
### Authentification
- Register & login utilisateur
- Génération de token JWT
- Hash sécurisé des mots de passe (BCrypt)
### Gestion des tâches
- CRUD complet (Create / Read / Update / Delete)
- Tâches liées à un utilisateur
- Protection des routes via `[Authorize]`
### Fonctionnalités avancées
- Filtrage des tâches (status, titre)
- Pagination
- Tri dynamique
- Endpoint `GET /api/tasks/mine` — user context extrait du token
---
 
## Endpoints
 
### Auth
```
POST   /api/auth/register
POST   /api/auth/login          → retourne un JWT token
```
 
### Tasks
```
GET    /api/tasks               filtre · pagination · tri
GET    /api/tasks/{id}
GET    /api/tasks/mine          user context via token
POST   /api/tasks
PUT    /api/tasks/{id}
DELETE /api/tasks/{id}
```
 
> Routes protégées par `[Authorize]`.
 
---
 
## Sécurité
 
- Authentification via **JWT Bearer Token**
- `UserId` extrait des claims à chaque requête
- Mots de passe hashés avec **BCrypt** — jamais stockés en clair
- Isolation des données par utilisateur (ownership) — un user ne peut pas accéder aux tâches d'un autre
---
