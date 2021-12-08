using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicScript : MonoBehaviour
{
    public Sprite onImage;
    public Sprite offImage;
    public Button button;
    public AudioSource music;
    public void toggleMusic()
    {
        if (!music.mute)
        {
            button.image.sprite = offImage;
            music.mute = true;
        }
        else
        {
            button.image.sprite = onImage;
            music.mute = false;
        }
    }
}
