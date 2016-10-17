#include <iostream>
#include <string>
#include <fstream>

#include "datagram.h"
#include "system_utilities.h"
#include "definitions.h"


using namespace std;


ifstream* inp = new ifstream;


int main() {
    string* cmd_line = new string;
    string* parsed_command = new string[MAX_TOKENS_ON_A_LINE];
    int  number_of_tokens;
    int  done;
    
    int cmd;
    
    int error_code;
        
    // Determine and set up source of input.
    if(COMMANDS_FROM_FILE) {
        (*inp).open("test.txt", ios::in);
        if(!(*inp)) {
            cout << "***Could not open file.";
            return 0;
        }
    }
    
    fillSystemCommandList();
    
    done = 0;
    
    do {
        error_code = 0;
        
        //Get command from input and parse into arg0 and argv
        if(COMMANDS_FROM_FILE)
            getline(*inp, *cmd_line);
        else {
            cout << ">";           // command line prompt
            getline(cin, *cmd_line);//, MAX_CMD_LINE_LENGTH);
        }
        number_of_tokens = parseCommandLine(*cmd_line, parsed_command);
        if(ECHO_COMMAND) print_token_list(number_of_tokens, parsed_command);
        
        cmd = getCommandNumber(parsed_command[0]);
        switch(cmd) {
            case HALT:
                cout << "** Command HALT recognized" << endl;
                done = true;
                break;
            case SYSTEM_STATUS:
                cout << "** Command SYSTEM_STATUS recognized" << endl;
                cout << "The second token is the number " << convertStringToValue(parsed_command[1]) << endl;
                break;
            case CREATE_MACHINE:
                cout << "** Command CREATE_MACHINE recognized" << endl;
                break;
            case DESTROY_MACHINE:
                cout << "** Command DESTROY_MACHINE recognized" << endl;
                break;
            case CONNECT:
                cout << "** Command CONNECT recognized" << endl;
                break;
            case CHECK_CONNECTIONS:
                cout << "** Command CHECK_CONNECTIONS recognized" << endl;
                break;
            case DATAGRAM_CMD: {
                cout << "** Command DATAGRAM recognized" << endl;
                IPAddress* ip1 = new IPAddress;
                IPAddress* ip2 = new IPAddress;
                int i = ip1->parse(parsed_command[1]);
                int j = ip2->parse(parsed_command[2]);
                if (i != BAD_IP_ADDRESS && j != BAD_IP_ADDRESS) {
                    datagram* d = new datagram;
                    d->makeDatagram(*ip1, *ip2, parsed_command[3]);
                    d->display();
                    delete d;
                }
                else
                    error_code = BAD_IP_ADDRESS;
                delete ip1;
                delete ip2;
                break;
            }
            case CONSUME_DATAGRAM:
                cout << "** Command CONSUME_DATAGRAM recognized" << endl;
                break;
            case TIME_CLICK:
                cout << "** Command TIME_CLICK recognized" << endl;
                break;
            default:
                error_code = UNDEFINED_COMMAND;
                break;
        }
        
        if(error_code!=0) printError(error_code);
        
        if(COMMANDS_FROM_FILE) wait();
        
    }
    while(!done);
    
    cout << "That's all, folks." << endl;
    
    freeSystemCommandList(); //deletes the list of command elements
    delete[] parsed_command;
    delete cmd_line;
    delete inp;
}

