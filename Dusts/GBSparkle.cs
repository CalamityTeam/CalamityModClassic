﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Dusts {
public class GBSparkle : ModDust
{
    public override void OnSpawn(Dust dust)
    {
    	dust.velocity *= 0.3f;
    	dust.alpha = 100;
        dust.noGravity = true;
        dust.noLight = true;
        dust.scale = 0.25f;
    }

    public override bool Update(Dust dust)
    {
        dust.position += dust.velocity;
        dust.rotation += dust.velocity.X * 0.05f;
		dust.scale *= 0.99f;
        float light = 0.35f * dust.scale;
        Lighting.AddLight(dust.position, 0.05f, 0.4f, 0.25f);
        if (dust.scale < 0.1f)
        {
            dust.active = false;
        }
        return false;
    }
}}