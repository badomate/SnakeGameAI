# SnakeGameAI

My AI for the Snake game, it's developement is currently on hold. Almost perfect, only dies on the last 10 percent, the reason of that is under invetigation. 

At the beggining the program generates a hemilonian cycle (https://mathworld.wolfram.com/HamiltonianCycle.html) tha algortihm is very inefficent. I have plans to make it parallel. 

after that the snake follows that path, but every few itteration, it checks is it is possible to make a shortcut. it's done by an A* pathfinding algorithm and a "wall generator" 
which decides for each sqaure if it is safe for the snake to "step" into. 

and that loops over and over. 
