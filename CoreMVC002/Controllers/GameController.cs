using Microsoft.AspNetCore.Mvc;

public class GameController : Controller
{
    private static XAXBEngine _gameEngine = new XAXBEngine();

    // 顯示遊戲頁面
    public IActionResult Index()
    {
        ViewBag.GuessHistory = _gameEngine.GetGuessHistory();
        ViewBag.TotalGuesses = _gameEngine.TotalGuesses;
        ViewBag.IsGameWon = _gameEngine.IsGameWon;
        ViewBag.SecretNumber = _gameEngine.GetSecretNumber();
        return View();
    }

    // 提交猜測
    [HttpPost]
    public IActionResult Guess(string userGuess)
    {
        if (string.IsNullOrEmpty(userGuess) || userGuess.Length != 4)
        {
            ViewBag.ErrorMessage = "請輸入四位數字。";
        }
        else
        {
            var guessResult = _gameEngine.ProcessGuess(userGuess);
            ViewBag.GuessResult = guessResult;
        }

        return RedirectToAction("Index");
    }

    // 重新開始遊戲
    public IActionResult Restart()
    {
        _gameEngine.ResetGame();
        return RedirectToAction("Index");
    }
}
