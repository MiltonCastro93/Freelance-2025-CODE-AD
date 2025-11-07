using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Paneles UI")]
    public GameObject menuInicio;
    public GameObject victoriaPanel;
    public GameObject derrotaPanel;

    [Header("Jugador (opcional)")]
    public GameObject jugador;

    void Start()
    {
        Debug.Log("GameManager iniciado");

        Time.timeScale = 0f; 
        if (menuInicio != null) menuInicio.SetActive(true);
        if (victoriaPanel != null) victoriaPanel.SetActive(false);
        if (derrotaPanel != null) derrotaPanel.SetActive(false);
    }

    public void IniciarJuego()
    {
        Debug.Log("¡Jugar presionado!");
        Time.timeScale = 1f;
        if (menuInicio != null) menuInicio.SetActive(false);
    }

    public void Victoria()
    {
        Time.timeScale = 0f;
        if (victoriaPanel != null) victoriaPanel.SetActive(true);
    }

    public void Derrota()
    {
        Time.timeScale = 0f;
        if (derrotaPanel != null) derrotaPanel.SetActive(true);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}


