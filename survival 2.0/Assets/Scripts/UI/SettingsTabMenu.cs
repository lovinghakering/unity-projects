using System.Collections.Generic;
using UnityEngine;

public class SettingsTabMenu : MonoBehaviour
{
    public Color activeColor;
    public Color clickColor;
    public Color hoverColor;
    public Color idleColor;

    private List<SettingsTabButton> tabs = new List<SettingsTabButton>();
    private int activeTabIndex = 0;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tabs.Add(transform.GetChild(i).GetComponent<SettingsTabButton>());
        }
    }

    public void TabClick(int _index)
    {
        //Deactivate the old one
        tabs[activeTabIndex].Deactivate();
        tabs[activeTabIndex].transform.GetChild(0).gameObject.SetActive(false);
        
        //Activate the new one
        tabs[_index].Activate();
        tabs[_index].transform.GetChild(0).gameObject.SetActive(true);
        activeTabIndex = _index;
    }
}
