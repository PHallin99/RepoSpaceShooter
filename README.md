# RepoSpaceShooter

## How to Play!
**Movement** <br>
W / Up arrow key: Drive forward <br>
A / Left arrow key: Turn Left   <br>
S / Down arrow key: Reverse     <br>
D / Right arrow key: Turn Right <br>

**Shooting**  <br>
Space: Shoot  <br>

*Have fun shooting as many asteroids as possible! And don't crash into them!*
<br>
# Unity DOTS
DOTS stands for Data Oriented Technologt Stack and is Unitys solution for making games with a data oriented design instead of the more popular object oriented programming. After learning more about DOTS and seeing the capabilities of it through also getting my hands on it and understanding it more in depth have I understood how efficient it can be. Simply by rethinking your approach and implementing according to the data oriented design makes the project gain so much in performance and efficiency almost by default of reworking the architecture of the codebase. I also very much saw how the code became so much more clean in many ways because of the nature of splitting the code into data components and operating on that data with the systems and also gain the advantage of excellent multithreaded performance which leads to more complex games with more units, more effects etc. <br>

I first began creating a Asteroids clone in the way I'm most used to working in Unity. With monobehaviours with component designed systems where each component had all its data and methods within itself and also handled its own collisions. I also took note of the perfomance I got from doing it this way, having hundreds of things moving around in the game at once, everything from asteroids and lasers to the player and handling input. I noticed that the perfomance started to take a proper hit after the game had gone on for a while because of how many instances of different operations being done and with object oriented programming having so many things on the main thread but also having poor memory usage and it really does show after a while. When I was benchmarking with the profiler in development builds and also the stats from playing in editor I noticed the project would take something around 3ms in the very start but just after a little while when there was hundreds of objects around the time it took per frame to render was almost around 6ms. <br>

It was then I wanted to see both how it would be to refactor the whole game into using Unity's DOTS packages. I chose to take every part of DOTS into the project so all three of the C# Jobs system, ECS and of course the Burst Compiler. The C# Jobs enabled me to utilise the multicore CPUs with its multithreading capabilities and I was using it on the asteroids themselves, the lasers that the player shoots but also the collision system. I chose to use it on those parts of the project simply because I knew those things would be ran every frame on hundreds of entities all over the game and throughout the whole game. I think theres plenty of potential in utilising the multithreading in these types of projects where you will have plenty of entities where you can just put up the data oriented design and let the game shine on its strenghts. <br>

Coming to the second part of the Unity DOTS is the Entity Component System where I pretty much redefined the whole project from using only MonoBehaviours and now being my second time touching ECS APIs, last time was with the Photon netcode engine and when I was working with Unitys DOTS I did recall a lot of familiar ways of thinking. I really had to take a step back and think in different ways to rework DOTS into the project. ECS is all about seperating my code into logic and data, instead of having GameObjects with MonoBehaviours there is instead Enteties, Components and Systems. What makes this so much more performant is how is all setup with memory. For a CPU its way more efficient for it to look at the ECS more packed together groups of components instead of having to look all over memory for heavy GameObjects. I enjoyed having to think in a different way developing this project where components hold data, systems work on that data and entities refer to instances of component data. <br>

Now onto the last part of my optimising the project from a classic Unity object oriented programming style project, which is the burst compiler. The burst compiler translates C# code into very performant machine code and especially shines at optimising C# Jobs. I used plenty of jobs in the project that all are burst compiled to squeeze out even more quickness and performance in the project. The whole game almost runs on these jobs that operate in the Systems part of the project and makes all the operations needed through the game. <br>

# Edit After Feedback

