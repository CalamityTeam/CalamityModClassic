using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class PlasmaRod : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plasma Rod");
			/* Tooltip.SetDefault("Casts a low-damage plasma bolt\n" +
                "Shooting a tile will cause several bolts with increased damage to fire\n" +
                "Shooting an enemy will cause several debuffs for a short time"); */
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
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 1, 0, 0);
            Item.rare = 1;
	        Item.UseSound = SoundID.Item109;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("PlasmaRay").Type;
	        Item.shootSpeed = 6f;
	    }
	}
}