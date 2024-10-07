var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello world!");
/*
app.MapGet - co tato metoda dělá: zjednodusene receno stanovi nasi webove api jak reagovat na GET request pro urcitou URL adresu

copak udela teda tento radek
app.MapGet("/", () => "Hello world!");

Pokud prijde GET request na adresu api (u naseho lokalniho testovani to je nase zname localhost:PortNumber)
cela cesta je vlastne http://localhost:PortNumber/
tak posle zpatky odpoved "Hello world!"
*/

//prosim doplnit :) kod je zakomentovany aby compiler nekricel zlosti
//app.MapGet("/nejaka moje nova cesta", () => "Zdravím svet!");
//vysledkem by melo by ze kdyz spustis aplikaci a do browseru das cestu localhost:PortNumber/(moje nova cesta), tak ti to navrati pozdraveni v cestine :)

app.Run();
