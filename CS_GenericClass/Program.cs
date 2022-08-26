using CS_GenericClass;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Demo Generic");

GenericStack<int> intStack = new GenericStack<int>();

intStack.Push(10);
intStack.Push(20);
intStack.Push(30);
intStack.Push(40);
intStack.Push(50);

var intgers = intStack.Display();

foreach (var item in intgers)
{
    Console.WriteLine(item);
}

Console.WriteLine();

GenericStack<string> strStack = new GenericStack<string>();
strStack.Push("A");
strStack.Push("B");
strStack.Push("C");
strStack.Push("D");
strStack.Push("E");

var strings = strStack.Display();

foreach (var item in strings)
{
    Console.WriteLine(item);
}

Console.WriteLine();

GenericStack<Emoployee> empStack = new GenericStack<Emoployee>();

empStack.Push(new Emoployee() {EmpNo=1,EmpName="A" });
empStack.Push(new Emoployee() { EmpNo = 2, EmpName = "B" });
empStack.Push(new Emoployee() { EmpNo = 3, EmpName = "C" });
empStack.Push(new Emoployee() { EmpNo = 4, EmpName = "D" });
empStack.Push(new Emoployee() { EmpNo = 5, EmpName = "E" });


var employees = empStack.Display();

foreach (var item in employees)
{
    Console.WriteLine($"{item.EmpNo} {item.EmpName}");
}


Console.ReadLine();



public class Emoployee
{
    public int EmpNo { get; set; }
    public string EmpName { get; set; }
}