using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class RaidersGlory : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Raider's Glory");
	}

    public override void SetDefaults()
    {
        Item.damage = 37;
        Item.DamageType = DamageClass.Ranged;
        Item.crit += 10;
        Item.width = 58;
        Item.height = 22;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 2.25f;
        Item.value = 100000;
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item5;
        Item.autoReuse = true;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 15f;
        Item.useAmmo = 40;
    }
}}