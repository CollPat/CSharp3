# Feedback Assignment 5.1

Musel jsem vytvořit databázi aby to dělalo co má, poté to fungovalo bez problému.

Kde se asi stal ten problém proč jsi neměla databázi vytvořenou.

Příkaz

```cmd
dotnet ef database update
```

Vytvoří databázi a jde po cestě kterou máme definovanou jako Data source= **../../data/localdb.db**
Takže se databáze bude chtít vytvořit v místě

../ = jdi o složku výše
../ = jdi o složku výše
/data = jdi do složky data

a tam vytvoř localdb.db

Pokud ale tato cesta neexistuje (tedy složka **data** není vytvořená), tak to nemá validní cestu a databázi nám to nevytvoří. Příkaz NEvytvoří složku data, složka data v dané cestě už musí existovat!

Čili já jsem si to zproviznil jednoduše že jsem si vytvořil v ToDoList složku data (jak to bylo v lekci) a pak už mi tento příkaz fungoval ;)

Jinak ostatní vše ostatní funguje jak má.

Záměrně ti v rámci zpětné vazby neposílám vytvořenou databázi, zkus si to udělat.
Připomínám postup:
1) vytvoř složku data v ToDoList složce (bude to na stejné úrovni jako src nebo docs)
2) otevři si ToDoList.Persistence v integrovaném terminálu
3) použij command dotnet ef database update
4) zkontroluj si že ti to vytvořilo soubor localdb.db ve složce data
