﻿using System.Security.Claims;
using Homies.Data;
using Homies.Data.Models;
using Homies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Homies.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly HomiesDbContext data;

        public EventController(HomiesDbContext context)
        {
            data = context;
        }

        public async Task<IActionResult> All()
        {
            var events = await data.Events
                .AsNoTracking()
                .Select(e => new EventInfoViewModel(
                    e.Id,
                    e.Name,
                    e.Start,
                    e.Type.Name,
                    e.Organiser.UserName
                    ))
                .ToListAsync();

            return View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            var e = await data.Events
                .Where(e => e.Id == id)
                .Include(e => e.EventsParticipants)
                .FirstOrDefaultAsync();

            if (e == null)
            {
                return BadRequest();
            }


            string userId = GetUserId();

            if (!e.EventsParticipants.Any(p => p.HelperId == userId))
            {
                e.EventsParticipants.Add(new EventParticipant()
                {
                    EventId = e.Id,
                    HelperId = userId
                });

                await data.SaveChangesAsync();

            }
            return RedirectToAction("Joined");
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            string userId = GetUserId();

            var model = await data.EventsParticipants
                .Where(ep => ep.HelperId == userId)
                .AsNoTracking()
                .Select(ep => new EventInfoViewModel(
                    ep.EventId,
                    ep.Event.Name,
                    ep.Event.Start,
                    ep.Event.Type.Name,
                    ep.Event.Organiser.UserName
                    ))
                .ToListAsync();

            return View(model);
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? String.Empty;
        }
    }
}
