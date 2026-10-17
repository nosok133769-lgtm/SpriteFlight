using UnityEngine;
using YG;

public class MusicManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    public static MusicManager instance;
    public AudioSource music;

    private static int DeathCount =0;
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetVolume(float volume)
    {
        music.volume = volume;
    }

    public static void DeathCounter()
    {
        if (DeathCount == 10)
        {
            DeathCount = 0;
            YG2.InterstitialAdvShow();
        }
        else
        {
            DeathCount++;
        }

    }
}
    


