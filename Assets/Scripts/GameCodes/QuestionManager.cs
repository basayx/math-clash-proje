using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public Question GetAnQuestion(string typeName)
    {
        Question questionObj = new Question();
        string question = "";
        int answer = 0;

        string[] types = typeName.Split(' ');
        for(int i = 0; i < types.Length; i++)
        {
            if (types[i] == "toplama")
                types[i] = "+";
            else if (types[i] == "cikartma")
                types[i] = "-";
            else if (types[i] == "carpma")
                types[i] = "*";
            else if (types[i] == "bolme")
                types[i] = "/";
        }
        int beforeBracketCounter = 0;
        int targetNumCount = types.Length + 1;

        if (targetNumCount > 2)
        {
            beforeBracketCounter++;
            question += "(";
        }

        int firstNumber = UnityEngine.Random.Range(1, 11);
        question += firstNumber.ToString();
        targetNumCount--;

        foreach (string type in types)
        {
            int number = 0;

            beforeBracketCounter++;
            question += " " + type + " ";
            if (beforeBracketCounter == 1 && targetNumCount > 1)
            {
                question += "(";
            }

            switch (type)
            {
                case "+":
                    number = UnityEngine.Random.Range(0, 31);
                    question += number.ToString();
                    break;

                case "-":
                    number = UnityEngine.Random.Range(0, 31);
                    question += number.ToString();
                    break;

                case "*":
                    number = UnityEngine.Random.Range(0, 7);
                    question += number.ToString();
                    break;

                case "/":
                    number = UnityEngine.Random.Range(1, 5);
                    question += number.ToString();
                    break;
            }

            if(beforeBracketCounter >= 2)
            {
                question += ")";
                beforeBracketCounter = 0;
            }

            targetNumCount--;
        }

        questionObj.answer = (int)(new Calc().Solve(question));

        question = question.Replace('*', 'x');
        questionObj.question = question.ToString() + " = ?";

        return questionObj;
    }
}
public class Calc
{
    public double Solve(string equation)
    {
        // Remove all spaces
        equation = Regex.Replace(equation, @"\s+", "");

        Operation operation = new Operation();
        operation.Parse(equation);

        double result = operation.Solve();

        return result;
    }
}

public class Operation
{
    public Operation LeftNumber { get; set; }
    public string Operator { get; set; }
    public Operation RightNumber { get; set; }

    private Regex additionSubtraction = new Regex("[+-]", RegexOptions.RightToLeft);
    private Regex multiplicationDivision = new Regex("[*/]", RegexOptions.RightToLeft);

    public void Parse(string equation)
    {
        var operatorLocation = additionSubtraction.Match(equation);
        if (!operatorLocation.Success)
        {
            operatorLocation = multiplicationDivision.Match(equation);
        }

        if (operatorLocation.Success)
        {
            Operator = operatorLocation.Value;

            LeftNumber = new Operation();
            LeftNumber.Parse(equation.Substring(0, operatorLocation.Index));

            RightNumber = new Operation();
            RightNumber.Parse(equation.Substring(operatorLocation.Index + 1));
        }
        else
        {
            Operator = "v";
            result = double.Parse(equation);
        }
    }

    private double result;

    public double Solve()
    {
        switch (Operator)
        {
            case "v":
                break;
            case "+":
                result = LeftNumber.Solve() + RightNumber.Solve();
                break;
            case "-":
                result = LeftNumber.Solve() - RightNumber.Solve();
                break;
            case "*":
                result = LeftNumber.Solve() * RightNumber.Solve();
                break;
            case "/":
                result = LeftNumber.Solve() / RightNumber.Solve();
                break;
            default:
                throw new Exception("Call Parse first.");
        }

        return result;
    }
}
