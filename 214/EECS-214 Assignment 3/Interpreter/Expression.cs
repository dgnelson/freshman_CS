using System;
using System.Text;
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local

namespace Interpreter
{
    public abstract partial class Expression
    {
        public Expression[] Subexpressions { get; protected set; }
        public abstract string Label { get; }

        public virtual void WriteScheme(StringBuilder b)
        {
            b.Append("(");
            b.Append(Label);
            foreach (var child in Subexpressions)
            {
                b.Append(" ");
                child.WriteScheme(b);
            }
            b.Append(")");
        }

        public override string ToString()
        {
            var b = new StringBuilder();
            WriteScheme(b);
            return b.ToString();
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public abstract object Run(Dictionary dict);
    }

    /// <summary>
    /// An Expression with no children, e.g. a constant or variable reference.
    /// </summary>
    public abstract class ExpressionLeaf : Expression
    {
        static readonly Expression[] NoChildren = new Expression[0];
        protected ExpressionLeaf()
        {
            Subexpressions = NoChildren;
        }

        public override void WriteScheme(StringBuilder b)
        {
            b.Append(Label);
        }
    }

    public class Constant : ExpressionLeaf
    {
        public object Value { get; private set; }
        public Constant(object constantValue)
        {
            Value = constantValue;
        }

        public override string Label
        {
            get { return Value.ToString(); }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            return Value;
        }
    }

    public class VariableReference : ExpressionLeaf
    {
        public string VariableName { get; private set; }

        public VariableReference(string variableName)
        {
            VariableName = variableName;
        }

        public override string Label
        {
            get { return VariableName; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            return dict.Lookup(VariableName);
        }
    }

    public class VariableAssignment : ExpressionLeaf
    {
        public string VariableName { get; private set; }
        public Expression ValueExpression { get; private set; }

        public VariableAssignment(string variableName, Expression value)
        {
            VariableName = variableName;
            ValueExpression = value;
            Subexpressions = new[] { value };
        }

        public override void WriteScheme(StringBuilder b)
        {
            b.Append("(set! ");
            b.Append(VariableName);
            b.Append(" ");
            ValueExpression.WriteScheme(b);
            b.Append(")");
        }

        public override string Label
        {
            get { return "set!"; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            object o = ValueExpression.Run(dict);
            dict.SetMemberValue(VariableName, o);
            return o;
        }
    }

    public class MemberReference : Expression
    {
        public Expression ObjectExpression { get; private set; }
        public string MemberName { get; private set; }

        public MemberReference(Expression oExpression, string member)
        {
            ObjectExpression = oExpression;
            MemberName = member;
            Subexpressions = new[] { oExpression };
        }

        public override void WriteScheme(StringBuilder b)
        {
            ObjectExpression.WriteScheme(b);
            b.Append(".");
            b.Append(MemberName);
        }

        public override string Label
        {
            get { return "member"; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            object o = ObjectExpression.Run(dict);
            return o.GetMemberValue(MemberName);
        }
    }

    public class MemberAssignment : Expression
    {
        public Expression ObjectExpression { get; private set; }
        public string MemberName { get; private set; }
        public Expression ValueExpression { get; private set; }

        public MemberAssignment(Expression oExpression, string member, Expression value)
        {
            ObjectExpression = oExpression;
            MemberName = member;
            ValueExpression = value;
            Subexpressions = new[] { oExpression, value };
        }

        public override void WriteScheme(StringBuilder b)
        {
            b.Append("(set-member! ");
            ObjectExpression.WriteScheme(b);
            b.Append(".");
            b.Append(MemberName);
            b.Append(" ");
            ValueExpression.WriteScheme(b);
            b.Append(")");
        }

        public override string Label
        {
            get { return "member"; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            object o = ObjectExpression.Run(dict);
            object value = ValueExpression.Run(dict);
            o.SetMemberValue(MemberName, value);
            return value;
        }
    }

    public class MethodCall : Expression
    {
        public Expression ObjectExpression { get; private set; }
        public string MethodName { get; private set; }
        public Expression[] Arguments { get; private set; }

        public MethodCall(Expression oExpression, string method, params Expression[] args)
        {
            ObjectExpression = oExpression;
            MethodName = method;
            Arguments = args;
            var children = new Expression[args.Length + 1];
            children[0] = oExpression;
            args.CopyTo(children, 1);
            Subexpressions = children;
        }

        public override void WriteScheme(StringBuilder b)
        {
            b.Append("(");
            ObjectExpression.WriteScheme(b);
            b.Append(".");
            b.Append(MethodName);
            foreach (var arg in Arguments)
            {
                b.Append(" ");
                arg.WriteScheme(b);
            }
            b.Append(")");

        }

        public override string Label
        {
            get { return "call"; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            object o = ObjectExpression.Run(dict);
            object[] arguments = new object[Arguments.Length];
            for (int i = 0; i < arguments.Length; i++) 
                arguments[i] = Arguments[i].Run(dict);
            object o2 = o.CallMethod(MethodName, arguments);
            return o2;

        }
    }

    public class OperatorExpression : Expression
    {
        /// <summary>
        /// The operator, but this can't be called "operator" because that's a C# keyword.
        /// </summary>
        readonly Operator operation;

        public OperatorExpression(Operator op, params Expression[] args)
        {
            operation = op;
            Subexpressions = args;
        }

        public override string Label
        {
            get { return operation.Name; }
        }

        /// <summary>
        /// Runs the expression and returns its value
        /// </summary>
        public override object Run(Dictionary dict)
        {
            object[] arguments = new object[Subexpressions.Length];
            for (int i = 0; i < arguments.Length; i++)
                arguments[i] = Subexpressions[i].Run(dict);
            return Interpreter.GenericOperator(Label, arguments);
        }
    }
}
