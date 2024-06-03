using HomeWork3_13_1;

namespace HomeWork3_13_2;

public class Manager {

    public readonly int RequiredWordsCount;
    private int actualWordsCount;
    private object lockActualWords = new object();

    private Random rndWord = new Random();
    private Random rndX = new Random();
    private Random rndY = new Random();

    private string[] generatedWords = new string[] {
        "policy", "activity", "impression", "introduction",
        "setting","power","paper","analysis","safety","death"
        ,"cousin","government","member","farmer","poem"
        ,"property","candidate","airport","pollution"
        ,"girl","election","length","song","concept","disease"
        ,"storage","opinion","user","initiative","problem","employment","theoryv"
        ,"county","category","measurement","basis","committee","hearing","refrigerator"
        ,"baseball","agreement","requirement","failure","distribution","thing",
        "hat","investment","selection","freedom","nation"
    };

    const int CONSOLE_WIDTH = 120;
    const int CONSOLE_HEIGHT = 30;

    public Manager(int wordsCount) {
        RequiredWordsCount = wordsCount;
        actualWordsCount = 0;
        Console.SetWindowSize(CONSOLE_WIDTH, CONSOLE_HEIGHT);
        Console.Clear();
        Console.CursorVisible = false;

        MatrixWord.OnComplete += MatrixWord_OnComplete;
    }

    private void MatrixWord_OnComplete(MatrixWord word) {
        lock (lockActualWords) {
            actualWordsCount--;
        }

        AddNewMatrixWordThread();
    }

    public void Start() {
        for (int i = 0; i < RequiredWordsCount; i++) {
            AddNewMatrixWordThread();
        }
    }

    private void AddNewMatrixWordThread() {
        lock (lockActualWords) {
            actualWordsCount++;
        }

        var wordIndex = rndWord.Next(0, generatedWords.Length);
        var word = generatedWords[wordIndex];
        var wordCoordX = rndX.Next(0, CONSOLE_WIDTH);
        var wordCoordY = rndY.Next(0, CONSOLE_HEIGHT + 5);
        var matrixWord = new MatrixWord(word, wordCoordX, wordCoordY, CONSOLE_HEIGHT);

        var thread = new Thread(matrixWord.Display);
        thread.Start();
    }

}