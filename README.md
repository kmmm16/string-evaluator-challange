## Problem description

> Write a C# console application that evaluates a string expression consisting of non-negative integers and the + - / * operators only, taking into account the normal mathematical rules of operator precedence. Support for parenthesis is not required. The application can use an existing, well known algorithms to solve the problem, but should not use any code fragments from other solutions. The application should use English language for communication with the user, as well as the internal naming and comments.

> Expression examples and expected results:
>* an input string of "4+5*2" should output 14 as a result
>* an input string of "4+5/2" should output 6.5 as a result
>* an input string of "4+5/2-1" should output 5.5 as a result 

### Requirements stemming from a problem description
User stories:
- As a user I want to be able to put string expression consisting of non-negative integers and the + - / * operators
- As a user I want to get correct output result from application when input is valid
- As a user I want to get information if my input expression was not valid
- As a user I want to quit the program if my input is empty

Missing requirements:
- ~~Support for parenthesis is not required~~, because [YAGNI rule](https://en.wikipedia.org/wiki/You_aren%27t_gonna_need_it)
- ~~Filtering whitespaces from input~~, because it was not mentioned, so be careful what you put in your console

## Technology stack
Language: **C#**  
Framework: **.NET Core**  
Teting framework: **NUnit**  
IDE: **Visual Studio Code**  
Additional NuGet libraries: **Microsoft.Extensions.DependencyInjection**  
Used software development approaches: **TDD**, **SOLID**, **DI**, **IoC**

## Source code overview
Repository contains one .sln (*StringEvaluation.sln*) file and three projects.   
### Projects
For each project in solution we got subdirectory with sufix for better orientation, e.g. *StringEvaluation.UI*.  
Short projects overview (more details in code):
- StringEvaluation.UI - project including user interface, done as console application
- StringEvaluation.Tests - project including tests of *StringEvaluation.Logic* classes
- StringEvaluation.Logic - library project including logic for solution of problem

## Problem solution overview (implementation details)
Heart of my solution is placed in *StringEvaluation.Logic* project in class named *StringEvaluator*.  
Algorithm for that is 100% constructed by me and I didn't make any research to check if it is a known and named algorithm.  

### Short algorithm description
Basicly, the algorithm checks if string is valid with dependency class called *StringEvaluationValidator*.  
When string is valid it enables calculating value from string, if not, it returns a communicate about that.  

Next, the algorithm is spliting input string into two lists:
- List of numbers from input string
- List of operators from input string

With those, program starts the output calculation.  
First of all, we are searching for two numbers between '*' and '/' operators (math rule about order of operations!) and then we calculate them.  
After that, we are removing that operator from the list and also, we are removing numbers included in operation from second list.  
Then, we are placing the result of operation in the index of the frist number taken from the list.  

We are doing the same operation for the numbers between '-' and '+' operator.  

The calculation stops when we got 0 elements in the list of operators.  
You can find the whole process with implementation details in the code in the mentioned class.

### Extra solution (just for comparing)
As an extra, I placed a solution founded on stack overflow site.
It is just an implemented solution from *System* namespace, and you should treat it just like a bonus :sunglasses:.  
It is placed in *StringEvaluation.Logic* in *Implementation* folder as *SODDStringEvaluator* class.

## Important information
The repository is missing from the original commit history because of many changes of concept that I had along the way, and of some work commits which were not well described.  
Finally, it was squashed to one commit, sorry.
