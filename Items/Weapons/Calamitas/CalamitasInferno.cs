using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Calamitas {
public class CalamitasInferno : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Lashes of Chaos");
		//Tooltip.SetDefault("Watch the world burn...");
	}

    public override void SetDefaults()
    {
        Item.damage = 72;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 20;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 25;
        Item.useAnimation = 25;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7.5f;
        Item.value = 550000;
        Item.rare = ItemRarityID.Lime;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("BrimstoneHellfireballFriendly").Type;
        Item.shootSpeed = 16f;
    }
}}