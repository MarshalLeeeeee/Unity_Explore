# Unity_Explore
A repo recording my learning path in unity.
The exploration is led by scene.

## Scenes
Scenes are categoried by the big part in Unity. Every scene includes the features of the specific part. 

### MeshBasicScene
 - Change the skybox (mat) via ```Window / Rendering / Lighting / Environment```.
 - Create simple 3d mesh via ```+ / 3D Objects```. 
 - Adjust mesh position, rotation, scale in Inspector View or directly in the scene view. Via ```holding Ctrl```, we can move the object by grid.
 - Load material via dragging. If the surface type is ```Transparent```, we can set the alpha channel to have transparent effect of mesh.
 - Duplicate the mesh via ```Ctrl + D```.
 - Inspect the mesh in view via ```Shift + F```. When inspecting an object, press ``` Alt(left) + Left click``` to inspect aroung the object.
 - Align the mesh via ```Shift + V``` between two features, e.g. corner, of the target meshes.
 - Group mesh together via selecting and ```+ / Group Empty Parent```.
 - Define mesh as Prefab via ```dragging into Prefabs/``` so as to be able to change parent mesh by change prefab child mesh.
 - Add physics to the mesh via ```Add Component / Rigidbody```. ```Use Gravity``` determine if the rigidbody is affected by the gravity force toward minus Y. ```Drag``` act like a damping parameter, higher the value, higher the damping. ```Is Kinematic``` determine if the update() of the mesh is fully dependent on the script or animation.
 - For parent GameObject with children components, use the collider of the child mesh with the rigidbody of the parent. But if we would not like the newly instantiated child mesh to affect the center of mass of the parent mesh, we can add rigidbody of the children which overrides that of parent so that will not affect the mass which is defined in rigidbody component of the parent.
 - Add script to the object via ```Add Component / New Script```.


### MaterialScene
 - Create a new material via ```right clicking parent folder in project view / Create / Material```.
 - Load texture image via ```dragging the target image to Base Map```. The albedo color still affects. 


### LightingScene
 - Create ```Direct Light``` which creates the global light in the scene. The position of direct light will not affect the lighting effect while the angle define the global direction of light.
 - Create ```Spot Light``` which creates a cone light in the scene. ```Inner / Outer angle``` defines the angle attenuation, which is ```saturate((d-cos(r_o))/(cos(r_i)-cos(r_o)), min=0, max=1)```.


