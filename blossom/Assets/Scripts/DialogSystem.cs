using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public static string[] dialog_strings = { "Vay be, sonunda ulaştık şu Mars'a.", "Yani, o kadar uğraştık, ama sonunda Mars'a ayak basan ilk ülke biz olacağız.", "Hehehe, haklısın.", "Bu ne yaa??!! Benim gördüğümü sen de görüyor musun?", "...", "Gel şunlara hadlerini bildirelim."};
    public static Dialog[] dialogs;
    public class Dialog
    {
        public enum post_state { NEW_DIALOG, START_FIGHT };
        post_state poststate;
        public int id;
        public string dialog;
        public delegate void state_setter(int id);
        public state_setter setter;
        public Dialog(int id, post_state state)
        {
            this.id = id;
            poststate = state;
            dialog = dialog_strings[id];
            if(poststate == post_state.NEW_DIALOG)
            {
                setter = new state_setter(setDialog);
            }
            else if(poststate == post_state.START_FIGHT)
            {
                setter = new state_setter(FightSystem.startFight);
            }
        }
    }
    public static Dialog currentDialog;
    public static DialogSystem singleton;
    public static bool onDialog = true;
    static int _dialogCount = 0;
    public static int dialogCount
    {
        get
        {
            return _dialogCount;
        }
        set
        {
            _dialogCount = value;
            singleton.dialogText.text = dialog_strings[value];
        }
    }
    public Text dialogText;
    void Start()
    {
        singleton = this;
        dialogs = new Dialog[100];
        for(int i = 0; i < 5; i++)
        {
            dialogs[i] = new Dialog(i,Dialog.post_state.NEW_DIALOG);
        }
        dialogs[5] = new Dialog(5, Dialog.post_state.START_FIGHT);
        dialogCount = 0;
    }

    void Update()
    {
        if(onDialog && Input.GetMouseButtonDown(0))
        {
            dialogs[dialogCount].setter(dialogCount);
        }
    }

    public static void setDialog(int id)
    {
        dialogCount++;
    }
}
