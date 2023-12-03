using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using GorillaLocomotion;
using UnityEngine;

namespace HorrorMod
{
    public class Portal : GorillaTriggerBox
    {
        // Code from devs minecraft mod: https://github.com/developer9998/DevMinecraftMod/blob/main/DevMinecraftMod/Scripts/MinecraftQuitBox.cs & DecalFree!
        public void Start()
        {
            this.gameObject.layer = 15;
        }
        public override void OnBoxTriggered()
        {
            Vector3 target = new Vector3(111.5544f, - 33.9543f, - 25.0447f);

            Traverse.Create(Player.Instance).Field("lastPosition").SetValue(target);
            Traverse.Create(Player.Instance).Field("lastLeftHandPosition").SetValue(target);
            Traverse.Create(Player.Instance).Field("lastRightHandPosition").SetValue(target);
            Traverse.Create(Player.Instance).Field("lastHeadPosition").SetValue(target);

            Player.Instance.leftControllerTransform.position = target;
            Player.Instance.rightControllerTransform.position = target;
            Player.Instance.bodyCollider.attachedRigidbody.transform.position = target;

            Player.Instance.GetComponent<Rigidbody>().position = target;
            Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
