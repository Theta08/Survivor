using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Select_Item : UI_Popup
{
    enum AbilityItems
    {
        UI_Abilibty_item_1,
        UI_Abilibty_item_2,
        UI_Abilibty_item_3,
    }
    
    public override bool Init()
    {
        if (base.Init() == false)
            return false;
        
        Bind<UI_AbilityItem>(typeof(AbilityItems));
        // BindButton(typeof(Buttons));
        
        // 주석 풀어야함
        Managers.Game.Stop();
        
        // 랜덤선택
        // 기본 평타는x
        int[] ran = new int[3];
        int count = Managers.Data.ItemDatasDic.Count;
        
        while (true)
        {
            ran[0] = Random.Range(0, count);
            ran[1] = Random.Range(0, count);
            ran[2] = Random.Range(0, count);
            // ran[2] = 6;
            
            if(ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for (int i = 0; i < ran.Length; i++)
        {
            Item ranItem = Managers.Data.ItemDatasDic[ran[i]];

            Get<UI_AbilityItem>(i).gameObject.SetActive(true);
            
            // 무기 lvMax시 힐로 대체
            if (ranItem.maxLevel && i == ran.Length - 1)
            {
                ranItem = Managers.Data.ItemDatasDic[5];
                Get<UI_AbilityItem>(i).SetInfo(ranItem);
            }
            else if (ranItem.maxLevel && i != ran.Length - 1)
                Get<UI_AbilityItem>(i).gameObject.SetActive(false);
            else
                Get<UI_AbilityItem>(i).SetInfo(ranItem);
        }
        
        // enum 안쓰고 함
        // Get<UI_AbilityItem>((int)AbilityItems.UI_Abilibty_item_1)
        //     .SetInfo(Managers.Data.ItemDatasDic[ran[0]]);
        // Get<UI_AbilityItem>((int)AbilityItems.UI_Abilibty_item_2)
        //     .SetInfo(Managers.Data.ItemDatasDic[ran[1]]);
        // Get<UI_AbilityItem>((int)AbilityItems.UI_Abilibty_item_3)
        //     .SetInfo(Managers.Data.ItemDatasDic[ran[2]]);
        
        return true;
    }
    
}
