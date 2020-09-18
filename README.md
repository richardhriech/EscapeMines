# EscapeMines
Home assignment for a job application.

## Things that can be improved:
- Using dependency injection for even better separation of concerns. The size and the scope of this project didn't require DI,
but if it was needed, it could be easily implemented in the current structure.
- Using automapper. There were only a few cases where it could have been useful, so I did the mapping manually. Again, if
it was needed it would be easy to implement.
- Writing even more tests. When it comes to unit tests, the sky is the limit, but we've got to draw the line somewhere.
Given time constraint, the number of the tests (39) and the coverage (93%) seems pretty good to me.



# Requirements:
## Escape Mines
A turtle must walk through a minefield. Write a program (console application) that will
read the initial game settings and one or more sequences of moves. For each move
sequence, the program will output whether the sequence leads to the success or failure
of the little turtle.

The program should also handle the scenarios where the turtle doesn’t reach the exit
point or doesn’t hit a mine.

### Setup
- The board (or minefield) is a grid of N by M number of tiles.
- The starting position is a tile, represented by a set of zero based co-ordinates
(x, y) and the initial direction (i.e.: N, S, W or E).
- The exit point is a tile (x, y)
- The mines are defined as a list of tiles (x, y)

### Inputs
The game settings are to be loaded from a text file, which should follow this format:
- The first line should define the board size
- The second line should contain a list of mines (i.e. list of co-ordinates separated
by a space)
- The third line of the file should contain the exit point.
- The fourth line of the file should contain the starting position of the turtle.
- The fifth line to the end of the file should contain a series of moves.

#### Example:
5 4<br/>
1,1 1,3 3,3<br/>
4 2<br/>
0 1 N<br/>
R M L M M<br/>
R M M M<br/>

Where
- R = Rotate 90 degrees to the
right
- L = Rotate 90 degrees to the left
- N = North direction
- S = South direction
- W = West direction
- E = East direction
- M = Move

#### Turtle actions can be either:
- A move to the next neighbouring tile
- A rotation (90 degrees Right or Left)

### Results
Results can be:
- Success – if the turtle finds the exit point
- Mine Hit – if the turtle hits a mine
- Still in Danger – it the turtle has not yet found the exit or hit a mine 
