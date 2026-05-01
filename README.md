\# FirstApi



API REST développée en ASP.NET Core permettant la gestion de tâches avec authentification JWT et système multi-utilisateurs.



\---



\## Stack technique



\- ASP.NET Core Web API

\- Entity Framework Core

\- SQL Server (LocalDB)

\- JWT Authentication

\- AutoMapper

\- BCrypt (hash password)



\---



\## 🎯 Fonctionnalités



\### Authentification

\- Register utilisateur

\- Login utilisateur

\- Génération de token JWT

\- Hash sécurisé des mots de passe (BCrypt)



\### Gestion des tâches

\- CRUD complet (Create / Read / Update / Delete)

\- Tâches liées à un utilisateur

\- Protection des routes via `\[Authorize]`



\### Fonctionnalités avancées

\- Filtrage des tâches (status, titre)

\- Pagination

\- Tri dynamique

\- Endpoint “My Tasks” (user context via token)



\---



\## Sécurité



\- Authentification via JWT Bearer Token

\- UserId extrait du token (claims)

\- Hash des mots de passe avec BCrypt

\- Isolation des données par utilisateur (ownership)



\---



\## 📡 Endpoints principaux



\### Auth

\- POST `/api/auth/register`

\- POST `/api/auth/login`



\### Tasks

\- GET `/api/tasks`

\- GET `/api/tasks/{id}`

\- GET `/api/tasks/mine`

\- POST `/api/tasks`

\- PUT `/api/tasks/{id}`

\- DELETE `/api/tasks/{id}`



\---



\## ⚙️ Installation



```bash

dotnet restore

dotnet ef database update

dotnet run

