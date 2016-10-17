// This file contains functions used at the system and main level.

#include <iostream>
#include <string>

#include "definitions.h"
#include "system_utilities.h"

using namespace std;

command_element** system_commands = new command_element*[NUMBER_OF_COMMANDS];

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
    
    char token_terminator = ' ';
    string* accumulator = new string;
    *accumulator = "";
    int i = 0; //the index of the current character we're looking at
    int numTokens = 0; //the number of tokens we've collected so far
    
    if (line.length() > MAX_CMD_LINE_LENGTH)
    {
        return 0;
    }
    
    while (i < line.size()) { //while we have more characters to look at
        if (numTokens > MAX_TOKENS_ON_A_LINE)
        {
            return 0;
        }
        
        if (line[i] == ' ')
        {
            //we've encountered a space, is it a terminating space?
            if (*accumulator != "" && token_terminator == ' ')
            {
                tklist[numTokens] = *accumulator;
                *accumulator = "";
                numTokens++;
            } else if (*accumulator != "") //if not, it might be inside of quotes
            {
                *accumulator += " ";
            }
            i++; //move forward
        }
        else if (line[i] == '\"')
        {
            //we've encountered a quote is it an ending quote?
            if (*accumulator != "")
            {
                tklist[numTokens] = *accumulator;
                *accumulator = "";
                numTokens++;
                token_terminator = ' ';
            }
            else //if not, it must be a starting quote
            {
                token_terminator = '\"';
            }
            i++;
        }
        else { //not a quote or space, so just add the char to accumulator
            *accumulator = *accumulator + line[i];
            i++;
        }
    }
    
    //the loop has finished, so if there's stuff left in the accumulator, add it
    if (*accumulator != "")
    {
        tklist[numTokens] = *accumulator;
        *accumulator = "";
        numTokens++;
    }
    
    delete accumulator;
        
    return numTokens;
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

//******define 4 functions and one array for PA5 below here******
//******stubs are provided for the 4 functions******

void fillSystemCommandList() {
    system_commands[0] = new command_element;
    (*system_commands[0]).c = "halt";
    (*system_commands[0]).cnum = HALT;
    system_commands[1] = new command_element;
    (*system_commands[1]).c = "system_status";
    (*system_commands[1]).cnum = SYSTEM_STATUS;
    system_commands[2] = new command_element;
    (*system_commands[2]).c = "create_machine";
    (*system_commands[2]).cnum = CREATE_MACHINE;
    system_commands[3] = new command_element;
    (*system_commands[3]).c = "destroy_machine";
    (*system_commands[3]).cnum = DESTROY_MACHINE;
    system_commands[4] = new command_element;
    (*system_commands[4]).c = "connect";
    (*system_commands[4]).cnum = CONNECT;
    system_commands[5] = new command_element;
    (*system_commands[5]).c = "check_connections";
    (*system_commands[5]).cnum = CHECK_CONNECTIONS;
    system_commands[6] = new command_element;
    (*system_commands[6]).c = "datagram";
    (*system_commands[6]).cnum = DATAGRAM_CMD;
    system_commands[7] = new command_element;
    (*system_commands[7]).c = "consume_datagram";
    (*system_commands[7]).cnum = CONSUME_DATAGRAM;
    system_commands[8] = new command_element;
    (*system_commands[8]).c = "time_click";
    (*system_commands[8]).cnum = TIME_CLICK;
}

void freeSystemCommandList() {
    for (int i = 0; i == NUMBER_OF_COMMANDS; i++){
        delete system_commands[i];
    } //need to call delete[] system_commands?
}

int getCommandNumber(string s) {
    for (int i = 0; i < NUMBER_OF_COMMANDS; i++){
        if ((*system_commands[i]).c == s) {
            return (*system_commands[i]).cnum;
        }
    }
    return UNDEFINED_COMMAND;
}

int convertStringToValue(string arg) {
    int num = 0;
    for (int i=0; i<arg.length(); i++)
        num = (num * 10) + (arg[i]-'0');
    return num;
}

