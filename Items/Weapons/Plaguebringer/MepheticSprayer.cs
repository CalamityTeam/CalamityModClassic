using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Plaguebringer
{
	public class MepheticSprayer : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Blight Spewer");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 99;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 76;
			Item.height = 36;
			Item.useTime = 10;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 2f;
			Item.UseSound = SoundID.Item34;
			Item.value = 1200000;
			Item.rare = ItemRarityID.Yellow;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("CorossiveFlames").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 7.5f;
			Item.useAmmo = 23;
		}
	}
}