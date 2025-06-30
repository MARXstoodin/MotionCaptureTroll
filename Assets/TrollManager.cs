using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrollManager : MonoBehaviour
{
    [Header("Список объектов")]
    public GameObject[] objects;
    private int currentIndex = 0;

    [Header("UI")]
    public Button nextButton;
    public Button prevButton;
    public Text nameText;

    [Header("Анимированный объект")]
    public GameObject animatedObject;

    private void Start()
    {
        nextButton.onClick.AddListener(SwitchNext);
        prevButton.onClick.AddListener(SwitchPrevious);
        UpdateActiveObject();
    }

    void SwitchNext()
    {
        currentIndex = (currentIndex + 1) % objects.Length;
        UpdateActiveObject();
        ActivateAnimatedObject();
    }

    void SwitchPrevious()
    {
        currentIndex = (currentIndex - 1 + objects.Length) % objects.Length;
        UpdateActiveObject();
        ActivateAnimatedObject();
    }

    void UpdateActiveObject()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }

        if (nameText != null)
        {
            nameText.text = objects[currentIndex].name;
        }
    }

    void ActivateAnimatedObject()
    {
        if (animatedObject != null)
        {
            animatedObject.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(DisableAfterSeconds(5f));
        }
    }

    IEnumerator DisableAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (animatedObject != null)
        {
            animatedObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Animator anim = hit.collider.GetComponent<Animator>();
                if (anim != null)
                {
                    anim.Play("Attack");
                }
            }
        }
    }
}
