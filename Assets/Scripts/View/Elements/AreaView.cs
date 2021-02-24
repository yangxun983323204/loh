using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaView : Toggle
{
    protected override void Awake()
    {
        base.Awake();
        onValueChanged.AddListener(v => {
            if (v)
            {
                var cols = colors;
                cols.normalColor = cols.selectedColor = new Color32(255,143,0,255);
                colors = cols;
            }
            else
            {
                var cols = colors;
                cols.normalColor = cols.selectedColor = Color.white;
                colors = cols;
            }
        });
    }
}
