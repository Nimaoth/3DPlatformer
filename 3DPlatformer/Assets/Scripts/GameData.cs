using System.ComponentModel;

public class GameData
{

    private static GameData instance;

    public delegate void LivesChangedHandler(int lives, int old);
    public delegate void ScoreChangedHandler(int score, int old);

    private int _score;
    private int _lives;

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            int old = _score;
            _score = value;
            ScoreChanged(_score, old);
        }
    }

    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            int old = _lives;
            _lives = value;
            LivesChanged(_lives, old);
        }
    }

    public event LivesChangedHandler LivesChanged;
    public event ScoreChangedHandler ScoreChanged;

    public static GameData Instance
    {
        get
        {
            if (instance == null)
                instance = new GameData();
            return instance;
        }
    }

}
