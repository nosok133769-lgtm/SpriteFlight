using UnityEngine;
using YG;

public class LangManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static string language = "ru";

    private void Start()
    {
        language = YG2.envir.language;

        Debug.Log("Язык Яндекс Игр: " + language);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
