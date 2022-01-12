# MazeGenerator
Implementation of the Hunt and Kill and Recursive Backtracking algorithms to randomly generate perfect mazes in Unity.

# Important Scripts:
- Assets/GameManager.cs: GameManager class to manage/monitor game state. Listens for when user switches camera views.
- Assets/RobotMovement.cs: Logic for moving the player character (which, contextually, represents a robot). Contains speed and gravity scalars.
- Assets/CollectableBehaviour.cs: Controls behaviour (rotation, collision) of goal collectable in the environment.
- Assets/GenerateMaze.cs: Creates an 11x11 grid with cells with walls around the outer edges.
- Assets/RecursiveBacktracker.cs: Implementation of the Recursive Backtracking maze generation algorithm.
- Assets/Maze.cs: (Not used in actual simulation) First pass implementation of the Hunt and Kill algorithm. 
                  This class does several things:
                    - Creates a Grid with walls around the outer edges
                    - Uses the Hunt and Kill algorithm to create a maze within the Grid
                    - Spawns in robot (player character)
                    - Spawns in collectable (goal state)


# Recursive Backtracking Algorithm:
This algorithm is inspired on the Depth-First Search (DFS) "brute force" algorithm. At a high level, this works by randomly selecting a cell as a starting point and mark it as 'visited'.
Next, you check all adjacent neighbouring cells that are unvisted and pick one at random. Remove the wall between the current cell and the selected cell. Move to the selected cell and mark it as 'visited'.
From the next cell, once again, check all unvisited adjacent neighbours. Select one at random. Remove the wall between the current cell and the chosen cell. Move the the chosen cell and mark it as 'visited'.
Repeat this process until you have a cell whose neighbours are all visited. When this happens, backtrack to the most recent cell that has at least one unvisited neighbor and repeat this process until all cells are visited.

  0) Begin with a grid divided into cells
  1) Select a starting cell
  2) Mark cell as 'visited'
  3) While the current cell has any unvisited neighbouring cells:
    3.1) Randomly choose one of the unvisited neighbour cells
    3.2) Remove the wall between current cell and chosen neighbour cell
    3.3) Repeat routine recursively for chosen cell
    
    
# Hunt and Kill Algorithm:
  0) Begin with a grid divided into cells
  1) Select a starting cell
  2) Perform a random walk from the current cell, carving paths to unvisited neighbours 
  3) Repeat until current cell has no unvisited neighbouring cells
  4) Enter "hunt" mode: Scan the grid from top to bottom, left to right, looking for an unvisited cell that is adjacent to a visited cell.
    4.1) If found, carve a passage between the two and let the formerly unvisited cell be the new starting location
  5) Repeat steps 2) and 3) until the "hunt" mode scans the entire grid and finds no unvisited cells.
