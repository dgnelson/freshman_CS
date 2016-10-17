// This file contains functions used at the system and main level.

#include <iostream>
#include <string>

#include "definitions.h"

using namespace std;

void wait() {
	// Prompt user for input and wait.
    string answer;

	cout << "Press ENTER to continue:";
    getline(cin, answer);
	return;
}

void printError(int errcode) {
	
	cout << "  ***ERROR:  ";
	switch(errcode) {
		case BAD_IP_ADDRESS:	
			cout << "Bad IP address.\n";
			break;
		default:
			cout << "Unrecognized error code.\n";
	}
}

int parseCommandLine(string line, string tklist[]) {
    int num_tokens = 0;
    string* ptr = tklist;
    string tempstr = "";
    bool midtk = false;
    bool midstr = false;
    for (int i = 0; i < line.length(); i++) {
        if (line[i] == '"'){
            if (midstr) {
                midstr = false; //now set a new token
                midtk = false;
                num_tokens++;
                *ptr = tempstr;
                tempstr = "";
                ptr++;
            }
            else
                midstr = true; //now move on to next char in line
        }
        else if (line[i] == ' ') {
            if (midstr)
                tempstr += line[i]; //now move on to next char in line
            else if (midtk){
                midtk = false; //now set a new token
                num_tokens++;
                *ptr = tempstr;
                tempstr = "";
                ptr++;
            }
        }
        else {
            midtk = true;
            tempstr += line[i];
        }
    }
    if (midtk) {
        num_tokens++;
        *ptr = tempstr;
        tempstr = "";
        ptr++;
    }
    return num_tokens;
}

void print_token_list(int num, string commands[])
{
    cout << "Number of tokens: " << num <<endl;
    for (int k = 0; k < num; k++)
    {
        cout << "token: " << commands[k] << "\t";
    }
    cout << endl;
}
