using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

string targetPath = @"C:\Photos\USA_25.05.2022-23.06.2022\";
string originalPath = @"A:\Photos\USA_Denis_25.05.2022-23.06.2022\";

string[] pathArray = Directory.GetFiles(targetPath);
string[] filesArrayTarget = pathArray.Select(path => Path.GetFileName(path)).ToArray();

pathArray = Directory.GetFiles(originalPath);
string[] filesArrayOriginal = pathArray.Select(path => Path.GetFileName(path)).ToArray();

//merging 2 arrays
filesArrayTarget = filesArrayTarget.Concat(filesArrayOriginal).ToArray();

filesArrayTarget = filesArrayTarget.Select(file => file.Remove(file.Length - 4)).ToArray();

//visualizing content of array
Console.WriteLine("Print a content of array:");
filesArrayTarget.ToList().ForEach(file => Console.WriteLine(file.ToString()));

Dictionary<string, int> ocurrences = new Dictionary<string, int>();

int i = 0;
while (i < filesArrayTarget.Length)
{
    if (!ocurrences.ContainsKey(filesArrayTarget[i]))
    {
        ocurrences.Add(filesArrayTarget[i], 0);
    }
    ocurrences[filesArrayTarget[i]]++;
    i++;
}

Console.WriteLine($"Writing the result: ...");

foreach (var pair in ocurrences)
{
    Console.WriteLine($"{pair.Key} - {pair.Value}");
}

// delete all MOV files which are of > 1 reference
var keysToDelete = ocurrences.Where(file => file.Value >= 2)
                            .Select(file => file.Key);

keysToDelete = keysToDelete.Select(file => file + ".MOV").ToArray();

Console.WriteLine($"These keys will be deleted: ...");

foreach (var key in keysToDelete)
{
    if (!File.Exists(originalPath + key) && !File.Exists(targetPath + key)) {
        Console.WriteLine($"{key} file missing!");
    }
    else 
    {
        if (File.Exists(originalPath + key))
        {
            //File.Delete(originalPath + key);
            Console.WriteLine($"{key} file exists in {originalPath}!");
        }
        if (File.Exists(targetPath + key))
        {
            File.Delete(targetPath + key);
            Console.WriteLine($"{key} file deleted in {targetPath}!");
        }
    }
}
Console.WriteLine("Operation finished!");



