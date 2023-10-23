using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Panel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    
    public void PanelActivityStatus(bool status)=>panel.SetActive(status);

    public virtual void LoadScene() => SceneManager.LoadScene(0);
}
