# Unity_Explore
A repo recording my learning path in unity.
The exploration is led by scene.

## Scenes
Scenes are categoried by the big part in Unity. Every scene includes the features of the specific part. 

### MeshBasicScene
 - Change the skybox (mat) via ```Window / Rendering / Lighting / Environment```. We can also set the fog effect there.
 - Create simple 3d mesh via ```+ / 3D Objects```. 
 - Adjust mesh position, rotation, scale in Inspector View or directly in the scene view. Via ```holding Ctrl```, we can move the object by grid.
 - Load material via dragging. If the surface type is ```Transparent```, we can set the alpha channel to have transparent effect of mesh.
 - Duplicate the mesh via ```Ctrl + D```.
 - Inspect the mesh in view via ```Shift + F```. When inspecting an object, press ``` Alt(left) + Left click``` to inspect aroung the object.
 - Align the mesh via ```Shift + V``` between two features, e.g. corner, of the target meshes.
 - Group mesh together via selecting and ```+ / Group Empty Parent```.
 - Define mesh as Prefab via ```dragging into Prefabs/``` so as to be able to change parent mesh by change prefab child mesh.
 - Add physics to the mesh via ```Add Component / Rigidbody```. ```Use Gravity``` determine if the rigidbody is affected by the gravity force toward minus Y. ```Drag``` act like a damping parameter, higher the value, higher the damping. ```Rigidbody``` decides if the object is simulated by the unity physical engine and react to collision and forces.  ```Is Kinematic``` determine if the update() of the mesh is fully dependent on the script or animation. An object is ```Static Collider``` if the Gameobject has a collider but no rigidbody, which means it cannot be affected by any force (AddForce() is a method of Rigidbody). An object is ```Rigidbody Collider``` if the Gameobject has a collider and rigidbody with ```Is Kinematic``` set as False, which means it is simulated by the defaule physicis engine. An object is ```Kinematic Rigidbody Collider``` if the Gameobject has a collider and rigidbody with ```Is Kinematic``` set as True, which means it is not simulated by the defaule physicis engine with force and collision reaction, however since it still has a rigidbody component, we can addforce explicitly.
 - If ```Is Trigger``` in the collider is set as True, the gameobject will not collide and has collision reaction with other gameobejcts when the collider penetrates. The original collider region becomes a trigger, so that it is allowed to penetrates. From the code perspective, it reflects in the difference between ```OnCollisionXXX()``` and ```OnTriggerXXX()```.
 - For parent GameObject with children components, use the collider of the child mesh with the rigidbody of the parent. But if we would not like the newly instantiated child mesh to affect the center of mass of the parent mesh, we can add rigidbody of the children which overrides that of parent so that will not affect the mass which is defined in rigidbody component of the parent.
 - Add script to the object via ```Add Component / New Script```.
 - One gameObject can have more than one collider. Currently, trial with one collider and one trigger does work.


