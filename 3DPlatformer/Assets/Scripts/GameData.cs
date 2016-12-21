using System.ComponentModel;

public class GameData
{

    private static GameData instance;

    public delegate void LivesChangedHandler(int lives, int old);
    public delegate void ScoreChangedHandler(int score, int old);
    public delegate void PausedChangedHandler(bool paused);

    private int _score;
    private int _lives;
    private bool _paused;

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
            if (ScoreChanged != null)
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
            if (LivesChanged != null)
                LivesChanged(_lives, old);
        }
    }

    public bool Paused
    {
        get
        {
            return _paused;
        }
        set
        {
            _paused = value;
            if (PausedChanged != null)
                PausedChanged(_paused);
        }
    }

    public event LivesChangedHandler LivesChanged;
    public event ScoreChangedHandler ScoreChanged;
    public event PausedChangedHandler PausedChanged;

    public GameData()
    {
        Score = 0;
        Lives = 3;
        Paused = false;
    }

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
