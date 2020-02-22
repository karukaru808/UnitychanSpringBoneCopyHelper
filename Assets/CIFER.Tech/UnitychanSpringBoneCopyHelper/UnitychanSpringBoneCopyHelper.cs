using UTJ;

namespace CIFER.Tech.UnitychanSpringBoneCopyHelper
{
    public static class UnitychanSpringBoneCopyHelper
    {
        public static void SpringBoneCopy(UnitychanSpringBoneCopyHelperData copyData)
        {
            var length = copyData.Before.Length < copyData.After.Length
                ? copyData.Before.Length
                : copyData.After.Length;

            for (var i = 0; i < length; i++)
            {
                if (copyData.Before[i] == null || copyData.After[i] == null)
                    continue;

                var pivot = copyData.After[i].pivotNode;
                var sphCollider = copyData.After[i].sphereColliders;
                var capCollider = copyData.After[i].capsuleColliders;
                var panelCollider = copyData.After[i].panelColliders;

                copyData.After[i].stiffnessForce = copyData.Before[i].stiffnessForce;
                copyData.After[i].dragForce = copyData.Before[i].dragForce;
                copyData.After[i].springForce = copyData.Before[i].springForce;
                copyData.After[i].windInfluence = copyData.Before[i].windInfluence;
                copyData.After[i].pivotNode = pivot;
                copyData.After[i].angularStiffness = copyData.Before[i].angularStiffness;
                copyData.After[i].yAngleLimits = copyData.Before[i].yAngleLimits;
                copyData.After[i].zAngleLimits = copyData.Before[i].zAngleLimits;
                copyData.After[i].radius = copyData.Before[i].radius;
                copyData.After[i].sphereColliders = sphCollider;
                copyData.After[i].capsuleColliders = capCollider;
                copyData.After[i].panelColliders = panelCollider;
            }
        }
    }

    public struct UnitychanSpringBoneCopyHelperData
    {
        public SpringBone[] Before;
        public SpringBone[] After;
    }
}