### MaterialScene
 - Create a new material via ```right clicking parent folder in project view / Create / Material```.
 - Load texture image via ```dragging the target image to Base Map```. The albedo color still affects. 
 - ```Physic Material``` can be added to GameObject on its collider material, which is used to adjust friction and bouncing effects of colliding objects. For more details, click[here](https://docs.unity3d.com/Manual/class-PhysicMaterial.html). ```Friction``` reflects to coefficient of friction, ```[0,1]```, which is further classified as ```static friction``` and ```dynamic friction```. ```Bounciness``` reflects to bouncy, ```[0,1]```. Both parameter has mode of ```Combine```, which defines how the parameter of the two colliders are combined. The combination priority is ```Average < Minimum < Multiply < Maximum```. The default setup for ```Physic Material = None``` is the default initialized setup.
 - We can get the material of the gameObject by ```GetComponent<Renderer>().material```.


### LightingScene
 - Create ```Direct Light``` which creates the global light in the scene. The position of direct light will not affect the lighting effect while the angle define the global direction of light.
 - Create ```Spot Light``` which creates a cone light in the scene. ```Inner / Outer angle``` defines the angle attenuation, which is ```saturate((d-cos(r_o))/(cos(r_i)-cos(r_o)), min=0, max=1)```.


### AudioScene
 - The sound of the scene is listened by ```Audio Listener```. There could be one and only one ```Audio Listener``` in the scene. By default, it is within the ```Main Camera```.
 - Attach a sound affect to a game object via ```Add Component / Audio Source```. Select the sound file in ```AudioClip```. Mark ```Loop``` for continuous play'''. 2D / 3D effect is controlled by ```Spatial Blend```. In ```3D Sound Settings```, we can activate ```Doppler Level``` and customize the ```Volume Rolloff```, which reflects the relationship between distance and ratio of volume.
 - ```AudioClip``` is the variable type of sound file.


### InteractionScene
 - The camera follows the player in a third person manner by a script which update the position of the camera according to the position of a GameObject reference. The angle of camera is controlled by user's mouse.
 - Object is controlled by the player through ```Input```. Detailed introduction of ```Input``` can refer to __Section:Script & API__ / Input.
 - Define behaviour that will be periodically happened as prefab. The prefab can store variables of its own. 
 - We can set the life time ```t``` of an object ```obj``` by implementing ```Destroy(obj, t)``` in ```Start()```.
 - Instantiate new object relative to another object via ```Instantiate(Object obj, Transform parent)```, in which case the bahaviour of prefab is also relative to its parent.
 - Implement the automatic shooting by explicit time counting, ```InvokeRepeating()```, ```StartCoroutines()``` that invokes the function to instantiate the bullet repeatedly.
 - Implement jump by applying force to the ```Rigidbody```. We should get user input in ```Update()``` and apply physics in ```FixedUpdate()```.


### AnimationScene
 - Add ```Animator``` to GameObejct which point to an ```animation controller``` which defines the configuration and transition of meta animations and ```avatar``` which defines the structure, e.g. the bone structure, of the character.
 - We can define animation state and their transitions in Animator Editor. One state is set as default state which serves as the first state when the animation starts.
 - We can blend animation with ```Blend Tree``` which interpolates two or more animations via defined parameter. The specific blending method is defined by ```Blend Type```. In this scene, we blend idle, walk in 4 directions and run forward using ```2D Freeform Directional```. ```2D Freeform Directional``` allows multiple animations in the same direction, e.g. in our case walk forward and run forward, while ```2D Simple Directional``` does not allow such case. ```2D Freeform Cartesian``` is best used when the parameter does not represent the direction. For more details, click [here](https://docs.unity3d.com/Manual/BlendTree-2DBlending.html).
 - The transition has one attribute - ```Has Exit Time``` which determines if the previous animation plays to an end when the transition condition is satisfied.
 - The controller script can communicate with Animator by first ```GetComponent<Animator>()``` and use methods like ```SetFloat()``` to change the parameters in animator controller.
 - We can modify the structure and kinematics of the animator components by ```OnAnimatorIK(int layerIndex)```. It should be noted that the ```IK Pass``` option of the layer in animator controller should be chosen.


### UIScene
 - The UI panel is realized by ```Canvas```.
 - We can add text by ```+ / UI / Text - TextMeshPro``` and for other UI compenents similiarly. ```TextMeshPro``` is an improvement of default text using a different rendering technique ```SDF```, which is based on the principle of rendering a Font Atlas at a high resolution. Resize the text will not lead to blur. Still, we can control the visual effect of text without recreating new font. We can combine ```Rich Text``` with ```TextMeshPro```. Even material, named ```Material Preset``` can be added to text to give it a special effect. For more details of the difference between ```TextMeshPro``` and default text, click [here](https://blog.unity.com/technology/making-the-most-of-textmesh-pro-in-unity-2018)
 - ```Button - TextMeshPro``` is a Button that uses ```Text - TextMeshPro```, which is equivalent to create default button and replace text child with ```Text - TextMeshPro```.
 - To use ```TextMeshPro``` in C#, add ```using TMPro```.
 - We can anchor the text so that the position is relatively still with regard to the anchor position regardless of the size of the game window.
 - We can make the text unaffected by the text box by choosing ```Wrapping``` as disable.
 - We can control the visibility of the text and other UI components by controlling their alive status.
 - Button interaction can be defined through editor by ```Button / OnClick()```, which receives a accessable (usually public) function as the behavior after being clicked. Additionally, we can define the click behavior through ```button.onClick.AddListener()``` which can bring us less limitation and really decide what function to be called. But remember to remove the listener afterwards.
 - Scene is managed by ```SceneManager```, which stores the information as well as behavior of the scene.


### 2DScene (not included in this repo)
 - In a 2D template project, the camera is orthographic by default. The canvas is parallel to XY-Plane where the the width is measured in ```X``` and height is measured in ```Y```. Since the camera is orthographic, ```Z``` does not affect the size of a game object, however, affected the visibility among multiple objects. What's more, only when the ```Z coordinate``` of an object is within the camera clipping planes will it be seen by camera.
 - We can alter the scene view between 2D and 3D via ```2D Button``` in the tool bar of scene view so as to visualize the depth of objects.
 - We can alter the scene view between perspective and isometric via the icon at right top beneath the coordinate icon, which cooresponds to the perspective camera and orthographic camera.
 - The basic 2D object is called Sprite. We can define the specific sprite shape in ```Sprite Renderer / Sprite```. We can add ```Rigidbody 2D``` alike what we do to 3D meshes. One thing different from 3D mesh is that, the collider is added to 3D mesh by default, but it's not the case in 2D case. Thus, we have to add collider with the specific shape type to the sprite with which we can use ```OnCollideXXX()```. The ```Polygon Collider``` provides the tight(est) bound.



## Script & API
 - The Script name has to be identical with the class name. (Automatically done via ```Add Component / New Script```.) Therefore, script name, the same as the class name, is also a valid type in C# script, e.g., we can use ```GetComponent<ScriptName>()``` to get the reference to the class object to get the public fields and use the public methods.

 - MonoBehaviour: The base class from which every Unity script derives. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/MonoBehaviour.html)
   - Start(), Update(), ... are ```Event Functions``` in Unity. Running a Unity script executes a number of event functions in a predetermined order, for details click [here](https://docs.unity3d.com/Manual/ExecutionOrder.html). The ```Physics```, which contains ```OnTriggerXXX(), OnCollideXXX(), ...``` is seperated from ```Game Logic```, which contains ```Update(), LateUpdate(), ...```. Therefore, an GameObject with ```RigidBody``` which gives physics can also be affected by ```Update()```. 
   - Invoke a function ```f``` with time delay ```t``` via ```Invoke(f, t)```. If ```t == 0```, just call the function directly. Other invoke related methods are ```InvokeRepeating(f,t,interval)``` which invoking function repeatedly, ```IsInvoking(f)``` which checks if a function is being invoked and ```CancelInvoke()``` which cancel all invokes in this. *(However, for better performance and maintability, use Coroutines instead.)
   - Coroutine combines the function of ```Invoke()``` and ```InvokeRepeating()``` with more flexibility. For instance, ```InvokeRepeating()``` can only repeat the invoke with fixed time interval but Coroutine is able to dynamicaaly change the time interval. ```StartCoroutine(IEnumerator routine)``` directly receives the function call ```routine(...)``` which returns an IEnumerator object in the format ```yield return xxx;```. Specifically, we can yield return ```WaitForSeconds(float timeInterval)``` to allow the coroutine to resume on the first frame after ```timeInterval``` seconds has passed.
   - ```OnCollisionXXX(Collision collision)``` is invoked whenever there is a penetration between collider and collider. ```OnCollisionEnter(Collision collision)``` is invoked whenever there is a penetration between collider and trigger / trigger and trigger. For more details, click [here](https://docs.unity3d.com/Manual/CollidersOverview.html)
   - ```OnAnimatorIK(int layerIndex)``` allows to update the kinematics of animation components, like bones, in a desired manner. It should be noted that the ```IK Pass``` option of the layer in animator controller should be chosen.
   - ```LateUpdate()``` is the last invoked event function in the Game Logic part, which is invoked after the internal animation update. Thus we can use it to update the joint in animation as we desired.
   - ```OnEnable()``` is called when the object becomes enabled and active. ```OnDisable()``` is called when the behaviour becomes disabled.
   - We detect the interaction between our mouse and collider with ```OnMouseDown()``` when we press the mouse while colliding, ```OnMouseUp()``` when we release the mouse while colliding, ```OnMouseDrag()``` when we keep pressing the mouse while colliding, ```OnMouseEnter()``` when the mouse collider enters the object collider, ```OnMouseExit()``` when the mouse collider exits the object collider, ```OnMouseOver()``` when the mouse collider stays colliding with the object. They are detected simultanuously, i.e., we can conclude that when ```OnMouseDrag()``` is called, ```OnMouseOver()``` must can called as well. These functions should be declared in the script which is the component of the target gameObject whose collider is what we want to interact with. Also, make sure the gameObject has its collider, whether trigger or not is not important.

 - Object: Base class for all objects Unity can reference. Therefore, for static methods, we do not have to explicit write prefix ```Object.```. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Object.html).
   - Every object in unity can transfer to string via ```ToString()```.
   - We can destroy an object ```obj``` with time delay ```t``` via ```Destroy(Object obj, float t = 0.0f)```. 
   - We can instantiate an object ```original``` via ```Instantiate(Object original)```. We can specify the parent of ```original``` via ```Transform parent``` parameter which is suppoted by some overloads, otherwise ```original``` will not have parent. We can specify ```Vector3 position``` and ```Quaternion rotation``` in world coordinate, otherwise ```original``` is instantiated as the default position and rotation of the mesh or prefab, either in world coordinate or parent coordinate.
   - We can find the first active object by ```FindObjectOfType<T>``` all objects of type ```T``` by ```FindObjectsOfType<T>```.

 - GameObject: Base class for all entities in Unity Scenes. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/GameObject.html).
   - We can find an GameObject by name via ```Find(string name)```. ```name``` with '/' can indicate a hierarchy path name. Note that ```Find(string name)``` returns exactly one GameObject, but Unity allows duplicate names for different GameObjects. So it would be better to give different absolute name to different GameObjects and we can organize the same kind of GameObjects with the same tag and fetch the list of them with ```FindGameObjectsWithTag(string tag)```.
   - We activate / deavtivate the GameObject by ```SetActive(bool flag)```. A GameObject is active only when the it and all its direct and indirect parents are active, therefore activating a GameObject whose parents are inactivate will not actually active it. ```OnEnable()``` or ```OnDisable()``` are called as the GameObject received ```SetActive(true)``` or ```SetActive(false)```. Activating / deactivating a GameObjetc is useful, e.g., when only one object is at most used occationally.

 - Transform: Position, rotation and scale of an object. Transform follows the hierarchy of the scene. Every Transform can have at most one parent and several children. Transform is relative to the parent transform, which can be visualized by ```Pivot Local Coordinate```. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Transform.html).
   - Get the parent by ```transform.parent```. Get children number by ```childCount``` and get children by ```transform``` which is iterable, or by ```GetChild(int index)```. We can also get a child with its name by ```Find(string name)```.
   - To relationship between local and world coordinate is ```transform.parent.localToWorldMatrix.MultiplyPoint3x4(transform.localPosition)) == transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3(0.0f, 0.0f, 0.0f)) == transform.position``` and ```transform.parent.worldToLocalMatrix.MultiplyPoint3x4(transform.position)) == Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale).MultiplyPoint3x4(new Vector3(0.0f, 0.0f, 0.0f))) == transform.localPosition```. The reason why we use ```transform.parent.localToWorldMatrix``` is becasue ```localPosition, localRotation (localEulerAngles), localScale``` is all relative to the parent transform. Via ```Matrix4x4.TRS(transform.localPosition, transform.localRotation, transform.localScale)```, we get the relative tranform between child and parent.
   - ```position, rotation (eulerAngles), scale``` store the information in the world coordinate.
   - Update either ```eulerAngle (Vector3)``` or ```rotation (Quaternion)``` will make the object rotate and update the other one automatically. However, explicitly updating ```eulerAngle (Vector3)``` might result in Gimbal Lock. ```rotation (Quaternion)```, on the other hand, does not suffer from Gimbal Lock, but cannot rotate over 180 degree, (recall that quaternion rotates in the least distance, which means rotation over 180 degree will actually rotate in the other direction instead.)  Thus, use ```Rotate()``` method instead of explicit modify these fields and try to avoid large degree rotation. For more details, click [here](https://docs.unity3d.com/Manual/QuaternionAndEulerRotationsInUnity.html).
   - Transform also stores the tag of the gameObject.
   - Retrieve the local X, Y, Z direction in world coordinate, a unit vector, by ```right, up, forward```. Since it is in the local coordinate, it take the rotation of the object in the world coordinate as well. If we would like to retrieve the global X, Y, Z direction, using ```Vector3.[Static Properties]```.
   - For ```Translate(), Rotate()```, ```Space.Self``` is used as default coordinate. We can specify as, for instance, Space.World according to the signature of the methods. E.g. ```transform.Translate(Vector3.forward, Space.Self) == transform.Translate(transform.forward, Space.World)```.

 - Rigidbody: Control of an object's position through physics simulation. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Rigidbody.html).
   - Apply force and torque via ```AddForce()```, ```AddTorque()``` where the parameter ```force``` and ```torque``` is in world coordinate or ```AddRelativeForce()```, ```AddRelativeTorque()``` where the parameter ```force``` and ```torque``` is in self coordinate. The second parameter is ```ForceMode```, including ```Force``` which applies the force ```f=m*a```, ```Acceleration``` which applies the acceleration ```a```, ```Impulse``` which applies the impulse ```j=m*dv```, ```VelocityChange``` which applies the diff velocity ```dv```.

 - Physics: Global physics properties and helper methods. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Physics.html).
   - Simulate a ray in the scene for collision detection. ```Raycast(...)``` returns true when the ray intersects any collider, otherwise false. ```Raycast(...)``` has several signatures and those that has parameter ```out RaycastHit hitInfo``` can store more information about where the closest collider was hit. 

 - RaycastHit: Structure used to get information back from a raycast. For more detailed API, click [here](https://docs.unity3d.com/ScriptReference/RaycastHit.html).

 - Collision: Describes a collision with a specific GameObject. For more details, click [here](https://docs.unity3d.com/ScriptReference/Collision.html).
   - Collision is linked with a specific GameObject. Therefore, the following attribute ```gameObject```, ```relativeVelocity```, ```rigidbody```, ```transform``` is all related to this collided GameObject and ther are all ReadOnly.
   - However, even if there is only one collided GameObject, there can be multiple contact points. ```contactCount``` tells the number of contact points. ```contacts``` returns an array of ContactPoint. From inside ```OnCollisionStay``` or ```OnCollisionEnter``` you can always be sure that contacts has at least one element. However, avoid directly retrieving ```contacts```, use ```GetContact(int index)``` to get a certain ContactPoint.

 - ContactPoint: Describes a contact point where the collision occurs with property ```normal```, ```point```, ```seperation``` and two colliders - ```thisCollider``` and ```otherCollider```. ```normal``` and ```point``` are described in world coordinate. For more details, click [here](https://docs.unity3d.com/ScriptReference/ContactPoint.html).

 - Animator: Interface to control the Mecanim animation system. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Animator.html).
   - Set parameters in controller by ```SetFloat(), SetInteger(), SetBool(), SetTrigger()``` which corresponds to the four data types in animator controller.
   - We can rotate the bone and its child joint by ```SetBoneLocalRotation(HumanBodyBones humanBoneId, Quaternion rotation)```. ```HumanBodyBones``` provides enums for predefined joint in humanoid avatar, for more details, click [here](https://docs.unity3d.com/ScriptReference/HumanBodyBones.html). ```rotation``` is measured in local coordinate, which is equalavent to what we get by ```transform.localRotation```.

 - AudioSource: A representation of audio sources in 3D. For more details, click [here](https://docs.unity3d.com/ScriptReference/AudioSource.html).
   - The default clip is saved in ```clip```. The volume is saved in ```volume```. If loop is saved in ```loop```.
   - We can ```Play()``` / ```Pause()``` / ```UnPause()``` / ```Stop()``` the AudioClip saved in ```clip```.
   - Play the clip with a delayed time via ```PlayDelayed(float second)```
   - Play a temporary clip via ```PlayOneShot(AudioClip clip, float volumeScale = 1.0F)```, it is not looped. After the clip is over, the default clip is resumed.

 - Input: Interface into the Input system. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Input.html).
   - Get the value of the virtual axis via ```Input.GetAxis()``` in continuous field in [-1,1] or ```Input.GetAxisRaw()``` in discrete field in {-1 ,0 ,1}. To set up input or view the options for ```axisName``` via ```Edit / Project Settings / Input Manager```. The options include the trigger event for positive and negative reactions. It is proper for retrieving user's mouse and joystick movement.
   - Get specific key interaction, including keyboard, mouse and joystick, via ```GetKeyDown(...)``` which detects if the user starts pressing one key down, ```GetKey(...)``` which detects if the user keeps holding one key down, ```GetKeyUp(...)``` which detects if the user starts releasing one key up. This brings more flexifibility than the previous introduced virtual axis.
   - Gey specific mouse interaction via ```GetMouseButtonDown(...)``` which detects if the user starts pressing one mouse button down, ```GetMouseButton(...)``` which detects if the user keeps holding one mouse button down, ```GetMouseButtonUp(...)``` which detects if the user starts releasing one mouse button up. This is equivalent to ```GetKeyXXX()``` whose parameter is ```KeyCode.MouseX```. 

 - Material: Describe the property of material. We can get the material of a gameObject by ```Renderer.material``` For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Material.html).
   - Get the attribute of texture by ```mainTexture, mainTextureOffset, mainTextureScale```. These properties can be read as well as modified.

 - KeyCode: Key codes that map to physical key, including keyboard, mouse and joystick. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/KeyCode.html).

 - Time: The interface to get time information. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Time.html).
   - deltaTime: The passed time of every frame.
   - frameCount: The total number of passed frames.
   - timeScale: The scale of time. ```1.0f``` is the normal speed, ```0.5f``` slow the time to half and ```0.0f``` freeze the time which can be used as the way of pausing the game.

 - Random: Generate random data. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Random.html).
   - Generate Vecotr4-like Color via ```ColorHSV```.
   - Generate random float number via ```Range(float min, float max)```.
   - Generate random unit vector via property ```onUnitSphere```.

 - Debug: Class containing methods to ease debugging while developing. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Debug.html).
   - Print a message at the bottom of game view via ```Log()```.

 - Mathf: A collection of common math functions. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Mathf.html).

 - TextMeshPro: Interface to TextMeshPro component. For detailed API, click [here](http://digitalnativestudios.com/textmeshpro/docs/ScriptReference/TextMeshPro.html)
   - To interact with ```TextMeshPro```, using the interface ```TMPro``` in the C# script.
   - We can get the gameObject of the UI component by ```gameObject``` property, then we can control it using the interface of gameObject in Unity.

 - Button: Class in UnityEngine.UI. A standard button that can be clicked in order to trigger an event. For detailed API, click [here](https://docs.unity3d.com/2018.3/Documentation/ScriptReference/UI.Button.html)
   - We can determine the behavior of button clicking using 2 methods - Editor / ```button.onClick.AddListener()```. Editor implements a static binding which is faster which ```button.onClick.AddListener()``` implements a dynamic binding which enables us to choose the behavior via script with less limitations, e.g., more conditions. A good advice of coding is to call ```AddListener()``` in ```OnEnable()``` and ```RemoveListener()``` in ```OnDisable()```.

 - Cursor: Interface for setting the cursor (mouse pointer). For detailed API, click [here](https://docs.unity3d.com/ScriptReference/Cursor.html).
   - ```lockState``` determines whether the hardware pointer is locked to the center of the view, constrained to the window, or not constrained at all. ```CursorLockMode.None``` makes the cursor unmodified, ```CursorLockMode.Locked``` makes the cursor lock at the center of the game window, ```CursorLockMode.Confined``` constrain the cursor inside the window.
   - ```visible``` determines whether the cursor is visible. Note that ```CursorLockMode.Locked``` implicitly makes the cursor invisible.
   - ```SetCursor(Texture2D texture, Vector2 hotspot, CursorMode cursorMode)``` set cursor with texture.

 - SceneManager: API for Scene management at run-time. For detailed API, click [here](https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html).
   - We can switch the scenes by ```void LoadScene(int sceneBuildIndex)``` or ```void LoadScene(string sceneName)```. The index of the scenes can be found in ```File / Build Settings```
   - We can get the current active scene by ```GetActiveScene()```. The returned ```Scene``` type has properties, e.g., ```buildIndex```, ```name``` ect.