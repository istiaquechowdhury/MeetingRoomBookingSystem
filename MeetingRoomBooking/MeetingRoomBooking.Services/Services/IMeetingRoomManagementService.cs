﻿using MeetingRoomBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomBooking.Services.Services
{
    public interface IMeetingRoomManagementService
    {
        void CreateMeeting(MeetingRoom meetingRoom);
    }
}
