using SDG.Unturned;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RestoreMonarchy.SellDoor.Helpers
{
    public class StructureHelper
    {
        /*
        public static StructureDrop FindStructureDropByPosition(Guid assetGuid, Vector3 point)
        {
            
            List<RegionCoordinate> regions = new();
            Regions.getRegionsInRadius(point, 0.1f, regions);

            List<Transform> results = new();
            StructureManager.getStructuresInRadius(point, 0.1f, regions, results);

            foreach (Transform result in results)
            {
                NetId netId = NetIdRegistry.GetTransformNetId(result);
                if (netId == NetId.INVALID)
                    return null;
                netId.id--;
                StructureDrop drop = NetIdRegistry.Get<StructureDrop>(netId);
                StructureData structureData = drop.GetServersideData();

                if (structureData.point == point && drop.asset.GUID == assetGuid)
                {
                    return drop;
                }
            }
            

            

            return null;
        }
        */

        public static StructureDrop ForceDropStructure(ItemStructureAsset asset, Vector3 point, Vector3 angle, ulong owner, ulong group)
        {
            Structure structure = new(asset, asset.health);
            Quaternion rotation = Quaternion.Euler(angle.x, angle.y, angle.z);

            StructureManager.dropReplicatedStructure(structure, point, rotation, owner, group);

            NetId dropNetId = new NetId(NetIdRegistry.counter - 2);

            StructureDrop drop = NetIdRegistry.Get<StructureDrop>(dropNetId);

            return drop;
        }
    }
}
