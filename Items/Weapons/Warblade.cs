using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class Warblade : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Warblade");
	}

    public override void SetDefaults()
    {
        Item.damage = 27;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.width = 32;
        Item.height = 48;
        Item.useTime = 19;
        Item.useAnimation = 19;
        Item.useTurn = true;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 4.25f;
        Item.value = 50000;
        Item.rare = ItemRarityID.Blue;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
    }
}}