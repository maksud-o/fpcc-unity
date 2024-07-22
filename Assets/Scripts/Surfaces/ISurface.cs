using UnityEngine;

public interface ISurface
{
    public SurfaceType SurfaceName { get => surfaceName; }
    protected SurfaceType surfaceName { get; }
}
