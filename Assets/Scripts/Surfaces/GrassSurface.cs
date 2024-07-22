using UnityEngine;

public class GrassSurface : MonoBehaviour, ISurface
{
    SurfaceType ISurface.surfaceName => SurfaceType.Grass;
}
