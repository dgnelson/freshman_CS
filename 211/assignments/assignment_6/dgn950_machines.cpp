#include <iostream>
#include <stdlib.h>
#include <string>

#include "machines.h"

//*****************************************//
// node class functions

node::node(string n, IPAddress a){
    my_address = a;
    name = new string;
    *name = n;
}

node::~node(){
    delete name;
}

void node::display() {
    
    cout << "   Name: " << *name << "   IP address: ";
    my_address.display();
}

int node::amIThisComputer(IPAddress x){
    return x.sameAddress(this->my_address);
}

int node::myType(){
    return 0;
}

//*****************************************//
// laptop class functions

laptop::laptop(string n, IPAddress a) : node(n,a){
    this->incoming = NULL;
    this->outgoing = NULL;
    my_server.parse("0.0.0.0"); //this statement should be redundant
    
}

void laptop::display() {
    
    cout << "Laptop computer:  ";
    node::display();
    
    cout << "\n   Incoming datagram:  ";
    if(incoming==NULL) cout << "No datagram.";
    else               {cout << "\n";  incoming->display(); }
    
    cout << "\n   Outgoing datagram:  ";
    if(outgoing==NULL) cout << "No datagram." << endl;
    else               {cout << "\n"; outgoing->display(); }
    cout << endl;
}

void laptop::initiateDatagram(datagram* d){
    outgoing = d;
}

void laptop::receiveDatagram(datagram *d){
    incoming = d;
}

int laptop::myType(){
    return LAPTOP;
}

//*****************************************//
// server class functions

server::server(string n, IPAddress a) : node(n, a){
    this->number_of_laptops = 0;
    this->number_of_wans = 0;
}

void server::display() {
    
    cout << "Server computer:  ";
    node::display();
    
    cout << "\n   Connections to laptops: ";
    cout << "List is empty.";
    cout << "\n   Connections to WANs:    ";
    cout << "List is empty.";
    cout << endl;
    cout << endl;
}

int server::myType(){
    return SERVER;
}

//*****************************************//
// WAN class functions

WAN::WAN(string n, IPAddress a) : node(n,a) {
    this->number_of_servers = 0;
    this->number_of_wans = 0;
}

void WAN::display() {
    
    cout << "WAN computer:  ";
    node::display();
    
    cout << "\n   Connections to laptops: ";
    cout << "List is empty.";
    cout << "\n   Connections to WANs:    ";
    cout << "List is empty.";
    cout << endl;
    cout << endl;
}

int WAN::myType(){
    return WAN_MACHINE;
}