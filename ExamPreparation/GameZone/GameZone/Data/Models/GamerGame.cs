﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data.Models
{
    [PrimaryKey(nameof(GameId), nameof(GamerId))]
    public class GamerGame
    {
        [Comment("Identifier of the game")]
        public int GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        public Game Game { get; set; } = null!;

        [Comment("Identifier of the gamer")]
        public string GamerId { get; set; } = null!;

        [ForeignKey(nameof(GamerId))]
        public IdentityUser Gamer { get; set; } = null!;
    }
}