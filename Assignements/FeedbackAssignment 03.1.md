# Feedback ukolu 03.1

Skvele udelane, pouze male detaily ve formatovani, odsazovani, plus male drobnosti.

Jedna vec, smazal jsem z ToDoList.Domain.csproj závislost na ToDoList.WebApi.csproj, nějak se ti to tam omylem dostalo. Neměla jsi pak problém s kompilací?
Vznikla tam kruhová závislost.

**ToDoList.Domain.csproj** je závislý na **ToDoList.WebApi.csproj** který je závislý na **ToDoList.Domain.csproj** => build skonci failem

Pokud máš nějaké otázky, případně bys chtěla vysvětlit ty závislosti a proč mě teď potrápily, neváhej napsat na Discoru :wink:
