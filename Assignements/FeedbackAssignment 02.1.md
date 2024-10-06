# Feedback ukolu 02.1

Jsem velmi rád, že sis našla čas poupravit Greed kata :) Cvičení dělá mistry.

V domácím úkolu máš jeden nedostatek, který ale spíše přisuzuju drobnému nepochopení zadání, mluvím o posledním bodu
Nakonec přidejte novou cestu `/nazdarSvete`, která vás pozdraví v češtině a otestujte si ji v prohlížeči.

cílem nebylo změnit text "Hello world" na český text jak to máš v NewToDoList\src\ToDoList.WebApi\Program.cs

´´´
app.MapGet("/", () => "Zdravím svet!");
´´´

ale ponechat původní
´´´
app.MapGet("/", () => "Hello world!");
´´´
a k tomuto přidat featuru kdy pokud zadáš cestu localhost:*PortNumber*/nazdarSvete, tak nás naše webové API pozdraví česky.

Řešení je jednořádkové, trochu poradím. V Program.cs najdeš předpřipravený kód s vysvětlením.

Poprosím doopravit, ale i kdyby jsi to náhodou nestihla, tak úkol považuju za splněný.
Kdyby jsi měla jakékoliv další otázky / připomínky klidně i k mým zpětným vazbám, neváhej se ozvat.
