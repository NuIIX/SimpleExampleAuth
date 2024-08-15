# SimpleExampleAuth 🎉

## 🚀 О проекте

Добро пожаловать в **SimpleExampleAuth** — простой, но мощный пример реализации системы авторизации. Этот проект создан для того, чтобы наглядно продемонстрировать, как быстро и эффективно организовать систему аутентификации с использованием современных технологий.

### 🌟 Основные возможности

- **Классическая авторизация** через почту и пароль.
- **Авторизация через GitHub SSO** с использованием куки.
- **JWT-токены** для защиты пользовательских данных.
- **SHA256 шифрование** для безопасного хранения паролей.
- **Регистрация пользователей** с сохранением данных в защищённой базе данных.
- **Контроллер AccountController** для управления регистрацией и авторизацией.

## 🛠 Технические детали

- **JWT + SHA256**: Обеспечивает безопасную авторизацию с помощью токенов.
- **GitHub SSO**: Быстрая авторизация через GitHub с использованием куки.
- **База данных**: Надежное хранение информации о пользователях.
- **Структура проекта**: Все файлы структурированы и находятся в корневой папке `wwwroot` (`.cshtml`, `.css`, `.js`).

Проект является логическим продолжением [RedisTutorialProject](https://github.com/your-username/RedisTutorialProject), сохраняя его микросервисный функционал Redis и добавляя новые возможности.

## 📂 Структура проекта

```plaintext
SimpleExampleAuth/
│
├── Properties/
│   └── launchSettings.json              # Конфигурация для запуска проекта
│
├── wwwroot/
│   ├── css/                             # Стили для различных страниц
│   │   ├── CreateStyles.css
│   │   ├── EditStyles.css
│   │   ├── IndexStyles.css
│   │   ├── LoginStyles.css
│   │   └── RegStyles.css
│   │
│   ├── js/                              # Скрипты для различных страниц
│       ├── CreateScripts.js
│       ├── EditScripts.js
│       ├── IndexScripts.js
│       ├── LoginScripts.js
│       └── RegScripts.js
│
├── Controllers/
│   ├── AccountController.cs             # Контроллер для регистрации и авторизации пользователей
│   └── HomeController.cs                # Контроллер для домашней страницы
│
├── Models/
│   ├── Car.cs                           # Модель автомобиля
│   ├── CarContext.cs                    # Контекст базы данных для автомобилей
│   ├── RedisCacheService.cs             # Сервис для работы с кэшем Redis
│   ├── User.cs                          # Модель пользователя
│   └── UserContext.cs                   # Контекст базы данных для пользователей
│
├── Views/
│   ├── Account/
│   │   ├── Login.cshtml                 # Страница авторизации
│   │   └── Registration.cshtml          # Страница регистрации
│   │
│   ├── Home/
│   │   ├── Index.cshtml                 # Главная страница сайта
│   │   ├── Create.cshtml                # Страница создания нового элемента
│   │   └── Edit.cshtml                  # Страница редактирования элементов
│   │               
│   ├── _ViewImports.cshtml              # Общие импорты для представлений
│   │
│   └── Shared/
│       └── _Layout.cshtml               # Общий шаблон для всех страниц
│
├── appsettings.json                     # Настройки конфигурации проекта
├── JWTAuthOptions.cs                    # Конфигурация параметров JWT авторизации
└── Program.cs                           # Основная точка входа в приложение



## Установка 💻

Вот пошаговое руководство по установке проекта **SimpleExampleAuth** на вашем локальном компьютере:

1. **Клонирование репозитория**
    - Для начала, клонируйте репозиторий на ваш локальный компьютер. Используйте следующую команду:
    ```bash
    git clone https://github.com/your-username/SimpleExampleAuth.git
    ```

2. **Настройка базы данных**
    - Проект требует настройки базы данных для хранения данных пользователей. Убедитесь, что у вас установлена и настроена нужная база данных (например, SQL Server или другой). В файле `appsettings.json` настройте строку подключения к вашей базе данных.

3. **Установка зависимостей**
    - Перейдите в каталог проекта и выполните команду:
    ```bash
    dotnet restore
    ```
    Эта команда установит все необходимые зависимости, указанные в файле проекта.

4. **Запуск приложения**
    - После установки зависимостей, запустите приложение командой:
    ```bash
    dotnet run
    ```
    Приложение будет запущено на локальном сервере, и в командной строке вы увидите URL, по которому доступен ваш проект.

## Использование 🕹️

**SimpleExampleAuth** предоставляет следующие возможности:

- **Регистрация пользователей**: Создавайте новые аккаунты с надёжным шифрованием паролей.
- **Авторизация**: Входите в систему с помощью традиционного метода (почта и пароль) или через GitHub SSO.
- **Защита данных**: Все данные пользователей надёжно защищены с использованием JWT и SHA256 шифрования.

Приложение также включает в себя управление пользователями, поддерживает регистрацию и авторизацию на страницах, которые связаны с вашей базой данных. Весь интерфейс разбит на `.cshtml`, `.css`, и `.js` файлы, обеспечивая чёткое разделение логики, стилей и скриптов.