# Feedback assignment 04.1

Pěkné testy, mají drobné nedostatky, ale jde vidět že ses nad testy zamyslela a celkem téma ovládáš :)
Jenom pozor na to aby testy byly nezavislé! Viz komentáře.

Proč je to důležité. Pokud není test nezávislý (i.e. jeho výsledek je ovlivněn kódem který proběhl před ním), tak si zvykneš že failuje a ignoruješ ho i když po nějaké změně začně failovat kvůli zanesené chybě. Čili se vytrácí efektivnost kontroly kódu.
Plus ve verzovacích systémech (Git) může CI pipeline kontrolovat testy a pokud nějaké neprochází, tak zablokuje merge. Což by ti nikdo nepoděkoval pokud by mu neprocházelo mergování kvůli nějaké náhodné chybě v testech :) 
