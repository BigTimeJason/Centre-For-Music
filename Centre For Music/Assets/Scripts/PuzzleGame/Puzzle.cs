using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public AudioClip blip;
    public AudioClip complete;
    public Texture2D[] images;
    public int blocksPerLine = 4;
    public int shuffleLength = 5;
    public float defaultMoveDuration = .2f;
    public float shuffleMoveDuration = .05f;
    public float scale = 1.5f;
    public int level = 0;

    public GameObject backButton;
    public GameObject nextLevelButton;
    public GameObject drumMachine;
    enum PuzzleState { Solved, Shuffling, InPlay };
    [SerializeField] PuzzleState state;

    Block[,] blocks;
    Block emptyBlock;
    bool blockIsMoving;
    int shuffleMovesRemaining;
    Vector2Int previousShuffleOffset;

    Queue<Block> inputs;

    // Start is called before the first frame update
    void Start()
    {
        level = 0;
        CreatePuzzle(level);
        StartShuffle();
    }

    private void Update()
    {
        
    }

    public void NextLevel()
    {
        DestroyPuzzle();
        CreatePuzzle(level);
        StartShuffle();
    }

    private void DestroyPuzzle()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void CreatePuzzle(int level)
    {
        blocksPerLine = level + 2;
        //Create a 2d array of blocks
        blocks = new Block[blocksPerLine, blocksPerLine];

        //Get textures using ImageSlicer
        Texture2D[,] imageSlices = ImageSlicer.GetSlices(images[level], blocksPerLine);

        //For each block, create a quad starting at -1 * (blocksperline - 1 ) * .5 + (x,y) to create a grid of blocks
        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                GameObject blockObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                blockObject.transform.position = -Vector2.one * (blocksPerLine - 1) * (scale/2) + new Vector2(scale*x, scale*y);
                blockObject.transform.localScale = new Vector3(scale, scale, 1);

                //parent them to this object
                blockObject.transform.parent = transform;

                //Add block script to each one as well as a method to allow the puzzle to interact with it
                Block block = blockObject.AddComponent<Block>();
                block.OnBlockPressed += PlayerMoveBlock;
                block.OnFinishedMoving += OnBlockFinishedMoving;
                block.Init(new Vector2Int(x, y), imageSlices[x,y]);

                //Add block to the array
                blocks[x, y] = block;

                //Set the missing block when shtodauffling
                if (y == 0 && x == blocksPerLine - 1)
                {
                    emptyBlock = block;
                }
            }
        }

        //Resize the camera so it always fits the puzzle
        //Camera.main.orthographicSize = blocksPerLine * .55f;
        inputs = new Queue<Block>();
    }

    void PlayerMoveBlock(Block blockToMove)
    {
        if (state == PuzzleState.InPlay)
        {
            inputs.Enqueue(blockToMove);
            MakeNextPlayerMove();
        }
    }

    //Logic of moving block (swap coords in array as well as their internal coords) and finally animate
    void MoveBlock(Block blockToMove, float duration)
    {
        if ((blockToMove.coord - emptyBlock.coord).sqrMagnitude == 1)
        {
            blocks[blockToMove.coord.x, blockToMove.coord.y] = emptyBlock;
            blocks[emptyBlock.coord.x, emptyBlock.coord.y] = blockToMove;

            Vector2Int targetCoord = emptyBlock.coord;
            emptyBlock.coord = blockToMove.coord;
            blockToMove.coord = targetCoord;

            Vector2 targetPosition = emptyBlock.transform.position;
            emptyBlock.transform.position = blockToMove.transform.position;
            blockToMove.MoveToPosition(targetPosition, duration);
            blockIsMoving = true;
        }
    }

    //Called whenever the player has an input ready to go
    void MakeNextPlayerMove()
    {
        while (inputs.Count > 0 && !blockIsMoving)
        {
            AudioSource.PlayClipAtPoint(blip, new Vector3(0, 0, -10));
            MoveBlock(inputs.Dequeue(), defaultMoveDuration);
        }
    }

    //When a block is finished moving, check the puzzle is solved or if there are more moves in the queue;
    void OnBlockFinishedMoving()
    {
        blockIsMoving = false;
        CheckIfSolved();

        if (state == PuzzleState.InPlay)
        {
            MakeNextPlayerMove();
        }
        else if (state == PuzzleState.Shuffling)
        {
            if (shuffleMovesRemaining > 0)
            {
                MakeNextShuffleMove();
            } else
            {
                state = PuzzleState.InPlay;
            }
        }
    }

    void StartShuffle()
    {
        state = PuzzleState.Shuffling;
        shuffleMovesRemaining = shuffleLength;
        emptyBlock.gameObject.SetActive(false);
        MakeNextShuffleMove();
    }

    void MakeNextShuffleMove()
    {
        //create a list of possible movements and pick a random one
        Vector2Int[] offsets = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        int randomIndex = Random.Range(0, offsets.Length);

        //try moving a square in every direction
        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2Int offset = offsets[(randomIndex + i) % offsets.Length];

            //if the movement was not in the direction it just came from...
            if (offset != previousShuffleOffset * -1)
            {
                Vector2Int moveBlockCoord = emptyBlock.coord + offset;

                //and the move was also legal
                if (moveBlockCoord.x >= 0 && moveBlockCoord.x < blocksPerLine && moveBlockCoord.y >= 0 && moveBlockCoord.y < blocksPerLine)
                {

                    //make the move

                    AudioSource.PlayClipAtPoint(blip, new Vector3(0, 0, -10));
                    MoveBlock(blocks[moveBlockCoord.x, moveBlockCoord.y], shuffleMoveDuration);
                    shuffleMovesRemaining -= 1;
                    previousShuffleOffset = offset;
                    break;
                }
            }
        }
    }

    void CheckIfSolved()
    {
        foreach(Block block in blocks)
        {
            if (!block.IsAtStartingCoord())
            {
                return;
            }
        }

        state = PuzzleState.Solved;
        emptyBlock.gameObject.SetActive(true);
        nextLevelButton.SetActive(true);

        if (level != 2)
        {
            nextLevelButton.GetComponent<NextLevelButton>().Show("NEXT LEVEL");
            level++;
        } else
        {
            AudioSource.PlayClipAtPoint(complete, new Vector3(0, 0, -10));
            backButton.SetActive(false);
            DestroyPuzzle();
            drumMachine.SetActive(true);
            nextLevelButton.GetComponent<NextLevelButton>().Show("COMPLETE!");
            level++;
        }
    }
}
