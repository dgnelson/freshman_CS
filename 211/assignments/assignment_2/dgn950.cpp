#include <iostream>
using namespace std;

const int MAX_NUMBER_OF_PRIMES = 20;
const int MAX_NUMBER_OF_PRIME_FACTORS = 40;

/**
 *  Checks to see if a number is prime.
 *
 *  @param pr   an array of known prime numbers in ascending order
 *  @param nump the number of primes stored in the pr array
 *  @param x    the number we are checking
 *
 *  @return returns 0 if x is not prime. returns 1 if x is prime. returns -1 if
 *          we don't have enough primes in our array to determine if x is prime
 */
int isPrime(int pr[], int nump, int x) {
    if (x<=0)
        return 0;
    
    for ( int i=0; i<nump; i++){
        if (x<(pr[i]*pr[i]))
            return 1;
        else if (x%pr[i]==0)
            return 0;
    }
    return -1;
}

/**
 *  Generates and populates the prime number array, stopping once we reach
 *  MAX_NUMBER_OF_PRIMES primes.
 *
 *  @param pr the initial prime number array
 *  @param nump the number of primes stored in the pr array
 */
void genPrimes(int pr[], int nump) {
    for (int i=nump; i<MAX_NUMBER_OF_PRIMES; i++) {
        for (int j=pr[i-1]+1; true; j++) {
            if (isPrime(pr, nump, j)==1) {
                pr[i]=j;
                nump++;
                break;
            }
        }
    }
}

/**
 *  Computes the prime factors for the fourth input, x.
 *
 *  @param pr an array of prime numbers
 *  @param pf an array where we'll store the prime factors of x
 *  @param nump the number of primes stored in the pr array
 *  @param x  the number we want to factor
 *
 *  @return returns the number of prime factors stored in pf
 */
int genPrimeFactors(int pr[], int pf[], int nump, int x) {
    int numf = 0;
    int startIndex = 0;
    while (pr[startIndex]<=1)
        startIndex++;
    for (int i=startIndex; x>1; i++) {
        if (x%pr[i]==0){
            x = x/pr[i];
            pf[numf]=pr[i];
            numf++;
            i--;
        }
    }
    return numf;
}

int main() {
    //The provided main function shows how we will test your functions.
    //Feel free to add more tests here, but your code should work in
    //   with the tests that we have provided here.
    
    int primes[MAX_NUMBER_OF_PRIMES] = {}; //create an array for primes and allocate
    //enough space for MAX_NUMBER_OF_PRIMES
    //{} gives us default 0s
    int primeFactors[MAX_NUMBER_OF_PRIME_FACTORS] = {}; //an array to store the prime
    //    factors of a number once necessary
    //{} gives us default 0s
    int numberOfPrimes; //stores the number of primes currently held in the
    //    primes array
    
    // Set the first four primes and numberOfPrimes
    primes[0] = 2;
    primes[1] = 3;
    primes[2] = 5;
    primes[3] = 7;
    numberOfPrimes = 4;
    
    // tests for isPrime function
    int result = isPrime(primes, numberOfPrimes, 13);
    if (result == 1)    //13 is prime
        cout << "Test 1: pass\n";
    else
        cout << "Test 1: fail\n";
    
    result = isPrime(primes, numberOfPrimes, 47);
    if (result == 1)    //47 is prime
        cout << "Test 2: pass\n";
    else
        cout << "Test 2: fail\n";
    
    result = isPrime(primes, numberOfPrimes, 49);
    if (result == 0)    //49 is not prime
        cout << "Test 3: pass\n";
    else
        cout << "Test 3: fail\n";
    
    result = isPrime(primes, numberOfPrimes, 169);
    if (result == -1)   //don't have enough info for 169
        cout << "Test 4: pass\n";
    else
        cout << "Test 4: fail\n";
    
    // tests for genPrimes function
    genPrimes(primes, numberOfPrimes);
    numberOfPrimes = MAX_NUMBER_OF_PRIMES;
    
    cout << "Test 5: ";
    if (primes[0] == 2)
        cout << "pass\n";
    else
        cout << "fail\n";
    
    cout << "Test 6: ";
    if (primes[7] == 19)
        cout << "pass\n";
    else
        cout << "fail\n";
    
    cout << "Test 7: ";
    if (primes[19] == 71)
        cout << "pass\n";
    else
        cout << "fail\n";
    
    // tests for genPrimeFactors function
    int num_factors = genPrimeFactors(primes, primeFactors, numberOfPrimes, 98);
    cout << "Test 8: ";
    if ((num_factors == 3) && (primeFactors[0] == 2) && (primeFactors[1] == 7)
        && (primeFactors[2] == 7))
        cout << "pass\n";
    else
        cout << "fail\n";
    
    num_factors = genPrimeFactors(primes, primeFactors, numberOfPrimes, 5043);
    cout << "Test 9: ";
    if ((num_factors == 3) && (primeFactors[0] == 3) && (primeFactors[1] == 41)
        && (primeFactors[2] == 41))
        cout << "pass\n";
    else
        cout << "fail\n";
    
    num_factors = genPrimeFactors(primes, primeFactors, numberOfPrimes, 256);
    cout << "Test 10: ";
    if ((num_factors == 8) && (primeFactors[0] == 2) && (primeFactors[6] == 2))
        cout << "pass\n";
    else
        cout << "fail\n";
    
    return 0; //if we reach this point, return 0 to indicate that main ran!
}

