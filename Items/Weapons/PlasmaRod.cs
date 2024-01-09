using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class PlasmaRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Plasma Rod");
			//Tooltip.SetDefault("Casts a low-damage plasma bolt\nShooting a tile will cause several bolts with increased damage to fire\nShooting an enemy will cause several debuffs for a short time");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 8;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 10;
	        Item.width = 40;
	        Item.height = 40;
	        Item.useTime = 36;
	        Item.useAnimation = 36;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
	        Item.value = 60000;
	        Item.rare = ItemRarityID.Orange;
	        Item.UseSound = SoundID.Item109;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("PlasmaRay").Type;
	        Item.shootSpeed = 6f;
	    }
	}
}