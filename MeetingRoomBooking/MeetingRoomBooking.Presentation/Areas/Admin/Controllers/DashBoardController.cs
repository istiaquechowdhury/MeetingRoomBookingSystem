﻿using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
