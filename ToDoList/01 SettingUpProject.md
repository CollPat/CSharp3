# Setting Up Final Project

## 💡 Creating ToDoList solution

```cmd
cd ToDoList
dotnet new sln --name ToDoList
```

## 📡 Creating ToDoList.WebApi project

```cmd
cd ToDoList
dotnet new web --name ToDoList.WebApi --output src/ToDoList.WebApi
dotnet sln add src/ToDoList.WebApi
```

## 📘 Creating ToDoList.Domain project

```cmd
cd ToDoList
dotnet new classlib --name ToDoList.Domain --output src/ToDoList.Domain
dotnet sln add src/ToDoList.Domain
```

## 🧪 Creating ToDoList.Test project

```cmd
cd ToDoList
dotnet new xunit --name ToDoList.Test --output tests/ToDoList.Test
dotnet sln add tests/ToDoList.Test
```

## 🗃️ Creating ToDoList.Persistency project

```cmd
cd ToDoList
dotnet new classlib --name ToDoList.Persistency --output src/ToDoList.Persistency
dotnet sln add src/ToDoList.Persistency
```

## 🌐 Creating ToDoList.Frontend project

```cmd
cd ToDoList
dotnet new blazor --interactivity None --empty --name ToDoList.Frontend --output src/ToDoList.Frontend
dotnet sln add src/ToDoList.Frontend
```
