# EduFlowHub

Backend-система для онлайн-обучения на ASP.NET Core. Преподаватели создают курсы и загружают к ним учебные материалы (лекции, методички, изображения), студенты записываются на опубликованные курсы и скачивают материалы.

## Возможности

- Курсы с категориями, ценой и жизненным циклом **Draft → Published → Archived**
- Учебные материалы: файлы с порядковым номером внутри курса, загрузка и скачивание
- Запись студентов на курсы
- Роли **Admin**, **Teacher**, **Student** и проверка прав на конкретный курс:
  - преподаватель управляет только своими курсами;
  - студент видит материалы только тех курсов, на которые записан
- JWT-аутентификация, хеширование паролей (BCrypt), блокировка аккаунта на 15 минут после 5 неверных паролей
- Фильтрация и пагинация на стороне БД, ограничение частоты запросов (rate limiting)
- Документация API в Swagger

## Стек

.NET 10 · ASP.NET Core Web API · EF Core · PostgreSQL · MediatR · AutoMapper · FluentValidation · Docker Compose · Swagger

## Структура

| Проект | Назначение |
|---|---|
| `EduFlowHub.Domain` | Сущности и бизнес-правила |
| `EduFlowHub.Application` | Сценарии (команды и запросы MediatR), проверка прав, DTO |
| `EduFlowHub.Infrastructure` | JWT, хеширование паролей, файловое хранилище |
| `EduFlowHub.Persistence` | EF Core: контекст, репозитории, миграции |
| `EduFlowHub.Api` | Контроллеры, валидация, обработка ошибок, Swagger |

---

## Запуск в Docker

**Требуется:** Docker и Docker Compose.

**1.** Создайте файл `.env` в корне репозитория:

```dotenv
DB_PORT=5432
DB_NAME=eduflowhub
DB_USER=postgres
DB_PASSWORD=postgres

# Ключ подписи JWT, не короче 32 символов
JWT_KEY=replace_with_random_string_at_least_32_chars

STORAGE_ROOT_PATH=/var/app/storage

# Development включает Swagger и тестовые данные
ASPNETCORE_ENVIRONMENT=Development

ADMIN_USERNAME=admin
ADMIN_PASSWORD=Admin123
```

**2.** Запустите:

```bash
docker compose up --build
```

Сначала стартует PostgreSQL, затем контейнер `migration` применяет миграции, после чего запускается API. Загруженные файлы хранятся в Docker volume `storage`.

| Сервис | Адрес |
|---|---|
| API | http://localhost:5292/api/v1 |
| Swagger | http://localhost:5292/swagger |

Остановить: `docker compose down`. Остановить и удалить данные: `docker compose down -v`.

---

## Локальный запуск

**Требуется:** .NET 10 SDK, PostgreSQL, инструмент `dotnet-ef` (`dotnet tool install --global dotnet-ef`).

**1.** Создайте файл `.env.development` в корне репозитория:

```dotenv
ConnectionStrings__Postgres__DB_HOST=localhost
ConnectionStrings__Postgres__DB_PORT=5432
ConnectionStrings__Postgres__DB_NAME=eduflowhub
ConnectionStrings__Postgres__DB_USER=postgres
ConnectionStrings__Postgres__DB_PASSWORD=postgres

Authentication__Jwt__SecretKey=replace_with_random_string_at_least_32_chars

FileStorage__Local__RootPath=storage

InitialAdmin__Username=admin
InitialAdmin__Password=Admin123

ASPNETCORE_ENVIRONMENT=Development
```

**2.** Примените миграции и запустите API:

```bash
dotnet ef database update --project EduFlowHub.Persistence --startup-project EduFlowHub.Persistence
dotnet run --project EduFlowHub.Api
```

API: http://localhost:5292, Swagger: http://localhost:5292/swagger

---

## Тестовые пользователи

| Роль | Логин | Пароль |
|---|---|---|
| Admin | `admin` | `Admin123` (значения из `ADMIN_USERNAME` / `ADMIN_PASSWORD`) |
| Teacher | `teacher` | `Teacher123` |
| Student | `student` | `Student123` |

- Администратор создаётся при первом запуске, если в базе ещё нет ни одного администратора.
- Преподаватель, студент, 4 категории и 5 черновиков курсов создаются автоматически в окружении `Development`.

## Как пользоваться API

1. Получите токен: `POST /api/v1/auth/login` с телом `{ "username": "teacher", "password": "Teacher123" }`.
2. В Swagger нажмите **Authorize** и вставьте токен; для curl добавьте заголовок `Authorization: Bearer <token>`.
3. Основной сценарий:
   - преподаватель создаёт курс (`POST /api/v1/courses/teachers/me`);
   - загружает материал (`POST /api/v1/courses/{courseId}/lesson-materials`, form-data: `Title`, `OrderNumber`, `File`);
   - публикует курс (`POST /api/v1/courses/teachers/me/{courseId}/publish`);
   - студент находит курс в каталоге (`GET /api/v1/courses/published`);
   - записывается (`POST /api/v1/courses/{courseId}/enroll`);
   - скачивает материал (`GET /api/v1/courses/{courseId}/lesson-materials/{id}/file`).

Можно загружать файлы `.pdf`, `.jpg`, `.webp`, `.png`. Список расширений и максимальный размер файла задаются в `appsettings.json` → `FileStorage:Local`.

Полный список эндпоинтов, параметров и кодов ответа — в Swagger.
