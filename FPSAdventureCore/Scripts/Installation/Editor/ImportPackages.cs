
using UnityEditor;

public class ImportPackages : ScriptableWizard
{
    [MenuItem("FPS Adventure Tools/Install Package")]
    static void ImportFPSAdventurePackage()
    {
        ScriptableWizard.DisplayWizard<ImportPackages>("Import Resources", "Import");
    }
    
    

    // Update is called once per frame
    void OnWizardCreate ()
    {
        try
        {
            AssetDatabase.ImportPackage("Packages/com.happy30.fpsadventureengine/Package Resources/Resources/FPSAdventurePackage.unitypackage",true);
        }
        finally
        {

        }
    }

    void OnWizardUpdate()
    {
        helpString = "Import the necessary assets to use FPS Adventure Engine by clicking Import";
    }
    
    //[InitializeOnLoad]
}
