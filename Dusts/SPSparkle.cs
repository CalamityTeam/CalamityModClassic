﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point1.Dusts {
public class SPSparkle : ModDust
{
    public override void OnSpawn(Dust dust)
    {
        dust.velocity *= 0.1f;
        dust.noGravity = true;
        dust.noLight = true;
        dust.scale *= 0.5f;
    }

    public override bool Update(Dust dust)
    {
        dust.position += dust.velocity;
        dust.rotation += dust.velocity.X * 0.05f;
        dust.scale *= 0.99f;
        float light = 0.35f * dust.scale;
        Lighting.AddLight(dust.position, 0f, 0.6f, 1.0f);
        if(dust.scale < 0.1f)
        {
            dust.active = false;
        }
        return false;
    }
}}