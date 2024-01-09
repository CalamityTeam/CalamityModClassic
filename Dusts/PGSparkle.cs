﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityModClassic1Point0.Dusts {
public class PGSparkle : ModDust
{
    public override void OnSpawn(Dust dust)
    {
        dust.velocity *= 0.75f;
        dust.noGravity = true;
        dust.noLight = true;
        dust.scale *= 1.0f;
    }

    public override bool Update(Dust dust)
    {
        dust.position += dust.velocity;
        dust.rotation += dust.velocity.X * 0.05f;
        dust.scale *= 0.99f;
        float light = 0.35f * dust.scale;
        Lighting.AddLight(dust.position, 0.5f, 0.05f, 0.5f);
        if(dust.scale < 0.5f)
        {
            dust.active = false;
        }
        return false;
    }
}}