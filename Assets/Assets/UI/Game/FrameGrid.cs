using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GridLayoutGroup))]
public class FrameGrid : SecondMonoBehaviour
{
    private static FrameGrid _instance;
    public static FrameGrid Instance => _instance;

    [SerializeField] private LevelProfileSO _levelsProfile;
    [SerializeField] private List<Transform> _frames;

    [SerializeField] private Transform _frame;
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;

    [SerializeField] private Transform _candy;
    [SerializeField] private List<int> _candysIndex;

    [SerializeField] private Transform _cake;
    [SerializeField] private int _cakeIndex;

    [SerializeField] private Transform _giftBox;
    [SerializeField] private int _giftBoxIndex;


    public bool _canMove = true;

    protected override void Awake()
    {
        base.Awake();
        if (FrameGrid._instance != null) Debug.LogError("Only 1 Gridd allow to exist");
        FrameGrid._instance = this;

        this.SetWidthHeight();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLevelsProfile();
        this.LoadFrames();
        this.LoadCandy();
        this.LoadCake();
        this.LoadGiftBox();
        this.LoadCharacterIndex();
    }
    private void LoadLevelsProfile()
    {
        if (this._levelsProfile != null) return;
        this._levelsProfile = Resources.Load<LevelProfileSO>("LevelsProfileSO");
        Debug.LogWarning(transform.name + ": LoadLevelsProfile", gameObject);
    }

    private void LoadFrames()
    {
        if (this._frame != null) return;
        this._frame = GameObject.Find("Character").transform.Find("Frame").transform;
        Debug.LogWarning(transform.name + ": LoadCandy", gameObject);
    }
    
    private void LoadCandy()
    {
        if (this._candy != null) return;
        this._candy = GameObject.Find("Character").transform.Find("Candy").transform;
        Debug.LogWarning(transform.name + ": LoadCandy", gameObject);
    }
    private void LoadCake()
    {
        if (this._cake != null) return;
        this._cake = GameObject.Find("Character").transform.Find("Cake").transform;
        Debug.LogWarning(transform.name + ": LoadCake", gameObject);
    }
    private void LoadGiftBox()
    {
        if (this._giftBox != null) return;
        this._giftBox = GameObject.Find("Character").transform.Find("GiftBox").transform;
        Debug.LogWarning(transform.name + ": LoadGiftBox", gameObject);
    }

    private void LoadCharacterIndex()
    {
        foreach(LevelInfo levelInfo in this._levelsProfile.levelInfos)
        {
            if(levelInfo.levelName == SceneManager.GetActiveScene().name)
            {
                GridLayoutGroup gridLayoutGroup = transform.GetComponent<GridLayoutGroup>();
                if (gridLayoutGroup != null) gridLayoutGroup.cellSize = levelInfo.cellSize;
                this._rows = levelInfo.rows;
                this._columns = levelInfo.columns;
                this._cakeIndex = levelInfo.cakeIndex;
                this._giftBoxIndex = levelInfo.giftBoxIndex;
                this._candysIndex = levelInfo.candysIndex;
            }    
        }    
    }

    private void SetWidthHeight()
    {
        RectTransform rectTransform = transform.GetComponent<RectTransform>();
        foreach(LevelInfo levelInfo in this._levelsProfile.levelInfos)
        {
            if(levelInfo.levelName == SceneManager.GetActiveScene().name)
            {
                Vector2 WidhtHeight = new Vector2(levelInfo.cellSize.x * levelInfo.rows, levelInfo.cellSize.y * levelInfo.columns);
                rectTransform.sizeDelta = WidhtHeight;
            }    
        }    
    }    

    protected override void Start()
    {
        base.Start();
        this.SpawnFrame();
        this.SpawnCandy();
        this.SpawnCake();
        this.SpawnGiftBox();
    }
    private void SpawnFrame()
    {
        for (int i = 0; i < this._rows * this._columns; i++)
        {
            Transform transform = Instantiate(_frame);
            transform.SetParent(this.transform);
            transform.localScale = new Vector3(1f, 1f, 1f);
            this._frames.Add(transform);
        }    
    }

