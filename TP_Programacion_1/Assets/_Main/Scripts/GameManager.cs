using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player = null;
    //[SerializeField] private int points;
    private bool gameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        player.lifeController.OnDie.AddListener(OnPlayerDieListener);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        //acá cargaría la escena
        Debug.Log("El jugador murio");
        gameEnd = true;
    }

    public void Victory()
    {
        //acá cargaría la escena
        gameEnd = true;
    }

    private void OnPlayerDieListener()
    {
        //Por ahora, mori. 
        GameOver();
    }

}
