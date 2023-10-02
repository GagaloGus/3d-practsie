using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Litten pkmn1 = new Litten();
        Bulbasur pkmn2 = new Bulbasur();

        List<Pokemon> pokemonList = new List<Pokemon>
        {
            pkmn1,
            pkmn2
        };

        foreach(Pokemon pok in pokemonList)
        {
            pok.Attack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
