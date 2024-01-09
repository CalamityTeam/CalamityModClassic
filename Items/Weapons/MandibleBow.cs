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
	public class MandibleBow : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mandible Bow");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 13;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 22;
	        Item.height = 40;
	        Item.useTime = 25;
	        Item.useAnimation = 25;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 2f;
	        Item.value = 15000;
	        Item.rare = ItemRarityID.Blue;
	        Item.UseSound = SoundID.Item5;
	        Item.autoReuse = false;
	        Item.shoot = ProjectileID.PurificationPowder;
	        Item.shootSpeed = 30f;
	        Item.useAmmo = 40;
	    }
	}
}