﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Dusts {
public class TMSparkle : ModDust
{
    public override void OnSpawn(Dust dust)
    {
    	dust.velocity *= 0.1f;
        dust.noGravity = true;
        dust.noLight = true;
        dust.scale = 1.5f;
    }

    public override bool Update(Dust dust)
    {
        dust.position += dust.velocity;
        dust.rotation += dust.velocity.X * 0.05f;
		dust.scale *= 0.99f;
        float light = 0.35f * dust.scale;
        Lighting.AddLight(dust.position, light, light, light);
        if (dust.scale < 0.3f)
        {
            dust.active = false;
        }
        return false;
    }
}}