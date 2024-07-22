using UnityEngine;

public class MetalSurface : MonoBehaviour, ISurface
{
    SurfaceType ISurface.surfaceName => SurfaceType.Metal;
}
