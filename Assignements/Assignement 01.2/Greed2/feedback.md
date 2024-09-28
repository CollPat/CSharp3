# Feedback

Zadáni (mimo bonus) je splněno, dělá to to co má - nikde nebylo specifikováno jak generovat čísla kostek, třída Random je spíše vylepšovák pro testování abychom to nemuseli měnit manuálně.

V práci ti přišel ticket na vytvoření metody CalculateScore, ticket jsi splnila a funguje to jak má, všichni jsou spokojení.
Jenže ouha! Přišel nový požadavek (bonus) a má ho implementovat jiný vývojář (ty už děláš na dalším zajímavém projektu).

Az tady se ukážou některé nedostatky tvého zpracování
1. čitelnost - bez přečtení zadání, jak má celá hra fungovat, je obtížnější se v kódu zorientovat co vlastně dělá (to jde vyřešit pomocí komentářů v kódu. Nebo ještě lépe díky skvělému "Samodokumentačnímu" kódu, který přesně říká, co dělá)
2. udržitelnost, škálovatelnost - pokud bych to rozšiřoval a modifikoval stejným způsobem, jakým je to teď napsané, časem by se z toho mohla stát metoda co má stovky a stovky řádků a to přináší obtíže při řešení bugů, změn atd..

Teď maličko přeháním :), ale v případě složitějšího algoritmu se mi to klidně může stát a tady na takovém drobném zadání si můžeme krásně vyhrát a vyzkoušet různé možnosti jak to vylepšit.

(Pokud teď potřebuješ motivaci, přejdi na **Závěr**)
Jak bych na to šel (pokud je chyba v kódech, tak se omlouvám, psal jsem to v mardownu)

## Samostatné metody

```
public void ScoreBasedOnSingleDigit(Dictionary<int,int> dices, int digit, int digitValue) //jméno by ideálně mělo popisovat ve zkratce to co metoda dělá
{
    if(!dices.ContainsKey[digit])
    {
        return 0;
    }
    dices.Remove(digit) //slovnik je referencní datový typ :) pokud nevíš co to je, zkus napsat do nějakého chatbotu co je v "C# reference and value data type", je to velmi uzitecne :)
    return dices[digit]*digitValue;
}
```
Takto je to velmi přehledné co metoda dělá a můžu jednoduše modifikovat kód.
Nové pravidlo že kostka 6 má hodnotu 25 => nemusím složitě psát nový kód, můžu použít znova tuto metodu

## Samostatné třídy
Tady se moc rozepisovat nebudu, šlo by to i takto.

```
public class SingleDigitRule
{
    private int digitValue;
    private int digit;

    public SingleDigitRule(int digitValue,int digit)
    {
        this.digitValue = digitValue;
        this.digit = digit;
    }

    public int Score(Dictionary<int,int> dices)
    {
        ...
    }
}
```
Mělo by to tu výhodu že v případě že se hra rozšiřuje více a více, samostatná pravidla jsou v samostatné třídě v samostatném .cs souboru =>
nemám jeden soubor o tisíci řádcích, ale mám vícero malých tříd, jde se pak v tom mnohem lépe vyznat.

## Závěr
Dobrá práce, jsem rád že sis našla čas na bonus. Nenechej se mým hodnocením vyděsit, každý tento vhled získá až zkušenostmi (respektive až takto vytrestá sám sebe nebo má opravovat cizí legacy kód :D)
