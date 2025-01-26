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

        public JsonResult GetClientJsonData([FromBody] ClientListModel model)
        {
            var result = _ClientManagementService.GetClients(model.PageIndex, model.PageSize, model.Search, model.FormatSortExpression("Clienttype", "Name", "Address", "phone", "DOB", "NID", "TIN"));

            var ProductJsonData = new
            {
                recordsTotal = result.total,
                recordsFiltered = result.totaldisplay,
                data = (from record in result.data
                        select new string[]
                        {
                   
                    // Construct full S3 URL for image
                    HttpUtility.HtmlEncode(record.Clienttype),
                    HttpUtility.HtmlEncode(record.Name),
                    HttpUtility.HtmlEncode(record.Address),
                    HttpUtility.HtmlEncode(record.phone),
                    HttpUtility.HtmlEncode(record.DOB),
                    HttpUtility.HtmlEncode(record.NID),
                    HttpUtility.HtmlEncode(record.TIN),
                    //HttpUtility.HtmlEncode(record.ReturnDocument),
                    //HttpUtility.HtmlEncode(record.AcknowledgementDocument),
                    //HttpUtility.HtmlEncode(record.TaxCertificate),
                    //HttpUtility.HtmlEncode(record.TINCertificate),
                    //HttpUtility.HtmlEncode(record.NIDFrontSide),
                    //HttpUtility.HtmlEncode(record.NIDBackSide),
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


    }
}
