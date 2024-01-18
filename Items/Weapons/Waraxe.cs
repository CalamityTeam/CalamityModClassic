﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Waraxe : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
    {
        //texture =("CalamityModClassic1Point1/Items/Weapons/Waraxe");
        return true;
    }
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Waraxe");
        Item.damage = 18;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 32;
        Item.height = 30;
        Item.useTime = 29;
        Item.useAnimation = 29;
        Item.useTurn = true;
        Item.axe = 7;
        Item.useStyle = 1;
        Item.knockBack = 5.25f;
        Item.value = 50000;
        Item.rare = 1;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }
}}