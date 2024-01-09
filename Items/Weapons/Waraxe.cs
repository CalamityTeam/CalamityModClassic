using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Waraxe : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Waraxe");
	}

    public override void SetDefaults()
    {
        Item.damage = 22;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 32;
        Item.height = 30;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useTurn = true;
        Item.axe = 10;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 5.25f;
        Item.value = 50000;
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }
}}