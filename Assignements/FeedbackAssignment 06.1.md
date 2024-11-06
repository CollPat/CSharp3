# Feedback Assignment 6.1

Začnu asi hlavním zhodnocením - kód dělá co má (teda až na Delete ;) ) ale až po tom co jsem si zakomentoval jeden konstruktor ToDoItemController. Nepředpokládám že jsi ho tam nechala s větším úmyslel - ToDoItemContext již nevyužíváme v kontroléru a teda ten konsturktor nám k ničemu neslouží. To že jsme ho tam nechali bohužel způsobí to že při běhu naše Web API vyhodí exception při poslání jakéhokoliv HTTP requestu. Na to jsme narazili v breakout roomu minulou lekci. Pokud jsi to tam nechala z nějakého pádného důvodu, pak se omlouvám :) Pokud tomu tak je, tak by si ten konstuktor zasloužil komentář proč jsi ho tam nechala.

K metodě Delete (NotImplementedException), nevím co přesně se stalo za problém, narazila jsi na něco s čím sis nedokázala poradit nebo došel čas? Nebo ta exception má nějaký hlubší výynam kterému nerozumím? Pokud ses s tím nedokázala poprat, tak napiš a společně to pokoříme.

Ještě více rozeberu metodu Delete(int id) - dával jsem k tomu komentář v kódu. Třída ToDoItemRepository implementuje interface IRepository. Interface můžeme chápat jako komunikační rozhraní - definuje nám co třída umí za metody, properties, atd... Tím vlastně bez ohledu na implementaci víme že třída umí metody X,Y a jaké tyto metody mají návratové hodnoty. (Třída může implemetovat vícero interfaces plus umět vlastní metody a mít vlastní properties, ale o to nám momentálně nejde).
Pokud se podíváš do našeho kontroléru, tak uvidíš že nepracujeme s ToDoItemsRepository, nýbrž s IRepository. Tudíš se nedokážeme dostat k metodě Delete(int id).

Pokud ti to stále není jasné, prosím napiš ;) pokusím se najít nějaké pěkné video nebo návod na interface
