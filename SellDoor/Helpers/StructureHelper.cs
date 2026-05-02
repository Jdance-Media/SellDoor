using SDG.Unturned;
using UnityEngine;

namespace RestoreMonarchy.SellDoor.Helpers
{
    public class StructureHelper
    {
        public static StructureDrop ForceDropStructure(ItemStructureAsset asset, Vector3 point, Vector3 angle, ulong owner, ulong group)
        {
            Structure structure = new(asset, asset.health);
            Quaternion rotation = Quaternion.Euler(angle.x, angle.y, angle.z);

            StructureManager.dropReplicatedStructure(structure, point, rotation, owner, group);

            // dropReplicatedStructure goes through ClaimBlock(2u): drop is at counter-1, transform at counter.
            NetId dropNetId = new NetId(NetIdRegistry.counter - 1);
            return NetIdRegistry.Get<StructureDrop>(dropNetId);
        }
    }
}