    private void SpawnCandy()
    {
        foreach(int candyIndex in this._candysIndex)
        {
            Transform transform = Instantiate(_candy);
            transform.SetParent(this._frames[candyIndex]);
            transform.position = transform.parent.position;
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }    
    }

    private void SpawnCake()
    {
        Transform transform = Instantiate(_cake);
        transform.SetParent(this._frames[_cakeIndex]);
        transform.position = transform.parent.position;
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    private void SpawnGiftBox()
    {
        Transform transform = Instantiate(_giftBox);
        transform.SetParent(this._frames[_giftBoxIndex]);
        transform.position = transform.parent.position;
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

    private void Update()
    {
        if (InputManager.Instance.isMoveUp && _canMove && !UIFailed.Instance._isOpen && !UICompleted.Instance._isOpen)
        {
            MoveUp();
            InputManager.Instance.isMoveUp = false;
        }
        if (InputManager.Instance.isMoveDown && _canMove && !UIFailed.Instance._isOpen && !UICompleted.Instance._isOpen)
        {
            MoveDown();
            InputManager.Instance.isMoveDown = false;
        }
        if (InputManager.Instance.isMoveLeft && _canMove && !UIFailed.Instance._isOpen && !UICompleted.Instance._isOpen)
        {
            MoveLeft();
            InputManager.Instance.isMoveLeft = false;
        }
        if (InputManager.Instance.isMoveRight && _canMove && !UIFailed.Instance._isOpen && !UICompleted.Instance._isOpen )
        {
            MoveRight();
            InputManager.Instance.isMoveRight = false;
        }
    }

    public void MoveUp()
    {
        foreach (Transform frame in this._frames)
        {
            Movement movement = frame.GetComponentInChildren<Movement>();
            if (movement != null)
            {
                int index = this._frames.IndexOf(frame);    
                FindUpPosition(index, movement);
                movement.MoveUp();
            }
        }
    }
    public void MoveLeft()
    {
        foreach (Transform frame in this._frames)
        {
            Movement movement = frame.GetComponentInChildren<Movement>();
            if (movement != null)
            {
                int index = this._frames.IndexOf(frame);
                FindLeftPosition(index, movement);
                movement.MoveLeft();
            }
        }
    }

    public void MoveDown()
    {
        for (int i = this._frames.Count - 1; i >= 0; i--)
        {
            Transform frame = this._frames[i];
            Movement movement = frame.GetComponentInChildren<Movement>();
            if (movement != null)
            {
                int index = i;
                FindDownPosition(index, movement);
                movement.MoveDown();

            }
        }
    }
    
    public void MoveRight()
    {
        for (int i = this._frames.Count - 1; i >= 0; i--)
        {
            Transform frame = this._frames[i];
            Movement movement = frame.GetComponentInChildren<Movement>();
            if (movement != null)
            {
                int index = i;
                FindRightPosition(index, movement);
                movement.MoveRight();
            }
        }
    }

    public void FindUpPosition(int index, Movement movement)
    {
        int currentIndex = index;
        int upIndex = UpIndex(currentIndex);
        if (upIndex < 0 || upIndex > this._frames.Count - 1 || IsHaveCandy(this._frames[upIndex].transform) || IsHaveGiftBox(this._frames[upIndex].transform))
        {
            movement.UpPos = this._frames[currentIndex].transform;
            movement.transform.SetParent(this._frames[currentIndex].transform);
            return;
        }
        else if (IsHaveCake(this._frames[upIndex].transform))
        {
            movement.UpPos = this._frames[upIndex].transform;
            movement.transform.SetParent(this._frames[upIndex].transform);
        }    
        else
        {
            this.FindUpPosition(upIndex, movement);
        }
    }

    public void FindDownPosition(int index, Movement movement)
    {
        int currentIndex = index;
        int downIndex = DownIndex(currentIndex);
        if (downIndex < 0 || downIndex > this._frames.Count - 1 || IsHaveCandy(this._frames[downIndex].transform) || IsHaveCake(this._frames[downIndex].transform))
        {
            movement.DownPos = this._frames[currentIndex].transform;
            movement.transform.SetParent(this._frames[currentIndex].transform);
            return;
        }
        else if (IsHaveGiftBox(this._frames[downIndex].transform))
        {
            movement.DownPos = this._frames[downIndex].transform;
        }
        else
        {
            this.FindDownPosition(downIndex, movement);
        }
    }

    public void FindLeftPosition(int index, Movement movement)
    {
        int currentIndex = index;
        int leftIndex = LeftIndex(currentIndex);
        if (leftIndex < 0 || leftIndex > this._frames.Count - 1 || IsHaveCandy(this._frames[leftIndex].transform)
            || IsHaveGiftBox(this._frames[leftIndex].transform) || IsHaveCake(this._frames[leftIndex].transform))
        {
            movement.LeftPos = this._frames[currentIndex].transform;
            movement.transform.SetParent(this._frames[currentIndex].transform);
            return;
        }
        else
        {
            this.FindLeftPosition(leftIndex, movement);
        }
    }

    private void FindRightPosition(int index, Movement movement)
    {
        int currentIndex = index;
        int rightIndex = RightIndex(currentIndex);
        if (rightIndex < 0 || rightIndex > this._frames.Count - 1 || IsHaveCandy(this._frames[rightIndex].transform)
            || IsHaveGiftBox(this._frames[rightIndex].transform) || IsHaveCake(this._frames[rightIndex].transform))
        {
            movement.RightPos = this._frames[currentIndex].transform;
            movement.transform.SetParent(this._frames[currentIndex].transform);
            return;
        }
        else
        {
            this.FindRightPosition(rightIndex, movement);
        }
    }

    private int UpIndex(int currentIndex)
    {
        int currentRow = currentIndex / this._columns;
        int currentcolumn = currentIndex % this._columns;
        int upRow = currentRow - 1;
        int upColumn = currentcolumn;
        if (upRow < 0 || upRow > _rows - 1 || upColumn < 0 || upColumn > _columns - 1) return -1;
        int upIndex = upRow * 3 + upColumn;
        return upIndex;
    }

    private int DownIndex(int currentIndex)
    {
        int currentRow = currentIndex / this._columns;
        int currentcolumn = currentIndex % this._columns;
        int downRow = currentRow + 1;
        int downColumn = currentcolumn;
        if (downRow < 0 || downRow > _rows - 1 || downColumn < 0 || downColumn > _columns - 1) return -1;
        int downIndex = downRow * 3 + downColumn;
        return downIndex;
    }

    private int LeftIndex(int currentIndex)
    {
        int currentRow = currentIndex / this._columns;
        int currentcolumn = currentIndex % this._columns;
        int leftRow = currentRow;
        int leftColumn = currentcolumn - 1;
        if (leftRow < 0 || leftRow > _rows - 1 || leftColumn < 0 || leftColumn > _columns - 1) return -1;
        int leftIndex = leftRow * 3 + leftColumn;
        return leftIndex;
    }

    private int RightIndex(int currentIndex)
    {
        int currentRow = currentIndex / this._columns;
        int currentcolumn = currentIndex % this._columns;
        int rightRow = currentRow;
        int rightColumn = currentcolumn + 1;
        if (rightRow < 0 || rightRow > _rows - 1 || rightColumn < 0 || rightColumn > _columns - 1) return -1;
        int rightIndex = rightRow * 3 + rightColumn;
        return rightIndex;
    }

    private bool IsHaveCandy(Transform transform)
    {
        Candy candy = transform.GetComponentInChildren<Candy>();
        if (candy == null) return false;
        return true;
    }

    private bool IsHaveGiftBox(Transform transform)
    {
        GiftBox giftBox = transform.GetComponentInChildren<GiftBox>();
        if (giftBox == null) return false;
        return true;
    }

    private bool IsHaveCake(Transform transform)
    {
        Cake cake = transform.GetComponentInChildren<Cake>();
        if (cake == null) return false;
        return true;
    }

    public bool WinCheck()
    {
        foreach (Transform frame in this._frames)
        {
            Cake cakeCtrl = frame.GetComponentInChildren<Cake>();
            GiftBox giftBox = frame.GetComponentInChildren<GiftBox>();
            if (cakeCtrl && giftBox)
            {
                TimeCount.Instance.IsWin = true;
                UICompleted.Instance.Open();
            }
        }
        return false;
    }
}
