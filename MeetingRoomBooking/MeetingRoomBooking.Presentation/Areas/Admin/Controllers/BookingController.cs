﻿using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
