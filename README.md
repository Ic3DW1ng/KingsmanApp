# KingsmenApp

WPF-приложение (.NET Framework 4.8) по макетам «КИНГСМЭН».

## Запуск
1. Откройте `KingsmenApp.sln` в Visual Studio (2019/2022) с workload «Разработка классических приложений .NET».
2. Нажмите F5.

Тестовый вход: логин `admin`, пароль `admin`.

## Структура
- `AuthWindow` — авторизация
- `RegistrationWindow` — регистрация (поля со «*» необязательны)
- `MainWindow` — главная страница
- `ServicesWindow` / `EditServiceWindow` — список и редактирование услуг
- `Services/Storage.cs` — хранилище в памяти (данные не сохраняются после выхода)
- `Styles.xaml` — цвета и стили кнопок, полей, заголовков
