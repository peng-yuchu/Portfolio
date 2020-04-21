#include "header.h"




/*
Function: welcom_screen
Date Created: April 6 2019
Date Last Modified: April 6 2019
Description: Display a welcome screen and rules
Input Parameters: N/A
Returns: N/A

*/
void welcome_screen(void)
{
	printf("*****Welcome to battleship!*****\nRules of the Game:\n1. This is a two player game.\n2. Player1 is you and Player2 is the computer.\nPlot all of your ships by drawing an outline of each ship on the grid according to its size. For example, a battleship is four blocks, but an aircraft carrier is five blocks. This is illustrated in the key on the printable game page. Ships may not overlap.\n\nTake turns firing upon the enemy by calling out plot points - for example: A-5. Mark your shot as a hit (X) or a miss (O) on your enemy ship grid according to your opponents reply.\n\nWhen your enemy fires upon you, answer hit or miss, according to their shot. Mark your hit ships with an X on the ""my ships"" grid.\n\nWhen the ships are sunk, you must inform your opponent that it is sunk and which ship it is, for example, ""My aircraft carrier is sunk!"".\n\nThe first person to sink all of the enemy ships wins the game.\n\nHit enter to start the game!");
}
/*
Function: initialize_game_board
Date Created: April 6 2019
Date Last Modified:April 6 2019
Description: to initialize a game board
Input Parameters: a 2D array
Returns:N/A

*/
void initialize_game_board(char board[10][10])
{
	int row, column;
	for (row = 0; row < 10; row++) {
		for (column = 0; column < 10; column++) {
			board[row][column] = '-';
		}
	}
	

}
/*
Function:select_who_starts_first
Date Created:April 6 2019
Date Last Modified:April 6 2019
Description: To select which player start first
Input Parameters:N/A
Returns:a integer

*/
int select_who_starts_first(void)
{
	int player = 0;
	player = (rand() % 2) + 1;
	if (player == 1)
	{
		printf("Player 1 has been randomly selected to go first.\n");
		return 1;
	}
	else
	{
		printf("Player 2 has been randomly selected to go first.\n");
		return 0;
	}
}
/*
Function:manually_place_ships_on_board
Date Created:April 6 2019
Date Last Modified:April 6 2019
Description: manully place the ship on the game board
Input Parameters: a 2D array
Returns:N/A
*/
void manually_place_ships_on_board(char board[10][10])
{
	int i, row1 = 0, row2 = 0, row3 = 0, row4 = 0, row5 = 0, col1 = 0, col2 = 0, col3 = 0, col4 = 0, col5 = 0;
	printf("Please enter the five cells to place the Carrier across:\n");
	scanf("%d%d%d%d%d%d%d%d%d%d", &row1, &col1, &row2, &col2, &row3, &col3, &row4, &col4, &row5, &col5);
	
	board[row1][col1] = 'c';
	board[row2][col2] = 'c';
	board[row3][col3] = 'c';
	board[row4][col4] = 'c';
	board[row5][col5] = 'c';

	printf("Please enter the four cells to place the Battleship across:\n");
	scanf("%d%d%d%d%d%d%d%d", &row1, &col1, &row2, &col2, &row3, &col3, &row4, &col4);
	
	board[row1][col1] = 'b';
	board[row2][col2] = 'b';
	board[row3][col3] = 'b';
	board[row4][col4] = 'b';
	
	printf("Please enter the three cells to place the Cruiser across:\n");
	scanf("%d%d%d%d%d%d", &row1, &col1, &row2, &col2, &row3, &col3);
	
	board[row1][col1] = 'r';
	board[row2][col2] = 'r';
	board[row3][col3] = 'r';

	printf("Please enter the three cells to place the Submarine across:\n");
	scanf("%d%d%d%d%d%d", &row1, &col1, &row2, &col2, &row3, &col3);

	board[row1][col1] = 's';
	board[row2][col2] = 's';
	board[row3][col3] = 's';

	printf("Please enter the two cells to place the Destroyer across:\n");
	scanf("%d%d%d%d", &row1, &col1, &row2, &col2);

	board[row1][col1] = 'd';
	board[row2][col2] = 'd';
}
/*
Function:randomly_place_ships_on_board
Date Created:April 6 2019
Date Last Modified:April 6 2019
Description: randomly select ships on board
Input Parameters: a 2d array
Returns:N/A
*/
void randomly_place_ships_on_board(char board[10][10])
{
	int rand_row = 0, rand_col = 0, j, ans = 0, i =0;
	srand(time(NULL));
	while (i < 5) {
		if (i == 0)
		{
			rand_row = rand() % 6;
			rand_col = rand() % 10;
			for (j = 0; j < 5; j++)
			{
				board[rand_row][rand_col] = 'c';
				rand_row++;
			}
			

		}
		else if(i == 1)
		{
			while (ans != 1)
			{
				rand_row = rand() % 7;
				rand_col = rand() % 10;
				if (board[rand_row][rand_col] == '-')
				{
					if (board[rand_row+1][rand_col] == '-')
					{
						if (board[rand_row+2][rand_col] == '-')
						{
							if (board[rand_row+3][rand_col] == '-')
							{
								ans = 1;
							}
							else
							{
								ans = 0;
							}
						}
						else
						{
							ans = 0;
						}
					}
					else
					{
						ans = 0;
					}
				}
				else
				{
					ans = 0;
				}
			}

			for (j = 0; j < 4; j++)
			{
				board[rand_row][rand_col] = 'b';
				rand_row++;
			}
		
		}
		else if(i == 2)
		{
			while (ans != 1)
			{
				rand_row = rand() % 8;
				rand_col = rand() % 10;
				if (board[rand_row][rand_col] == '-')
				{
					if (board[rand_row+1][rand_col] == '-')
					{
						if (board[rand_row+2][rand_col] == '-')
						{
							ans = 1;
						}
						else
						{
							ans = 0;
						}
					}
					else
					{
						ans = 0;
					}
				}
				else
				{
					ans = 0;
				}
			}

			for (j = 0; j < 3; j++)
			{
				board[rand_row][rand_col] = 'r';
				rand_row++;
			}
		}
		else if(i == 3)
		{
			while (ans != 1)
			{
				rand_row = rand() % 8;
				rand_col = rand() % 10;
				if (board[rand_row][rand_col] == '-')
				{
					if (board[rand_row+1][rand_col] == '-')
					{
						if (board[rand_row+2][rand_col] == '-')
						{
							ans = 1;
						}
						else
						{
							ans = 0;
						}
					}
					else
					{
						ans = 0;
					}
				}
				else
				{
					ans = 0;
				}
			}

			for (j = 0; j < 3; j++)
			{
				board[rand_row][rand_col] = 's';
				rand_row++;
			}
		}
		else if(i == 4)
		{
			while (ans != 1)
			{
				rand_row = rand() % 9;
				rand_col = rand() % 10;
				if (board[rand_row][rand_col] == '-')
				{
					if (board[rand_row + 1][rand_col] == '-')
					{
						ans = 1;
					}
					else
					{
						ans = 0;
					}
				}
				else
				{
					ans = 0;
				}
			}

			for (j = 0; j < 2; j++)
			{
				board[rand_row][rand_col] = 'd';
				rand_row++;
			}
		}
		i++;
		ans = 0;
	}
}


