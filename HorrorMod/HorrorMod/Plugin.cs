using BepInEx;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;

namespace HorrorMod
{
    /// <summary>
    /// This is your mod's main class.
    /// </summary>

    /* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
    [ModdedGamemode]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    [ModdedGamemode("HORROR", "HORROR MODE", Utilla.Models.BaseGamemode.Infection)]
    public class Plugin : BaseUnityPlugin
    {
        GameObject assest;
        bool inRoom;

        void Start()
        {
            /* A lot of Gorilla Tag systems will not be set up when start is called /*
			/* Put code in OnGameInitialized to avoid null references */

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
        }

        void Update()
        {

        }
        public AssetBundle LoadAssetBundle(string path)
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(path);
            AssetBundle bundle = AssetBundle.LoadFromStream(stream);
            stream.Close();
            return bundle;
        }


        /* This attribute tells Utilla to call this method when a modded room is joined */
        [ModdedGamemodeJoin]
        public void OnJoin(string gamemode)
        {
            if (gamemode.Contains("HORROR"))
            {
                Debug.Log("Joined");
                RenderSettings.ambientSkyColor = Color.grey;

                inRoom = true;
            }
          
        }

        /* This attribute tells Utilla to call this method when a modded room is left */
        [ModdedGamemodeLeave]
        public void OnLeave(string gamemode)
        {
            /* Deactivate your mod here */
            /* This code will run regardless of if the mod is enabled*/

            inRoom = false;
        }
    }
}
