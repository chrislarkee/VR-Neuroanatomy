MatCap Shaders Pack, version 1.4
2019/03/30
(c) 2015-2019 - Jean Moreno
================================

This is a pack of MatCap-like shaders, used in popular 3d software such as ZBrush or Sculptris.
They allow to determine the whole shading model of an object with a single texture without using any lights.
As such the shaders are really fast and should work very well on mobile!

They are based on the MatCap shader by Daniel Brauer, from the Unify Community: http://wiki.unity3d.com/index.php/MatCap
Optimizations have been made to make them much faster, and to add some features such as adding a regular texture on top of the matcap one.

"Vertex" are the defaults shader, where most of the calculations are done per-vertex, so they are very fast.

"Bumped" are shaders with normap map support, with more calculations being made per-pixel, and as such are slower.
"Accurate Calculation" can help with non-uniform scaling and skinned mesh renderers in the bump shader, but it is slower.

You can create your own MatCap textures in the scene "MC_MatCap_Creator":
- setup your lighting and sphere material in the scene
- select the "CREATE MATCAP" GameObject in the Hierarchy
- press the "Save MatCap PNG" button to export your texture

========================

Check out more free and commercial Unity plugins at:
http://www.jeanmoreno.com/unity

========================

Release notes:
--------------
1.4
- added "MC_MatCap_Creator" scene to create MatCap textures in the Editor
- added 26 MatCap textures made with this editor
- fixed the shaders in Linear color space
- updated demo scene with UGUI
- Welcome Screen fix for Unity 2019.1

1.38
- support for SRPs (HDRP, LWRP), except for "Textured Lit" shader
- removed 'JMOAssets.dll', became obsolete with the Asset Store update notification system

1.37 (Unity 5)
- added fog support for all shaders
- updated Welcome Screen

1.362
- updated Welcome Screen

1.361
- added Welcome Screen

1.36 (Unity 5)
- fixed tangents normalization in non-accurate bumped shaders (should fix scaling issues)

1.351 (Unity 5)
- rewrote how the matcap is calculated, fixing scaling and rotation issues with Unity 5 (for real hopefully!)

1.35 (Unity 5)
- fixed scaling issues with Unity 5

1.34 (Unity 5)
- updated compatibility with Unity 5

1.33
- updated "JMO Assets" menu

1.32
- fixed compilation errors and warnings in d3d11

1.31
- fixed shadows casting/receiving support

1.3
- added JMO Assets menu (Window -> JMO Assets), to check for updates or get support

1.2:
- Added shader "Plain Additive Z" (avoid X-Ray like effect)

1.11:
 - Removed Asset Store license agreement, only applying to some textures

1.1:
 - Added Bumped Shaders
 - Added more MatCap textures