/*
Function:check_shot
Date Created:April 6 2019
Date Last Modified:April 6 2019
Description: check if the shot is missed or not
Input Parameters: two 2d array and two integers
Returns:N/A
*/
void check_shot(char board[10][10], char board1[10][10], int row, int col)
{
	if (board[row][col] == '-')
	{
		printf("%d,%d is a miss\n", row, col);
		board1[row][col] = 'm';
	}
	else
	{
		printf("%d,%d is a hit\n", row, col);
		board1[row][col] = '*';
	}
}

/*
Function:display_board
Date Created:April 6 2019
Date Last Modified:April 6 2019
Description:to display the game board
Input Parameters:two 2d array
Returns:N/A
*/

void display_board(char board[10][10], char board1[10][10])
{
	int i;
	printf("Player1's game board:\n");
	printf(" 0123456789\n");
	for (i = 0; i < 10; i++)
	{
		printf("%d%c%c%c%c%c%c%c%c%c%c\n", i, board[i][0], board[i][1], board[i][2], board[i][3], board[i][4], board[i][5], board[i][6], board[i][7], board[i][8], board[i][9]);
	}
	printf("Player2's game board:\n");
		printf(" 0123456789\n");
	for (i = 0; i < 10; i++)
	{
		printf("%d%c%c%c%c%c%c%c%c%c%c\n", i, board1[i][0], board1[i][1], board1[i][2], board1[i][3], board1[i][4], board1[i][5], board1[i][6], board1[i][7], board1[i][8], board1[i][9]);
	}
}

/*
Function:rand_shot
Date Created:April 6 2019
Date Last Modified:April 6 2019
Description:generate random shot on player one
Input Parameters: a 2d array
Returns:N/A
*/
void rand_shot(char board[10][10])
{
	int row = 0, col = 0, result =0;
	while (result != 1)
	{
		row = rand() % 10;
		col = rand() % 10;
		if (board[row][col] != 'm')
		{
			if (board[row][col] != '-')
			{
				printf("Player selects: %d %d\n%d,%d is a hit!\n", row, col, row, col);
				board[row][col] = '*';
				result = 1;
			}
			else
			{
				printf("Player selects: %d %d\n%d,%d is a miss!\n", row, col, row, col);
				board[row][col] = 'm';
				result = 1;
			}
		}
		else
		{
			result = 0;
		}
	}
	
}
/*
Function:check_boat
Date Created:April 6 2019
Date Last Modified:April 6 2019
Description:Check if all the boat is sunk
Input Parameters:a 2d array
Returns:a integer
*/
int check_boat(char board[10][10])
{
	int count = 0,i,j;
	for (i = 0; i < 10; i++)
	{
		for (j = 0; j < 10; j++)
		{
			if (board[i][j] == '*')
			{
				count=count+1;
			}
			else
			{
				count = count;
			}
		}
	}
	if (count == 17)
	{
		return 1;
	}
	else
	{
		return 0;
	}
}