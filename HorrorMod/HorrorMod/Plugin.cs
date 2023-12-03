using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace HorrorMod
{


    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [ModdedGamemode("HORROR", "HORROR MODE", Utilla.Models.BaseGamemode.Infection)]
    public class Plugin : BaseUnityPlugin
    {
        GameObject assest;
        GameObject portal;
        bool inRoom;

        void Start()
        {

            // pos 132.4225 -15.5873 0
            // rot 0 298.6074 0
            Utilla.Events.GameInitialized += OnGameInitialized;
        }


        void OnGameInitialized(object sender, EventArgs e)
        {
            var bundle = LoadAssetBundle("HorrorMod.Items.assetsforthehorrormode");
            assest = bundle.LoadAsset<GameObject>("assetsforthehorrormode");
            assest = GameObject.Instantiate(assest);

            foreach (MeshCollider g in assest.GetComponentsInChildren<MeshCollider>())
            {




                g.gameObject.AddComponent<GorillaSurfaceOverride>().overrideIndex = 0;



            }
            //pos -68.6526 11.8042 -81.8469
            // rot 0.1144 84.743 0
            //scale -0.0422 0.9351 0.6931
            portal = assest.transform.Find("Cube (2)").gameObject;

            portal.transform.rotation = Quaternion.Euler(0.1144f, 263.7137f, 0f);
            portal.transform.localScale = new Vector3(-0.1422f, 0.9351f, 0.5931f);
            portal.transform.Find("Trigger for portal").gameObject.AddComponent<Portal>();

            assest.transform.position = new Vector3(132.4225f, -15.5873f, 0f);
            assest.transform.rotation = Quaternion.Euler(0f, 298.6074f, 0f);
            portal.transform.localPosition = new Vector3(-68.9155f, 11.86f, -81.8204f);
            portal.transform.position = new Vector3(-68.9155f, 11.86f, -81.8204f);
            assest.SetActive(false);
        }

        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }


        
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            if (gamemode.Contains("HORROR"))
            {
                Debug.Log("Joined");
                RenderSettings.ambientSkyColor = Color.black;
                assest.SetActive(true);
                inRoom = true;
            }
          
        }

       
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            RenderSettings.ambientSkyColor = Color.white;
            assest.SetActive(false);
            inRoom = false;
        }
    }
}
