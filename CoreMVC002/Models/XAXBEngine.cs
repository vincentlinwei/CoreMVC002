using System;
using System.Collections.Generic;

public class XAXBEngine
{
    private string secretNumber;
    private List<GuessModel> guesses; // 紀錄每次的猜測內容與結果
    public int TotalGuesses { get; private set; } // 累計猜測次數
    public bool IsGameWon { get; private set; } // 判斷遊戲是否已猜中
    public bool IsGameOver => IsGameWon; // 遊戲結束標誌

    public XAXBEngine()
    {
        ResetGame();
    }

    // 產生不含重複數字的四位隨機數字
    private void GenerateSecretNumber()
    {
        Random random = new Random();
        List<int> digits = new List<int>();

        // 隨機選取四個不重複的數字
        while (digits.Count < 4)
        {
            int nextDigit = random.Next(0, 10);
            if (!digits.Contains(nextDigit))
            {
                digits.Add(nextDigit);
            }
        }

        secretNumber = string.Join("", digits);
    }

    // 重置遊戲
    public void ResetGame()
    {
        GenerateSecretNumber();
        guesses = new List<GuessModel>();
        TotalGuesses = 0;
        IsGameWon = false;
    }

    // 處理玩家的猜測並回傳結果
    public GuessModel ProcessGuess(string guess)
    {
        if (IsGameWon)
        {
            throw new InvalidOperationException("遊戲已結束，請重新開始遊戲。");
        }

        // 猜測邏輯處理 (計算 A 和 B)
        int aCount = 0, bCount = 0;
        for (int i = 0; i < 4; i++)
        {
            if (guess[i] == secretNumber[i])
            {
                aCount++;
            }
            else if (secretNumber.Contains(guess[i]))
            {
                bCount++;
            }
        }

        // 建立猜測的結果模型
        var guessResult = new GuessModel
        {
            Guess = guess,
            ACount = aCount,
            BCount = bCount
        };

        // 添加到猜測記錄
        guesses.Add(guessResult);
        TotalGuesses++;

        // 判斷是否猜中
        if (aCount == 4)
        {
            IsGameWon = true;
        }

        return guessResult;
    }

    // 回傳所有猜測記錄
    public List<GuessModel> GetGuessHistory()
    {
        return guesses;
    }

    // 回傳隱藏的號碼（遊戲結束後顯示）
    public string GetSecretNumber()
    {
        if (IsGameWon)
        {
            return secretNumber;
        }
        else
        {
            return "????"; // 如果尚未猜中，則不顯示號碼
        }
    }
}
