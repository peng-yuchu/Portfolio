/*************************************************************************
* Programmer: Peng-Yu Chu                                               *
* Class: CptS 121                                                       *
* Programming Assignment: PA6                                           *
* Date: April 6 2019                                                    *
*                                                                       *
* Description: This is a game program                                   *
************************************************************************/




#include "header.h"


int main(void)
{
	int c, who_start=0, option= 0, target_row= 0, target_col=0, result = 0, winner = 0;
	char a;
	char board1[10][10], board2[10][10], board2_hid[10][10];
	welcome_screen();
	scanf("%c", &a);
	if (a == '\n') {
		system("cls");
	}
	initialize_game_board(board1);
	initialize_game_board(board2);
	initialize_game_board(board2_hid);

	
	printf("Please select from the following menu:\n1. Enter positions of ships manually.\n2. Allow the program to randomly select positions ships.\n");
	scanf("%d", &option);
	switch (option)
	{
	case 1:
		manually_place_ships_on_board(board1);
		randomly_place_ships_on_board(board2_hid);
		printf("Player2 (Computer's) board has been generated.\n");
		break;
	case 2:
		randomly_place_ships_on_board(board1);
		randomly_place_ships_on_board(board2_hid);
		printf("Player2 (Computer's) board has been generated.\n");
		break;
	}
	who_start=select_who_starts_first();
	if (who_start == 1)
	{
		while (result != 1)
		{
			display_board(board1, board2);
			printf("Enter a target:\n");
			scanf("%d%d", &target_row, &target_col);
			system("cls");
			check_shot(board2_hid, board2, target_row, target_col);
			result = check_boat(board2);
			display_board(board1, board2);
			rand_shot(board1);
			result = check_boat(board1);
			printf("Hit enter to continue!\n");
			scanf("%c", &a);
			scanf("%c", &a);
			if (a == '\n')
			{
				system("cls");
			}
		}
	}
	else
	{
		while (result != 1)
		{
			display_board(board1, board2);
			rand_shot(board1);
			result = check_boat(board1);
			printf("Enter a target:\n");
			scanf("%d%d", &target_row, &target_col);
			system("cls");
			check_shot(board2_hid, board2, target_row, target_col);
			result = check_boat(board2);
			display_board(board1, board2);
			printf("Hit enter to continue!\n");
			scanf("%c", &a);
			scanf("%c", &a);
			if (a == '\n')
			{
				system("cls");
			}
		}
		

	}
	
	winner = check_boat(board1);
	if (result == 1)
	{
		printf("Player2 Win!");
	}
	else
	{
		printf("Palyer1 Win!");
	}
	



	return 0;
}