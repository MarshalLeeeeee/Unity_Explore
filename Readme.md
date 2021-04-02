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
 - Add script to the object via ```Add Component / New Script```.


#### Script & API
 - The Script name has to be identical with the class name. (Automatically done via ```Add Component / New Script```.) 
 - Start(), Update(), ... are ```Event Functions``` in Unity. Running a Unity script executes a number of event functions in a predetermined order, for details click [here](https://docs.unity3d.com/Manual/ExecutionOrder.html). The ```Physics```, which contains ```OnTriggerXXX(), OnCollideXXX(), ...``` is seperated from ```Game Logic```, which contains ```Update(), LateUpdate(), ...```. Therefore, an GameObject with ```RigidBody``` which gives physics can also be affected by ```Update()```.
 - Transform: Position, rotation and scale of an object. Transform follows the hierarchy of the scene. Every Transform can have at most one parent and several children. Transform is relative to the parent transform, which can be visualized by ```Pivot Local Coordinate```. For detailed API of Transform, click [here](https://docs.unity3d.com/ScriptReference/Transform.html).
   - Get the parent by ```transform.parent```. Get children by ```transform``` which is iterable, or by ```GetChild(index)```.
   - To relationship between local and world coordinate is ```transform.parent.localToWorldMatrix.MultiplyPoint3x4(transform.localPosition)) == transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3(0.0f, 0.0f, 0.0f)) == transform.position``` and ```transform.parent.worldToLocalMatrix.MultiplyPoint3x4(transform.position)) == Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale).MultiplyPoint3x4(new Vector3(0.0f, 0.0f, 0.0f))) == transform.localPosition```. The reason why we use ```transform.parent.localToWorldMatrix``` is becasue ```localPosition, localRotation, localScale``` is all relative to the parent transform. Via ```Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale)```, we get the relative tranform between child and parent.
   - Transform also stores the tag of the gameObject.
 - Mathf: A collection of common math functions. For detailed API of Mathf, click [here](https://docs.unity3d.com/ScriptReference/Mathf.html).


## MaterialScene
 - Create a new material via ```right clicking parent folder in project view / Create / Material```.
 - Load texture image via ```dragging the target image to Base Map```. The albedo color still affects. 


## LightingScene
 - Create ```Direct Light``` which creates the global light in the scene. The position of direct light will not affect the lighting effect while the angle define the global direction of light.
 - Create ```Spot Light``` which creates a cone light in the scene. ```Inner / Outer angle``` defines the angle attenuation, which is ```saturate((d-cos(r_o))/(cos(r_i)-cos(r_o)), min=0, max=1)```.


## AudioScene
 - The sound of the scene is listened by ```Audio Listener```. There could be one and only one ```Audio Listener``` in the scene. By default, it is within the ```Main Camera```.
 - Attach a sound affect to a game object via ```Add Component / Audio Source```. Select the sound file in ```AudioClip```. Mark ```Loop``` for continuous play'''. 2D / 3D effect is controlled by ```Spatial Blend```. In ```3D Sound Settings```, we can activate ```Doppler Level``` and customize the ```Volume Rolloff```, which reflects the relationship between distance and ratio of volume.