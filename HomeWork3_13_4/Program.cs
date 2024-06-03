static void MethodToCall(object arg) {
    var argAsInt = (int)arg;
    Console.WriteLine($"Arg received: {argAsInt}");

    if (argAsInt < 10) {
        var thread = new Thread(MethodToCall);
        thread.Start(argAsInt + 1);
    }
}


MethodToCall(0);
Console.ReadLine();