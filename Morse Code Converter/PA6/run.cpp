#include "Run.h"

void runapp()
{
	char firstChar = 0;
	string firstString;
	ifstream input;
	ifstream convert;
	input.open("MorseTable.txt");
	convert.open("Convert.txt");
	if (input)
	{
		input.get(firstChar);
		getline(input, firstString);
		getline(input, firstString);
		ListBT<char, string> morseTable(firstChar, firstString);
		collectTable(morseTable, input);
		morseTable.printTree();
		printMorse(morseTable, convert);
	}
	else
		cout << "Could not access files." << endl;
	cout << endl;
	input.close();
	convert.close();
}

void collectTable(ListBT<char, string>& morseTree, ifstream& input)
{
	char tempChar = 0;
	string tempString;
	while (input)
	{
		input.get(tempChar);
		getline(input, tempString);
		getline(input, tempString);
		if (tempChar != 'C')
			morseTree.insertNode(tempChar, tempString);
	}
}

void printMorse(ListBT<char, string>& morseTree, ifstream& convert)
{
	char tempChar = 0;
	string tempString;
	while (convert)
	{
		convert.get(tempChar);
		if (tempChar >= 'a' && tempChar <= 'z')
			tempChar -= 32;
		auto node = morseTree.search(tempChar);
		if (node)
			cout << node->getReference();
		else
			cout << tempChar;
	}
}