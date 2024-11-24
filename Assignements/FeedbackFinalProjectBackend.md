# Feedback Final Project Backend

V rámci backendu nemám co vytknout, poradila sis tím velmi dobře :wink:

Jak bys svůj kód mohla ještě vylepšit třeba rozšíření testů aby byli parametrizované

```
[Theory]
[InlineData(1, 2)]
[InlineData(-4, -6)]
[InlineData(2, 4)]
public void Test(int parametr1, int parametr2)
{
    
}
```

Kdy bychom mohli otestovat co se děje když v ToDoItem je v Category nějaký obsah (to jak ty testy máš teď, ve stringu Category něco je) a co se stane když je tam null.

Ale jinak velký palec nahoru.
