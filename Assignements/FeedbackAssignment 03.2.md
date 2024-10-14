# Feedback ukolu 03.1

Začnu chválou, pěkný stručný kód ke kterému nemám co vytknout - dělá to to co má, je to minimalistické v implementaci (to je míněné jako kompliment :wink: ) a má to i případné komentáře, to je vždycky fajn.

Ale velké ale... Musel jsem hodně bojovat abych to zkompiloval. Moc jsem kvůli tomu nedával komentáře do kódu - musel jsem zavést pár změn abych to rozjel.

1. HangmanClass a HangmanMethods jdou spojit do jedné třídy, není žádný důvod je mít jako dvě oddělené. Čili buď to dát do jednoho souboru, anebo využít klíčové slovo partial a z obou tříd udělat public partial class Hangman (partial je klíčové slovo které umožňuje rozdělení třídy/interface/... do vícero souborů - to se hodí pokud by Hangman třída měla mít tisíce a tisíce řádků, je to jeden z druhů organizace kódu aby byl více čitelnější a organizovanější. Pak při kompilaci se všechny spojí do jedné velké třídy)
2. kompilační errory
   1. to že jsou třídy rozdělené přineslo nějaké errory
   2. chybějící reference (System.Text v HangmanMethods)
   3. špatný konstruktor v HangmanClass (konstuktor třídy HangmanClass musí být void HangmanClass, pokud máš pouze void Hangman, tak to nepozná jako konstruktor, kompilér si myslí že je to metoda a pak se velmi zlobí)
   
   To mě vede k otázce jestli ti to jde zbuildit u tebe? Protože kompilér tohle nedokáže přechroustat. Nevím co by mohlo způsobit tady ten rozdíl...
   Případně jestli náhodou neuklízíš kód po implementaci kterou pak nezkotroluješ... Můžeš mi kdyžtak napsat do osobní zprávy na Discord jestli se ti tvoje řešení daří buildit tak jak ho teď máš? Zajímal by mě důvod tohoto problému.
3. Umístění HangmanClass a HangmanMethods tříd - asi bych to určitě nedával do složky Properties (tam jsou pro Web API projekt konfigurační soubory)
4. Na závěr slon ve skříni. Máš to založené jako web api projekt ale podle chování by bylo záhodno to mít jako konzolovou aplikaci

**Bonus**
Vidím že i píšeš komentáře, dám jenom radu. Pokud někde píšeš komentář, mělo by to mít své opodstatnění

například v kódu
 // Display the current masked word and incorrect guesses
Console.WriteLine(game.DisplayWord());
Console.WriteLine(game.DisplayIncorrectGuesses());

je komentář zbytečný, díky dobře pojmenovaným metodám jde hned poznat z kódu co se děje

Tady klidně piš komentáře jak potřebuješ :) je to kód i pro tebe, tohle ber čistě jako radu do praxe. Pokud si člověk zvykne psát všude komentáře, tak odvykne srozumitelně pojmenovávat metody, třídy, proměnné.
