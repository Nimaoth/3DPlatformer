using UnityEngine;
using System.Collections;
using UnityEditor;

public class Test123 {

	[MenuItem("OpenScene/Start Screen %0")]
    static void StartScreen()
    {
        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene("Assets/Scenes/Level1.unity");
    }
}
