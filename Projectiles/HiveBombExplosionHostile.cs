﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Projectiles
{
    public class HiveBombExplosionHostile : ModProjectile
    {
        public override void SetDefaults()
        {
            //Tooltip.SetDefault("Hive Bomb Explosion");
            Projectile.width = 200;
            Projectile.height = 200;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 5;
        }
        
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
        	target.AddBuff(Mod.Find<ModBuff>("Plague").Type, 500);
        }
    }
}