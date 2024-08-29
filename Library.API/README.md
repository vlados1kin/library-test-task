Для запуска проекта выполните следующие действия:
* Клонируйте данный репозиторий на свой компьютер `git clone https://github.com/vlados1kin/library-test-task`
* Выполните команду `dotnet restore`
* Перейдите в директорию `cd Library.API`
* Примените миграцию `dotnet ef database update`
* Убедитесь, что у вас установлен MS SQL Server
* Запустите решение `dotnet run`
* Если приложение не открылось автоматически в браузере, то перейдите по адресу `http://localhost:5244/swagger/index.html`