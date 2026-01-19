using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RestoreMonarchy.SellDoor.Helpers
{
    public static class BarricadeHelper
    {
        public static BarricadeDrop FindBarricadeDrop(Guid assetGuid, Vector3 point)
        {
            List<RegionCoordinate> regions = new();
            Regions.getRegionsInRadius(point, 0.1f, regions);

            List<Transform> results = new();
            BarricadeManager.getBarricadesInRadius(point, 0.1f, regions, results);

            foreach (Transform result in results)
            {

                NetId netId = NetIdRegistry.GetTransformNetId(result);
                if (netId == NetId.INVALID)
                    return null;
                netId.id--;
                BarricadeDrop drop = NetIdRegistry.Get<BarricadeDrop>(netId);


                BarricadeData barricadeData = drop.GetServersideData();

                if (barricadeData.point == point && drop.asset.GUID == assetGuid)
                {
                    return drop;
                }
            }

            return null;
        }

        public static BarricadeDrop ForceDropBarricade(ItemBarricadeAsset asset, Vector3 point, Vector3 angle, ulong owner, ulong group)
        {
            byte[] state = asset.getState(true);
            Barricade barricade = new(asset, asset.health, state);
            Quaternion rotation = Quaternion.Euler(angle.x, angle.y, angle.z);
            //Quaternion rotation = BarricadeManager.getRotation(barricade.asset, angleX, angleY, angleZ);

            Transform transform = BarricadeManager.dropNonPlantedBarricade(barricade, point, rotation, owner, group);


            NetId netId = NetIdRegistry.GetTransformNetId(transform);
            if (netId == NetId.INVALID)
                return null;
            netId.id--;
            BarricadeDrop drop = NetIdRegistry.Get<BarricadeDrop>(netId);

            return drop;
        }  
    }
}
