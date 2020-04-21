#include <stdio.h>
#include <string.h>
#include <stdlib.h>

void welcome_screen(void);
void initialize_game_board(char board[10][10]);
int select_who_starts_first(void);
void manually_place_ships_on_board(char board[10][10]);
void randomly_place_ships_on_board(char baord[10][10]);
void display_board(char board[10][10], char board1[10][10]);
void check_shot(char board[10][10], char board1[10][10], int row, int col);
void rand_shot(char board[10][10]);
int check_boat(char board[10][10]);

