# Unity_Explore
A repo recording my learning path in unity.
The exploration is led by scene.

## MeshBasicScene
 - Change the skybox (mat) via ```Window / Rendering / Lighting / Environment```.
 - Create simple 3d mesh via ```+ / 3D Objects```. 
 - Adjust mesh position, rotation, scale in Inspector View. 
 - Load material via dragging. If the surface type is ```Transparent```, we can set the alpha channel to have transparent effect of mesh.
 - Duplicate the mesh via ```Ctrl + D```.
 - Inspect the mesh in view via ```Shift + F```.
 - Align the mesh via ```Shift + V``` between two features, e.g. corner, of the target meshes.
 - Group mesh together via selecting and ```+ / Group Empty Parent```.
 - Define mesh as Prefab via ```dragging into Prefabs/``` so as to be able to change parent mesh by change prefab child mesh.
 - Add physics to the mesh via ```Add Component / Rigidbody```. ```Use Gravity``` determine if the rigidbody is affected by the gravity force toward minus Y. ```Drag``` act like a damping parameter, higher the value, higher the damping. ```Is Kinematic``` determine if the update() of the mesh is fully dependent on the script or animation.



## MaterialScene
 - Create a new material via ```right clicking parent folder in project view / Create / Material```.
 - Load texture image via ```dragging the target image to Base Map```. The albedo color still affects. 