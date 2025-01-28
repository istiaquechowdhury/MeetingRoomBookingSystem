using AutoMapper;
using MeetingRoomBooking.DataAccess.Identity;
using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Presentation.Areas.Admin.Models;
using MeetingRoomBooking.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace MeetingRoomBooking.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookingController : Controller
    {

        private readonly IBookingManagementService _bookingManagementService;
        private readonly IMapper _mapper;
        private readonly IMeetingRoomManagementService _meetingRoomManagementService;
        private readonly UserManager<ApplicationUser> _userManager;




        public BookingController(
        IMapper mapper, IBookingManagementService bookingManagementService,IMeetingRoomManagementService meetingRoomManagementService, UserManager<ApplicationUser> userManager)
        {
            _bookingManagementService = bookingManagementService;
            _mapper = mapper;
            _meetingRoomManagementService = meetingRoomManagementService;   
            _userManager = userManager;

           
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetBookingJsonData([FromBody] BookingListModel model)
        {
            var result = _bookingManagementService.GetBookings(model.PageIndex, model.PageSize, model.Search, model.FormatSortExpression("UsrName", "Pin", "PhoneNumber", "MeetingTitle", "MeetingPurpose", "RepeatBooking", "StatDate", "StartTime", "EndDate", "EndTime", "Department","Remarks"));

            var ProductJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totaldisplay,
                data = (from record in result.data
                        select new string[]
                        {

                  


                    HttpUtility.HtmlEncode(record.UserName),
                    HttpUtility.HtmlEncode(record.Pin),
                    HttpUtility.HtmlEncode(record.PhoneNumber),
                    HttpUtility.HtmlEncode(record.MeetingTitle),
                    HttpUtility.HtmlEncode(record.MeetingPurpose),
                    HttpUtility.HtmlEncode(record.RepeatBooking),
                    HttpUtility.HtmlEncode(record.StatDate),
                    HttpUtility.HtmlEncode(record.StartTime),
                    HttpUtility.HtmlEncode(record.EndDate),
                    HttpUtility.HtmlEncode(record.EndTime),
                    HttpUtility.HtmlEncode(record.Department),
                    HttpUtility.HtmlEncode(record.Remarks),




                    record.Id.ToString(),

                        }
                    ).ToArray()
            };

            return Json(ProductJsonData);
        }





        public async Task<IActionResult> Create()
        {

            var model = new BookingCreateModel();
            ViewBag.RepeatBooking = new SelectList(new[] { "No Repeat", "Daily", "Weekly", "Custom"});
            ViewBag.Department = new SelectList(new[] { "Science", "Programming", "Math", "English"});
            var user = await _userManager.GetUserAsync(User); // Retrieve the user asynchronously
            model.UserName = user?.UserName ?? string.Empty;

            //var meeting = _meetingRoomManagementService.GetMeetingRooms();

            return View(model);
        }

      
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingCreateModel bookingCreateModel, bool redirectToNew = false)
        {
            if (ModelState.IsValid)
            {
               
                // Map ProductModel to Product entity
                var booking = _mapper.Map<Booking>(bookingCreateModel);

                // Set related entities
                var user = await _userManager.GetUserAsync(User); // Retrieve the user asynchronously
                booking.UserName = user?.UserName ?? string.Empty;

                try
                {
                    // Save the product
                    _bookingManagementService.CreateBooking(booking);


                    TempData["success"] = "Item inserted successfully";

                    // Redirect based on whether 'Save and New' was clicked
                    if (redirectToNew)
                    {
                        TempData["success"] = "Item inserted successfully";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Item name should be unique";
                }

                return RedirectToAction("Index");
            }

            // If the model is invalid, re-populate dropdowns and return to the view
            var model = new BookingCreateModel();
            ViewBag.RepeatBooking = new SelectList(new[] { "No Repeat", "Daily", "Weekly", "Custom" });
            ViewBag.Department = new SelectList(new[] { "Science", "Programming", "Math", "English" });

            return View(bookingCreateModel);
        }

        public IActionResult Update(Guid id)
        {
            Booking booking = _bookingManagementService.GetBooking(id);
            
            var model = _mapper.Map<BookingUpdateModel>(booking);
            ViewBag.RepeatBooking = new SelectList(new[] { "No Repeat", "Daily", "Weekly", "Custom" });
            ViewBag.Department = new SelectList(new[] { "Science", "Programming", "Math", "English" });

            //var meeting = _meetingRoomManagementService.GetMeetingRooms();

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(BookingUpdateModel bookingUpdateModel, bool redirectToNew = false)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing product from the database
                var booking = _bookingManagementService.GetBooking(bookingUpdateModel.Id);

                // Handle image removal if the checkbox is checked


                // Map the updated model values to the existing product entity
                booking = _mapper.Map(bookingUpdateModel, booking);

                // Set related entities (Category, Taxes, Measurement)

                // Retain the image path
              

                try
                {
                    // Update the product in the database
                    _bookingManagementService.UpdateBooking(booking);

                    TempData["success"] = "Item updated successfully";

                    // Redirect based on whether 'Save and New' was clicked
                    if (redirectToNew)
                    {
                        // Redirect back to the create page to add a new product after updating the current one
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        // Redirect to the product list page (Index)
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Item should be unique";


                    // Return to the Index on error
                    return RedirectToAction("Index");
                }
            }

            // If the model is invalid, re-populate dropdowns and return to the view

            ViewBag.RepeatBooking = new SelectList(new[] { "No Repeat", "Daily", "Weekly", "Custom" });
            ViewBag.Department = new SelectList(new[] { "Science", "Programming", "Math", "English" });

            return View(bookingUpdateModel);
        }

        public IActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bookingManagementService.DeleteBooking(id);

                    TempData["success"] = "item Deleted Successfully";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["error"] = "item delete failed";



                    return RedirectToAction("Index");



                }

            }

            return View();
        }




    }
}
