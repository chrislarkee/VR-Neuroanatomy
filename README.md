# VR Neuroanatomy
Understanding the anatomy of complex organs such as the human brain can be a challenge for students, so there is a definite advantage given to students who can experience the structures in a spatial way through real or virtual experiences. Since 2016, the Marquette Visualization Lab has been utilizing a dataset of 3 and 7 Tesla MRI scans from the Structural Informatics Group at University of Washington to visualize the structures of the brain in their Cave, utilizing the Unity game engine and 10 stereoscopic 3D projectors. Most MRI scans are presented as a volumetric rendering, but this dataset is unique because it was fully labelled and parcellated, allowing lab users to not only view the structures of the brain, but also virtually dissect it in a large scale, collaborative virtual reality environment. 

This year, in the interest of making the learning experience more mobile and accessible, the visualization was ported to the Oculus Quest. In first impressions, the Quest may seem unsuitable to visualizing high-complexity medical data due to its limitations in processing powered compared to a multi-node Cave, but through careful optimization and rendering techniques, the data set was able to be rendered on the mobile device while maintaining its high degree of interactivity. Specifically, although the polygon count is far over the recommended limit for the device, the target frame rate was still achievable by using low-poly mesh colliders, a minimal amount of draw calls, and very low shader complexity. This demonstration will consist of the Oculus Quest port of the interactive brain atlas, as well as a laptop screen mirroring the user’s headset perspective. 

Citations: 

Sundsten, J. W. (1994). Digital anatomist: Interactive brain atlas. 
Nolte, J., & Sundsten, J. W. (2001). The human brain : an introduction to its functional anatomy. Mosby. 

## Development Requirements:
* [Unity](https://unity.com/) version [2019.4.0](https://unity3d.com/unity/qa/lts-releases?version=2019.4)
* [Blender](https://www.blender.org/) version [2.82](https://download.blender.org/release/Blender2.82/)

## Additional Unity Packages
* A license to [Dotween Pro](https://assetstore.unity.com/packages/tools/visual-scripting/dotween-pro-32416) is required to run this without errors. Perhaps Dotween free will work, but I haven't tested it.
* This version is built around the [Oculus Integration](https://assetstore.unity.com/packages/tools/integration/oculus-integration-82022) package, and is designed primarily for the Quest. It also runs very well on the Rift and Rift S, and runs partially on the Gear VR & Oculus Go.

