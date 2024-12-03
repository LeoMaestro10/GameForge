using GameForge.Models;
using GameForge.Models.GameForge.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GameForge.Controllers
{
    public class WalletController : Controller
    {
        // Display the user's wallet balance
        public IActionResult Index()
        {
            var userId = 1;  // Example: Replace with the actual logged-in user's ID

            // Fetch the user's wallet
            var wallet = DataStore.Wallets.FirstOrDefault(w => w.UserId == userId);

            if (wallet == null)
            {
                // If no wallet exists for the user, create a new one
                wallet = new Wallet { UserId = userId, Balance = 0 };
                DataStore.Wallets.Add(wallet);  // Add the wallet to the in-memory store
            }

            return View(wallet);  // Display the wallet balance
        }

        // Display the top-up form where the user enters the gift card code
        public IActionResult TopUp()
        {
            return View();  // Show form for gift card code input
        }

        // Handle the top-up when the user submits a gift card code
        [HttpPost]
        public IActionResult TopUp(string giftCardCode)
        {
            var userId = 1;  // Example: Replace with the actual logged-in user's ID

            // Look for the gift card in the in-memory data store
            var giftCard = DataStore.GiftCards.FirstOrDefault(g => g.Code == giftCardCode);

            if (giftCard == null)
            {
                TempData["Error"] = "Invalid gift card code.";
                return View();  // Re-display the top-up form with error message
            }

            // Find the user's wallet
            var wallet = DataStore.Wallets.FirstOrDefault(w => w.UserId == userId);

            if (wallet == null)
            {
                TempData["Error"] = "Wallet not found.";
                return View();  // Re-display the top-up form with error message
            }

            // Add the gift card amount to the user's wallet balance
            wallet.Balance += giftCard.Amount;

            TempData["Message"] = "Wallet topped up successfully!";
            return RedirectToAction("Index");  // Redirect to the wallet page to show the updated balance
        }
    }
}