### AudioScene
 - The sound of the scene is listened by ```Audio Listener```. There could be one and only one ```Audio Listener``` in the scene. By default, it is within the ```Main Camera```.
 - Attach a sound affect to a game object via ```Add Component / Audio Source```. Select the sound file in ```AudioClip```. Mark ```Loop``` for continuous play'''. 2D / 3D effect is controlled by ```Spatial Blend```. In ```3D Sound Settings```, we can activate ```Doppler Level``` and customize the ```Volume Rolloff```, which reflects the relationship between distance and ratio of volume.


### InteractionScene
 - The camera follows the player in a third person manner by a script which update the position of the camera according to the position of a GameObject reference. 
 - Object is controlled by the player through ```Input```. Detailed introduction of ```Input``` can refer to __Section:Script & API__ / Input.
 - Define behaviour that will be periodically happened as prefab. The prefab can store variables of its own. 
 - We can set the life time ```t``` of an object ```obj``` by implementing ```Destroy(obj, t)``` in ```Start()```.
 - Instantiate new object relative to another object via ```Instantiate(Object obj, Transform parent)```, in which case the bahaviour of prefab is also relative to its parent.
 - Implement the automatic shooting by ```InvokeRepeating()``` that invokes the function to instantiate the bullet repeatedly.


### 2DScene (not included in this repo)
 - In a 2D template project, the camera is orthographic by default. The canvas is parallel to XY-Plane where the the width is measured in ```X``` and height is measured in ```Y```. Since the camera is orthographic, ```Z``` does not affect the size of a game object, however, affected the visibility among multiple objects. What's more, only when the ```Z coordinate``` of an object is within the camera clipping planes will it be seen by camera.
 - We can alter the scene view between 2D and 3D via ```2D Button``` in the tool bar of scene view so as to visualize the depth of objects.
 - We can alter the scene view between perspective and isometric via the icon at right top beneath the coordinate icon, which cooresponds to the perspective camera and orthographic camera.
 - The basic 2D object is called Sprite. We can define the specific sprite shape in ```Sprite Renderer / Sprite```. We can add ```Rigidbody 2D``` alike what we do to 3D meshes. One thing different from 3D mesh is that, the collider is added to 3D mesh by default, but it's not the case in 2D case. Thus, we have to add collider with the specific shape type to the sprite with which we can use ```OnCollideXXX()```. The ```Polygon Collider``` provides the tight(est) bound.



## Script & API
 - The Script name has to be identical with the class name. (Automatically done via ```Add Component / New Script```.) 
 - MonoBehaviour: The base class from which every Unity script derives. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html)
   - Start(), Update(), ... are ```Event Functions``` in Unity. Running a Unity script executes a number of event functions in a predetermined order, for details click [here](https://docs.unity3d.com/Manual/ExecutionOrder.html). The ```Physics```, which contains ```OnTriggerXXX(), OnCollideXXX(), ...``` is seperated from ```Game Logic```, which contains ```Update(), LateUpdate(), ...```. Therefore, an GameObject with ```RigidBody``` which gives physics can also be affected by ```Update()```. 
   - Invoke a function ```f``` with time delay ```t``` via ```Invoke(f, t)```. If ```t == 0```, just call the function directly. Other invoke related methods are ```InvokeRepeating(f,t,interval)``` which invoking function repeatedly, ```IsInvoking(f)``` which checks if a function is being invoked and ```CancelInvoke()``` which cancel all invokes in this. *(However, for better performance and maintability, use Coroutines instead.)
 - Object: Base class for all objects Unity can reference. Therefore, for static methods, we do not have to explicit write prefix ```Object.```. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Object.html).
   - Every object in unity can transfer to string via ```ToString()```.
   - We can destroy an object ```obj``` with time delay ```t``` via ```Destroy(Object obj, float t = 0.0f)```. 
   - We can instantiate an object ```original``` via ```Instantiate(Object original)```. We can specify the parent of ```original``` via ```Transform parent``` parameter which is suppoted by some overloads, otherwise ```original``` will not have parent. We can specify ```Vector3 position``` and ```Quaternion rotation``` in world coordinate, otherwise ```original``` is instantiated as the default position and rotation of the mesh or prefab, either in world coordinate or parent coordinate.
 - Transform: Position, rotation and scale of an object. Transform follows the hierarchy of the scene. Every Transform can have at most one parent and several children. Transform is relative to the parent transform, which can be visualized by ```Pivot Local Coordinate```. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Transform.html).
   - Get the parent by ```transform.parent```. Get children by ```transform``` which is iterable, or by ```GetChild(index)```.
   - To relationship between local and world coordinate is ```transform.parent.localToWorldMatrix.MultiplyPoint3x4(transform.localPosition)) == transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3(0.0f, 0.0f, 0.0f)) == transform.position``` and ```transform.parent.worldToLocalMatrix.MultiplyPoint3x4(transform.position)) == Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale).MultiplyPoint3x4(new Vector3(0.0f, 0.0f, 0.0f))) == transform.localPosition```. The reason why we use ```transform.parent.localToWorldMatrix``` is becasue ```localPosition, localRotation (localEulerAngles), localScale``` is all relative to the parent transform. Via ```Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale)```, we get the relative tranform between child and parent.
   - ```position, rotation (eulerAngles), scale``` store the information in the world coordinate.
   - Update either ```eulerAngle (Vector3)``` or ```rotation (Quaternion)``` will make the object rotate and update the other one automatically.
   - Transform also stores the tag of the gameObject.
   - Retrieve the local X, Y, Z direction in world coordinate, a unit vector, by ```right, up, forward```. Since it is in the local coordinate, it take the rotation of the object in the world coordinate as well. If we would like to retrieve the global X, Y, Z direction, using ```Vector3.[Static Properties]```.
   - For ```Translate(), Rotate()```, ```Space.Self``` is used as default coordinate. We can specify as, for instance, Space.World according to the signature of the methods. E.g. ```transform.Translate(Vector3.forward, Space.Self) == transform.Translate(transform.forward, Space.World)```.
 - Mathf: A collection of common math functions. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Mathf.html).
 - Input: Interface into the Input system. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Input.html).
   - Get the value of the virtual axis via ```Input.GetAxis()``` in continuous field in [-1,1] or ```Input.GetAxisRaw()``` in discrete field in {-1 ,0 ,1}. To set up input or view the options for ```axisName``` via ```Edit / Project Settings / Input Manager```. The options include the trigger event for positive and negative reactions.
   - Get specific key interaction, including keyboard, mouse and joystick, via ```GetKeyDown(...)``` which detects if the user starts pressing one key down, ```GetKey(...)``` which detects if the user keeps holding one key down, ```GetKeyUp(...)``` which detects if the user starts releasing one key up. This brings more flexifibility than the previous introduced virtual axis.
   - Gey specific mouse interaction via ```GetMouseButtonDown(...)``` which detects if the user starts pressing one mouse button down, ```GetMouseButton(...)``` which detects if the user keeps holding one mouse button down, ```GetMouseButtonUp(...)``` which detects if the user starts releasing one mouse button up. This is equivalent to ```GetKeyXXX()``` whose parameter is ```KeyCode.MouseX```. 
 - KeyCode: Key codes that map to physical key, including keyboard, mouse and joystick. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/KeyCode.html).
 - Time: The interface to get time information. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Time.html).
   - deltaTime: The passed time of every frame.
   - frameCount: The total number of passed frames.
 - Random: Generate random data. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Random.html).
   - Generate Vecotr4-like Color via ```ColorHSV```.
   - Generate random float number via ```Range(float min, float max)```.
 - Debug: Class containing methods to ease debugging while developing. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Debug.html).
   - Print a message at the bottom of game view via ```Log()```.
