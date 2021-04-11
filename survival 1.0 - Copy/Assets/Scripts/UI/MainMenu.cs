using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] buttons;
    public int selectedBtn = 0;

    public Material idleMaterial;
    public Material selectedMaterial;

    private void Start()
    {
        buttons[0].GetComponent<MeshRenderer>().material = selectedMaterial;

        BindControls();
    }

    private void BindControls()
    {
        if (buttons[0] != null)
            buttons[0].GetComponent<GOButton>().onClick.AddListener(Play);
    }

    private void Play()
    {
        Debug.Log("Play");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectButton(-1);
            UdpateMenu();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            SelectButton(1);
            UdpateMenu();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ClickButton();
        }
    }

    private void ClickButton()
    {
        buttons[selectedBtn].GetComponent<GOButton>().Click();
    }

    private void SelectButton(int a)
    {
        int b = selectedBtn + a;
        selectedBtn = Mathf.Clamp(b, 0, buttons.Length - 1);
    }

    private void UdpateMenu()
    {
        Debug.Log("Updating menu");
        /*for (int i = 0; i < buttons.Length; i++)
        {
            Debug.Log(i);
            if (i == selectedBtn)
            {
                buttons[selectedBtn].GetComponent<MeshRenderer>().material = idleMaterial;
            }
            else
            {
                buttons[selectedBtn].GetComponent<MeshRenderer>().material = selectedMaterial;
            }
        }*/

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == selectedBtn)
            {
                buttons[i].GetComponent<MeshRenderer>().material = selectedMaterial;
                //Debug.Log(buttons[i].name);
            }
            else
            {
                buttons[i].GetComponent<MeshRenderer>().material = idleMaterial;
            }
        }

    }
}
