using System.Collections.Generic;

namespace Interpreter
{

    public class Operator
    {
        static Operator()
        {
            DefineOperator("=", 0);
            DefineOperator("+", 10);
            DefineOperator("-", 10);
            DefineOperator("*", 11);
            DefineOperator("/", 11);
            DefineOperator("negate", 50);
        }

        public string Name { get; private set; }
        public int Precedence { get; private set; }

        Operator(string token, int precedence)
        {
            Name = token;
            Precedence = precedence;
        }

        static readonly Dictionary<string, Operator> Operators = new Dictionary<string, Operator>();

        public static void DefineOperator(string token, int precedence)
        {
            Operators[token] = new Operator(token, precedence);
        }

        public static Operator Lookup(string token)
        {
            return Operators[token];
        }

        public static bool IsOperator(string token)
        {
            return Operators.ContainsKey(token);
        }
    }
}
