using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Litten : Pokemon
{
    public Litten() : base()
    {
        name = "Litten";
        type = Tipo.Fire;
    }

    public override void Attack()
    {
        Debug.Log($"{name} le clava un puñal tipo {type}");
    }
}

public class Bulbasur : Pokemon
{
    public Bulbasur() : base()
    {
        name = "Bulbasur";
        type = Tipo.Grass;
    }
}
