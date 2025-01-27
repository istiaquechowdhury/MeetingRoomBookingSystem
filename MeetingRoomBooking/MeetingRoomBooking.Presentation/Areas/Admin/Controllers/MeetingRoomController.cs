using AutoMapper;
using MeetingRoomBooking.Domain.Entities;
using MeetingRoomBooking.Presentation.Areas.Admin.Models;
using MeetingRoomBooking.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace MeetingRoomBooking.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class MeetingRoomController : Controller
    {
        private readonly IMeetingRoomManagementService _MeetingManagementService;
        private readonly IMapper _mapper;
       
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public MeetingRoomController(
        IMapper mapper, IMeetingRoomManagementService MeetingManagementService,
        IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _MeetingManagementService = MeetingManagementService;   
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetMeetingRoomJsonData([FromBody] MeetingRoomListModel model)
        {
            var result = _MeetingManagementService.GetMeetings(model.PageIndex, model.PageSize, model.Search, model.FormatSortExpression("Name", "location", "Capacity", "facilities", "Description", "Color", "ImagePath","Status", "AvailableDay","Time"));

            var ProductJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totaldisplay,
                data = (from record in result.data
                        select new string[]
                        {
                   
                    "",
                                       

                    HttpUtility.HtmlEncode(record.ImagePath),
                    HttpUtility.HtmlEncode(record.Name),
                    HttpUtility.HtmlEncode(record.Facilities),
                    HttpUtility.HtmlEncode(record.Capacity),
                    HttpUtility.HtmlEncode(record.Color),
                    HttpUtility.HtmlEncode(record.Status ? "Active" : "Inactive"),

                    record.Id.ToString(),

                        }
                    ).ToArray()
            };

            return Json(ProductJsonData);
        }

        public IActionResult Create()
        {

            var model = new MeetingRoomCreateModel();
            ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5" });
            ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });

            return View(model);
        }




       
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MeetingRoomCreateModel MeetingRoomCreateModel, bool redirectToNew = false)
        {
            if (ModelState.IsValid)
            {
                // Handle image file if provided
                if (MeetingRoomCreateModel.ImageFile != null && MeetingRoomCreateModel.ImageFile.Length > 0)
                {
                    // Define the directory to save the images
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath); // Create the directory if it doesn't exist
                    }

                    // Generate a unique file name for the image
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(MeetingRoomCreateModel.ImageFile.FileName);

                    // Combine the directory and file name
                    var filePath = Path.Combine(uploadPath, uniqueFileName);

                    // Save the file to the specified directory
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await MeetingRoomCreateModel.ImageFile.CopyToAsync(fileStream);
                    }

                    // Store the relative path in the database
                    MeetingRoomCreateModel.ImagePath = Path.Combine("uploads", uniqueFileName);
                }

                // Map ProductModel to Product entity
                var MeetingRoom = _mapper.Map<MeetingRoom>(MeetingRoomCreateModel);

                // Set related entities
              

                try
                {
                    // Save the product
                    _MeetingManagementService.CreateMeeting(MeetingRoom);



                    TempData["success"] = "Meeting inserted successfully";

                    // Redirect based on whether 'Save and New' was clicked
                    if (redirectToNew)
                    {
                        TempData["success"] = "Meeting inserted successfully";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Meeting name should be unique";
                   
                }

                return RedirectToAction("Index");
            }

            // If the model is invalid, re-populate dropdowns and return to the view

            ViewBag.Capacity = new SelectList(new[] { "1", "2","3","4","5" });
            ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });

            return View(MeetingRoomCreateModel);
        }



      
        public IActionResult Update(Guid id)
        {
            MeetingRoom meeting = _MeetingManagementService.GetMeeting(id);

           


            var model = _mapper.Map<MeetingRoomUpdateModel>(meeting);

            ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5" });
            ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });
            return View(model);
        }

       
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(MeetingRoomUpdateModel meetingUpdateModel, bool redirectToNew = false)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing product from the database
                var meeting = _MeetingManagementService.GetMeeting(meetingUpdateModel.Id);

                // Handle image removal if the checkbox is checked
                if (meetingUpdateModel.RemoveImage)
                {
                    // Delete the existing image if it exists
                    if (!string.IsNullOrEmpty(meeting.ImagePath))
                    {
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath, meeting.ImagePath);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath); // Delete the file
                        }
                        meetingUpdateModel.ImagePath = null; // Set the image path to null
                    }
                }
                else
                {
                    // Handle image file if a new one is uploaded
                    if (meetingUpdateModel.ImageFile != null && meetingUpdateModel.ImageFile.Length > 0)
                    {
                        // Define the upload path
                        var uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath); // Ensure the folder exists
                        }

                        // Generate a unique file name
                        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(meetingUpdateModel.ImageFile.FileName);

                        // Combine the folder path and file name
                        var filePath = Path.Combine(uploadPath, uniqueFileName);

                        // Save the new file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await meetingUpdateModel.ImageFile.CopyToAsync(fileStream);
                        }

                        // Set the new image path
                        // meetingUpdateModel.ImagePath = Path.Combine("uploads", uniqueFileName);
                        meetingUpdateModel.ImagePath = Path.Combine("uploads", uniqueFileName).Replace("\\", "/");

                        // Remove the old image if it exists
                        if (!string.IsNullOrEmpty(meeting.ImagePath))
                        {
                            var oldFilePath = Path.Combine(_hostingEnvironment.WebRootPath, meeting.ImagePath);
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }
                    }
                    else
                    {
                        // Retain the existing image path if no new image is uploaded
                        meetingUpdateModel.ImagePath = meeting.ImagePath;
                    }
                }

                // Map the updated model values to the existing product entity
                meeting = _mapper.Map(meetingUpdateModel, meeting);

                // Set related entities (Category, Taxes, Measurement)

                // Retain the image path
                meeting.ImagePath = meetingUpdateModel.ImagePath;

                try
                {
                    // Update the product in the database
                    _MeetingManagementService.UpdateMeeting(meeting);

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

            ViewBag.Capacity = new SelectList(new[] { "1", "2", "3", "4", "5" });
            ViewBag.Color = new SelectList(new[] { "Red", "Blue", "Green", "Purple", "Yellow" });
            return View(meetingUpdateModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
       
        public IActionResult Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _MeetingManagementService.DeleteMeetingRoom(id);

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
