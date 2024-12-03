using Microsoft.AspNetCore.Mvc;
using GameForge.Models;
using System.Linq;

namespace GameForge.Controllers
{
    public class SubscriptionController : Controller
    {
        public IActionResult Index()
        {
            var plans = DataStore.SubscriptionPlans;
            return View(plans);
        }

        public IActionResult Checkout(int id)
        {
            var plan = DataStore.SubscriptionPlans.FirstOrDefault(p => p.Id == id);
            if (plan == null) return NotFound();

            return View(plan);
        }

        [HttpPost]
        public IActionResult ProcessPayment(int id, string paymentMethod, string? couponCode)
        {
            var plan = DataStore.SubscriptionPlans.FirstOrDefault(p => p.Id == id);
            if (plan == null) return NotFound();

            var userWallet = DataStore.Wallets.FirstOrDefault(w => w.UserId == 1); // Assume UserId = 1

            if (paymentMethod == "Wallet")
            {
                if (userWallet != null && userWallet.Balance >= plan.Price)
                {
                    userWallet.Balance -= plan.Price;
                    AddUserSubscription(1, plan); // Add subscription
                    ViewData["Message"] = "Subscription activated using wallet.";
                }
                else
                {
                    ViewData["Message"] = "Insufficient wallet balance.";
                }
            }
            else if (paymentMethod == "Coupon" && !string.IsNullOrEmpty(couponCode))
            {
                var coupon = DataStore.Coupons.FirstOrDefault(c => c.Code == couponCode && !coupon.IsUsed);

                if (coupon != null && coupon.DiscountAmount >= plan.Price)
                {
                    coupon.IsUsed = true;
                    AddUserSubscription(1, plan); // Add subscription
                    ViewData["Message"] = "Subscription activated using coupon.";
                }
                else
                {
                    ViewData["Message"] = "Invalid or insufficient coupon.";
                }
            }
            else
            {
                ViewData["Message"] = "Invalid payment method.";
            }

            return RedirectToAction("Index");
        }

        private void AddUserSubscription(int userId, SubscriptionPlan plan)
        {
            // Fixed free games for every user
            var fixedFreeGames = new List<int> { 1, 2 }; // IDs of free games

            DataStore.UserSubscriptions.Add(new UserSubscription
            {
                UserId = userId,
                SubscriptionPlanId = plan.Id,
                StartDate = DateTime.Now,
                EndDate = plan.Id == 1 ? DateTime.Now.AddMonths(1) : DateTime.Now.AddYears(1),
                FreeGameIds = fixedFreeGames
            });

            // Notify the user about free games
            var freeGameTitles = DataStore.Games
                .Where(g => fixedFreeGames.Contains(g.Id))
                .Select(g => g.Title)
                .ToList();

            ViewData["FreeGames"] = string.Join(", ", freeGameTitles);
        }
    }
}
