using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementPopUp : MonoBehaviour
{
    [SerializeField] private GameObject achPopUp;
    [SerializeField] private TextMeshProUGUI achPopUpText;
    [SerializeField] private Image achPopUpImage;
    [SerializeField] private Animator animator;

    public float cooldown = 2.5f;
    [SerializeField]private float _cooldown;
    public bool timer = false;
    private void Start()
    {
        _cooldown = cooldown;
    }

    private void Update()
    {
        if (timer)
        {
            _cooldown -= Time.deltaTime;
        }

        if(_cooldown <= 0)
        {
            animator.SetBool("IsOpen", false);
            timer = false;
            _cooldown = cooldown;
            Debug.Log("PopUp desaparece");
        }
    }
    public void TogglePopUp(string achName, Sprite achCompleteImage)
    {
        animator.SetBool("IsOpen", true);
        achPopUpText.text = achName;
        achPopUpImage.sprite = achCompleteImage;
        timer = true;
        Debug.Log("PopUp aparece");
    }
}
