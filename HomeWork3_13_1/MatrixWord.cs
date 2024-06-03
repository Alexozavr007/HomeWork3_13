namespace HomeWork3_13_1; 
public class MatrixWord {
    public delegate void MatrixWordParamDelegate(MatrixWord word);
    public static event MatrixWordParamDelegate OnComplete;

    private static object consoleLock = new object();
    private Random rnd = new Random();

    public string Word {  get; set; }
    private int StartY {  get; set; }
    private int StartX { get; set; }
    private int LimitY { get; set; }

    public MatrixWord(string word, int x, int y, int limitY) { 
        this.Word = word;
        this.StartX = x;
        this.StartY = y;
        this.LimitY = limitY;
    }

    public void Display() {
        for (int letterIdx = 0; letterIdx < Word.Length; letterIdx++) {
            var targetLetter = Word[letterIdx];
            var targetX = StartX;
            var targetY = StartY + letterIdx;

            if (targetY >= (LimitY - 1))
                break;

            for (int j = 0; j < 10; j++) {
                var randomASCIIcharNumber = rnd.Next(65, 90);
                var randomASCIIchar = (char)randomASCIIcharNumber;

                Thread.Sleep(1);

                lock (consoleLock) {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(targetX, targetY);
                    Console.Write(randomASCIIchar);
                   
                }
            }

            lock (consoleLock) {


                if (letterIdx != Word.Length) {
                    Console.ForegroundColor = ConsoleColor.Green;
                } else if (letterIdx == 1) { 
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }
                Console.SetCursorPosition(targetX, targetY);
                Console.Write(targetLetter);
            
                if (letterIdx > 0) {
                    Console.SetCursorPosition(targetX, targetY - 1);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(Word[letterIdx - 1]);
                }
            }

        }

        Thread.Sleep(500);

        
        for (int letterIdx = 0; letterIdx < Word.Length; letterIdx++) {
            var targetY = StartY + letterIdx;
            if (targetY >= (LimitY - 1))
                break;

            lock (consoleLock) {
                Console.SetCursorPosition(StartX, targetY);
                Console.Write(' ');
            }

            Thread.Sleep(50);
        }   

        if (OnComplete != null) {
            OnComplete(this);
        }
    }

}
