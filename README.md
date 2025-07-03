# Object Pooling

## What is that?

Object pooling is a strategy to prevent multiple objects that are similar from being instantiated in runtime. It simply involves instantiating many objects that you could use and simply activate them when you need them. 

For example: in a FPS game, you can have a gun loaded with 30 bullets. Instead of instantiating each bullet whenever the player shoots, you can instantiate all of them in the moment the magazine is loaded and simply activate them whenever the player shoots.

## Why and when should I use it?

Instantiating and destroying objects are CPU-consuming processes. Therefore, when we want to frequently instantiate an destroy object, using an Object Pooling strategy can save your game's performance.

## How does it work here?

In order to create a pool of objects in your game, you'll need to create and empty object in the scene and add the *SpawnManager* as a component. The *SpawnManager* will use the *GameObjectPooler* class to manage the pool of objects.

You can override the SpawnManager completely in order to create your game's logic to spawn/despawn objects.

### GameObjectPooler

The GameObjectPooler is a C# class that have the responsibility of managing the pool of objects. It has the following attributes:

- Parent: This attribute will be used to define the parent of the instantiated objects in the hierarchy.
- Object: Represents the object that will be instantiated.
- ObjectPool: Represents the pool of objects. When an object is instantiated, it will be added to the list.
- InactiveObjects: Stores the index of inactive objects in the pool. Whenever we want to activate an object, we dequeue an index from the queue. This A First-In-First-Out use of inactive objects
- MaxSize: Represents the maximum number of objects that can be pooled.
- DefaultSize: Represents the number of objects that are going to be pooled whenever the class is initialized.

And it has the following methods:

- GetObject: Get an inactive object from the pool and activate it. If there's no inactive object in the pool, a new object will be pooled respecting the MaxSize attribute.
- ReleaseObject: Releases an active object from the pool, inactivating it again. If resetRigidbody is passed as true, the objects rigidbody attribute's value will be reset.

Notice that, as the GameObjectPooler class is not a MonoBehaviour, it cannot be used as a component for GameObjects. That's why we need the second important class.

### SpawnManager

The SpawnManager is a MonoBehavior class in which you'll write the logic to spawn/despawn your pooled objects. It must contain the following attributes:

- Prefab: The prefab that you want to use in the object pooler.
- MaxSize: The max size of the object pooler.
- DefaultSize: The default size of the object pooler.
- ObjectPooler: The object pooler class.

It is important to initialize the object pooler with the defined arguments in the Awake() funtions, so that all objects will be instantiated as soon as the SpawnManager is created in the scene.

## Using strategy pattern to define how and where to instantiate

If your game uses a timer to instantiate the objects this code already implements a strategy pattern in the SpawnManager in order to define when and where the prefab will be instantiated. There are two strategies: The condition, which defines when the object will be instantiated; And the position, which defines where the objects will be instantiated.

In the editor, we can define which strategy our spawn manager will use and create the right attributes in the *Object Pooler Configuration/* path that are going to be passed down to the strategies.

In the Awake() function, we get the right strategy class using the factories and initiate them with the attributes. Than, we simply use their implemented methods in the Update() function to spawn the objects.

- TIMER CONDITION: Defines a fixed time to spawn objects.
- TIME_RANGE CONDITION: Defines a range of time to spawn objects.

- FIXED POSITION: Defines a fixed world position to spawn the objects.
- RANGE POSITION: Defines a range of x, y and z world positions to instantiate the objects.