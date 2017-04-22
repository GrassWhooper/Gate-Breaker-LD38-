using UnityEngine;
using System.Collections;
public class facebookpageLink : MonoBehaviour
{
    public void OpenSite()
    {
        Application.OpenURL("http://facebook.com/grasswhooper");
        //Change the URL in quotations to whatever website you want. 
    }
}