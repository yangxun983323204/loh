using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActorCanvas : MonoBehaviour
{
    public Slider Hp;
    public Text HpText;
    public RectTransform EffectsRoot;
    public BuffListUI EffectTemplate;

    public void SetHp(int curr,int max)
    {
        Hp.value = (float)curr / max;
        HpText.text = string.Format("{0}/{1}", curr.ToString(), max.ToString());
    }

    public void UpdateBuffList(List<BuffData> effects)
    {
        var needCreate = effects.Count - (EffectsRoot.childCount - 1);
        if (needCreate < 0)
        {
            var idx = EffectsRoot.childCount-1;
            for (int i = 0; i < needCreate; i++)
            {
                Destroy(EffectsRoot.GetChild(idx - i).gameObject);
            }
        }
        else if (needCreate > 0)
        {
            var obj = Instantiate(EffectTemplate.gameObject);
            obj.SetActive(true);
            obj.transform.SetParent(EffectsRoot);
        }

        for (int i = 1; i < EffectsRoot.childCount; i++)
        {
            EffectsRoot.GetChild(i).GetComponent<BuffListUI>().SetData(effects[i + 1]);
        }
    }
}
