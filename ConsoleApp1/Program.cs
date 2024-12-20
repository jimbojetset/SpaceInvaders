using System.Text.RegularExpressions;

string inputFilePath = "inputCode.cs"; // File with switch cases and function definitions
string outputFilePath = "outputCode.cs"; // File to save the refactored code

string code = File.ReadAllText(inputFilePath);

// Extract function definitions
var functionRegex = new Regex(@"private void (OP_[0-9A-Fa-f]+)\(\)\s*\{([\s\S]*?)\}");
var functions = functionRegex.Matches(code);

// Create a dictionary of function logic
var functionBodies = new Dictionary<string, string>();
foreach (Match match in functions)
{
    functionBodies[match.Groups[1].Value] = match.Groups[2].Value.Trim();
}

// Replace function calls in the switch cases with inline logic
var switchRegex = new Regex(@"case (0x[0-9A-Fa-f]+):\s*(OP_[0-9A-Fa-f]+)\(\);\s*return;");
var refactoredCode = switchRegex.Replace(code, match =>
{
    string opcode = match.Groups[1].Value;
    string functionName = match.Groups[2].Value;

    if (functionBodies.ContainsKey(functionName))
    {
        return $"case {opcode}:\n{functionBodies[functionName]}\nreturn;";
    }
    return match.Value; // Keep unchanged if no matching function body
});

File.WriteAllText(outputFilePath, refactoredCode);
Console.WriteLine("Refactoring complete. Refactored code saved to: " + outputFilePath);
