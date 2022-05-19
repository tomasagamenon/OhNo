using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeEffect : MonoBehaviour
{
    [SerializeField] private float typeSpeed;

    private float startSpeed;

    private void Start()
    {
        startSpeed = typeSpeed;
    }
    public Coroutine Run(string textToType, TMP_Text text)
    {
        return StartCoroutine(TypeText(textToType, text));
    }

    IEnumerator TypeText(string textToType, TMP_Text text)
    {
        text.text = string.Empty;
        GetComponent<AudioSource>().Play();

        yield return new WaitForSeconds(0.1f);

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            if(Input.anyKeyDown)
            {
                if (!Input.GetKeyDown(KeyCode.Escape))
                    typeSpeed *= 5;
            }
            t += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);
            text.text = textToType.Substring(0, charIndex);
            yield return null;
        }
        typeSpeed = startSpeed;
        GetComponent<AudioSource>().Stop();
    }
}
