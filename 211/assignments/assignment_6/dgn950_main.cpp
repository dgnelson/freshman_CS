#include <iostream>
#include <string>
#include <fstream>

#include "datagram.h"
#include "system_utilities.h"
#include "definitions.h"
#include "machines.h"

using namespace std;


ifstream* inp = new ifstream;
node* network[MAX_MACHINES];

int main() {
    for(int i = 0; i < MAX_MACHINES; i++)
        network[i] = NULL;
    string* cmd_line = new string;
    string* parsed_command = new string[MAX_TOKENS_ON_A_LINE];
    int  number_of_tokens;
    int  done;
    
    int cmd;
    int ec1, ec2, error_code;
    IPAddress ip1, ip2;
    datagram d;
    datagram* dat;

    // Determine and set up source of input.
    if(COMMANDS_FROM_FILE) {
        (*inp).open("p6input.txt", ios::in);
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
                done = 1;
                break;
            case SYSTEM_STATUS:
                cout << "** Command SYSTEM_STATUS recognized" << endl << "Nodes in the network:" << endl;
                for (int i = 0; i < MAX_MACHINES; i++) {
                    if (network[i]!=NULL)
                        network[i]->display();
                }
                cout << "End of network." << endl;
                break;
            case CREATE_MACHINE:
                cout << "** Command CREATE_MACHINE recognized" << endl << endl;
                if (ip1.parse(parsed_command[3])!=0) {
                    error_code = BAD_IP_ADDRESS;
                    break;
                }
                else if(parsed_command[1]!="laptop" && parsed_command[1]!="server" && parsed_command[1]!="wan"){
                    error_code = UNKNOWN_MACHINE_TYPE;
                    break;
                }
                for (int i = 0; i<MAX_MACHINES; i++) {
                    if (network[i] == NULL) {
                        if (parsed_command[1] == "laptop")
                            network[i] = new laptop(parsed_command[2], ip1);
                        else if (parsed_command[1] == "server")
                            network[i] = new server(parsed_command[2], ip1);
                        else //WAN
                            network[i] = new WAN(parsed_command[2], ip1);
                        network[i]->display();
                        break;
                    }
                    else if (i == MAX_MACHINES-1){
                        error_code = NETWORK_FULL;
                        break;
                    }
                }
                break;
            case DESTROY_MACHINE:
                cout << "** Command DESTROY_MACHINE recognized" << endl;
                if (ip1.parse(parsed_command[1])!= 0) {
                    error_code = BAD_IP_ADDRESS;
                    break;
                }
                for (int i = 0; i < MAX_MACHINES; i++) {
                    if (network[i]!=NULL && network[i]->amIThisComputer(ip1)) {
                        delete network[i];
                        network[i] = NULL;
                        cout << "Computer removed from the network." << endl;
                        break;
                    }
                    else if (i == MAX_MACHINES-1){
                        error_code = NO_SUCH_MACHINE;
                        break;
                    }
                }
                break;
            case CONNECT:
                cout << "** Command CONNECT recognized" << endl;
                break;
            case CHECK_CONNECTIONS:
                cout << "** Command CHECK_CONNECTIONS recognized" << endl;
                break;
            case DATAGRAM_CMD:
                cout << "** Command DATAGRAM recognized" << endl;
                ec1 = ip1.parse(parsed_command[1]);
                ec2 = ip2.parse(parsed_command[2]);
                if( (ec1!=0) || (ec2!=0) )
                    error_code = BAD_IP_ADDRESS;
                else {
                    for (int i = 0; i < MAX_MACHINES; i++) {
                        if (network[i]!=NULL && network[i]->amIThisComputer(ip1)) {
                            dat = new datagram();
                            dat->makeDatagram(ip1, ip2, parsed_command[3]);
                            dat->display();
                            cout << endl;
                            (dynamic_cast<laptop*>(network[i]))->initiateDatagram(dat);
                            break;
                        }
                        else if (i == MAX_MACHINES-1){
                            error_code = NO_SUCH_MACHINE;
                            break;
                        }
                    }
                }
                break;
            case CONSUME_DATAGRAM:
                cout << "** Command CONSUME_DATAGRAM recognized" << endl;
                break;
            case TIME_CLICK:
                cout << "** Command TIME_CLICK recognized" << endl;
                break;
            case UNDEFINED_COMMAND:
                cout << "***ERROR:  Unrecognized command" << endl;
                break;
        }
        
        if(error_code!=0) printError(error_code);
        
        if(COMMANDS_FROM_FILE) wait();
        
    }
    while(!done);
    
    cout << "That's all, folks.";
    
    freeSystemCommandList(); //deletes the list of command elements
    delete[] parsed_command;
    delete cmd_line;
    delete inp;
    
    //the following deletes any machines that were not "destroyed"
    for (int i = 0; i < MAX_MACHINES; i++)
    {
        if (network[i] != NULL)
            delete network[i];
    }
}

